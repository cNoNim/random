using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Testing;
using UnityEngine;

namespace Frontend
{
	public class TestFront
	{
		public Texture2D CoordTexture;
		public bool Enabled;
		public bool Initialized;
		public Texture2D NoiseTexture;
		public RandomnessTest Test;
	}

	public class RandomTest : MonoBehaviour
	{
		private const int Width = 256;

		private readonly List<TestFront> tests = new List<TestFront>();

		[SerializeField]
		private Font _font;

		[SerializeField]
		private RandomSequence[] _sequences;

		private GUIStyle button;

		private GUIStyle header;
		private bool initializeAllStarted;
		private int lastScreenHeight;
		private GUIStyle loading;

		private Vector2 optionsScroll;

		private int progress;
		private Vector2 scroll;
		private bool showCoords = true;

		private bool showNoise = true;
		private bool showOptions;
		private bool showStats = true;
		private GUIStyle stats;
		private GUIStyle style;

		private void Start()
		{
			foreach (var sequence in _sequences)
				tests.Add(new TestFront
				{
					Test = new RandomnessTest(sequence)
				});
		}

		public void InitializeAll()
		{
			if (initializeAllStarted)
				return;
			initializeAllStarted = true;
			StartCoroutine(Initialization());
		}

		private IEnumerator Initialization()
		{
			foreach (var test in tests)
			{
				if (test.Initialized)
					continue;

				yield return null;
				Initialize(test);
			}
		}

		private void Initialize(TestFront front)
		{
			if (front.Initialized)
				return;
			front.Initialized = true;

			var test = front.Test;
			test.Test();

			front.NoiseTexture = new Texture2D(256, 256) {filterMode = FilterMode.Point};
			test.Reset();
			for (var j = front.NoiseTexture.height - 1; j >= 0; j--)
			for (var i = 0; i < front.NoiseTexture.width; i++)
			{
				var f = test.NoiseSequence[i + j * 256];
				front.NoiseTexture.SetPixel(i, j, new Color(f, f, f, 1));
			}

			front.NoiseTexture.Apply();

			front.CoordTexture = new Texture2D(256, 256) {filterMode = FilterMode.Point};
			for (var j = front.CoordTexture.height - 1; j >= 0; j--)
			for (var i = 0; i < front.CoordTexture.width; i++)
			{
				var f = 1.0f - test.CoordsArray[i, j];
				front.CoordTexture.SetPixel(i, j, new Color(f, f, f, 1));
			}

			front.CoordTexture.Apply();

			progress++;
		}

		private void OnGUI()
		{
			if (Event.current.type == EventType.Repaint)
				GL.Clear(false, true, Color.white);
			var screenHeight = Screen.height;
			var screenWidth = Screen.width;
			var pt = screenHeight / 100f;

			var menuHeight = screenHeight * 0.1f;
			var bottomLine = screenHeight - menuHeight;

			if (style == null || lastScreenHeight != screenHeight)
			{
				style = new GUIStyle("label")
				{
					font = _font,
					normal = {textColor = Color.black},
					fontSize = Mathf.Max(10, (int) (screenHeight * 0.01f)),
					alignment = TextAnchor.UpperCenter
				};
				stats = new GUIStyle("label")
				{
					font = _font,
					normal = {textColor = Color.black},
					fontSize = Mathf.Max(10, (int) (screenHeight * 0.01f))
				};
				header = new GUIStyle("label")
				{
					normal = {textColor = Color.black},
					fontSize = Mathf.Max(12, (int) (screenHeight * 0.012f)),
					fontStyle = FontStyle.Bold,
					alignment = TextAnchor.UpperCenter
				};
				loading = new GUIStyle("label")
				{
					normal = {textColor = Color.black},
					fontSize = Mathf.Max(12, (int) (screenHeight * 0.012f)),
					fontStyle = FontStyle.Bold,
					alignment = TextAnchor.UpperCenter
				};
				button = new GUIStyle("button")
				{
					fontSize = Mathf.Max(12, (int) (screenHeight * 0.012f)),
					fontStyle = FontStyle.Bold
				};
				var scrollSize = screenHeight * 0.04f;
				GUI.skin.horizontalScrollbar.fixedHeight = scrollSize;
				GUI.skin.horizontalScrollbarThumb.fixedHeight = scrollSize;
				GUI.skin.verticalScrollbar.fixedWidth = scrollSize;
				GUI.skin.verticalScrollbarThumb.fixedWidth = scrollSize;
			}

			lastScreenHeight = screenHeight;

			var active = tests.Count(t => t != null && t.Enabled && t.Initialized);

			var pageWidth = pt * 30;
			scroll = GUI.BeginScrollView(new Rect(0, 0, screenWidth, bottomLine), scroll,
				new Rect(0, 0, pageWidth * active, pageWidth * 3));

			var count = tests.Count;
			var offset = 0;
			for (var i = 0; i < count; i++)
			{
				var front = tests[i];
				var test = front.Test;

				if (!front.Enabled || !front.Initialized)
					continue;

				GUILayout.BeginArea(new Rect(offset * pageWidth, 0, pageWidth, pageWidth * 3));
				offset++;

				GUILayout.Label(test.Name, header);

				var textureSize = Mathf.Min(Width, pt * 30);
				if (showNoise)
				{
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					GUILayout.Label(front.NoiseTexture, GUILayout.Width(textureSize), GUILayout.Height(textureSize));
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.Label("Sequence of 65536 random values.", style);
				}

				if (showCoords)
				{
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					GUILayout.Label(front.CoordTexture, GUILayout.Width(textureSize), GUILayout.Height(textureSize));
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.Label("Plot of 500000 random coordinates.", style);
				}

				if (showStats)
				{
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					GUILayout.Label(test.Result, stats);
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
				}

				GUILayout.EndArea();
			}

			GUI.EndScrollView();

			GUILayout.BeginArea(new Rect(0, bottomLine, screenWidth, menuHeight));
			var height = GUILayout.Height(menuHeight);
			if (GUILayout.Button("Options", button, height))
				showOptions = !showOptions;

			GUILayout.EndArea();

			if (showOptions)
				GUILayout.Window(0, new Rect(0, 0, screenWidth, bottomLine), OptionsWindow, "Options", header);

			if (!initializeAllStarted)
			{
				if (GUI.Button(new Rect(0, bottomLine - menuHeight, screenWidth, menuHeight),
					"Load All", button))
					InitializeAll();
			}
			else if (progress != count)
			{
				GUI.Label(new Rect(0, bottomLine - menuHeight, screenWidth, menuHeight),
					$"Loading {progress}/{count}", loading);
			}
		}

		private void OptionsWindow(int id)
		{
			optionsScroll = GUILayout.BeginScrollView(optionsScroll);
			showNoise = GUILayout.Toggle(showNoise, "Show Noise Image", button);
			showCoords = GUILayout.Toggle(showCoords, "Show Coordinates Plot", button);
			showStats = GUILayout.Toggle(showStats, "Show Statistics", button);

			foreach (var front in tests)
			{
				front.Enabled = GUILayout.Toggle(front.Enabled, front.Test.Name, button);
				if (front.Enabled && !front.Initialized)
					Initialize(front);
			}

			GUILayout.EndScrollView();
		}
	}
}
