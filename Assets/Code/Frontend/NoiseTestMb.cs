using System.Collections.Generic;
using Testing;
using UnityEngine;

namespace Frontend
{
	public class TestFront {
		public bool Enabled = true;
		public Texture2D NoiseTexture;
		public Texture2D CoordTexture;
	}

	public class NoiseTestMb : MonoBehaviour
	{
		private const int Width = 256;

		[SerializeField]
		private Font _font;
		
		[SerializeField]
		private RandomSequence[] _sequences;

		private readonly List<RandomnessTest> tests = new List<RandomnessTest>();
		private TestFront[] fronts;
		private bool showOptions;

		private GUIStyle header;
		private GUIStyle style;
		private GUIStyle button;

		private bool showNoise = true;
		private bool showCoords = true;
		private bool showStats = true;

		private int index;
		private Vector2 optionsScroll;
		private int lastScreenHeight;
		private Vector2 scroll;

		private void Start()
		{
			foreach (var sequence in _sequences)
				tests.Add(new RandomnessTest(sequence));

			fronts = new TestFront[tests.Count];

			for (var i = 0; i < fronts.Length; i++)
			{
				Initialize(i);
			}
		}

		private void Initialize(int testNr)
		{
			var test = tests[testNr];
			var front = new TestFront();
			fronts[testNr] = front;

			// Create noise texture.
			front.NoiseTexture = new Texture2D(256, 256) {filterMode = FilterMode.Point};
			test.Reset();
			for (var j = front.NoiseTexture.height - 1; j >= 0; j--)
			{
				for (var i = 0; i < front.NoiseTexture.width; i++)
				{
					var f = test.NoiseSequence[i + j * 256];
					front.NoiseTexture.SetPixel(i, j, new Color(f, f, f, 1));
				}
			}

			front.NoiseTexture.Apply();

			front.CoordTexture = new Texture2D(256, 256) {filterMode = FilterMode.Point};
			for (var j = front.CoordTexture.height - 1; j >= 0; j--)
			{
				for (var i = 0; i < front.CoordTexture.width; i++)
				{
					var f = 1.0f - test.CoordsArray[i, j];
					front.CoordTexture.SetPixel(i, j, new Color(f, f, f, 1));
				}
			}

			front.CoordTexture.Apply();
		}

		private void OnGUI()
		{
			if (Event.current.type == EventType.Repaint)
				GL.Clear(false, true, Color.white);
			var screenHeight = Screen.height;
			var screenWidth = Screen.width;

			var menuHeight = screenHeight * 0.1f;
			var bottomLine = screenHeight - menuHeight;

			if (style == null || lastScreenHeight != screenHeight)
			{
				style = new GUIStyle("label")
				{
					font = _font,
					normal = {textColor = Color.black},
					fontSize = Mathf.Max(10, (int) (screenHeight * 0.01f))
				};
				header = new GUIStyle("label")
				{
					normal = {textColor = Color.black}, 
					fontSize = Mathf.Max(12, (int) (screenHeight * 0.012f)),
					fontStyle = FontStyle.Bold
				};
				button = new GUIStyle("button")
				{
					fontSize = Mathf.Max(12, (int) (screenHeight * 0.012f)),
					fontStyle = FontStyle.Bold
				};
			}
			lastScreenHeight = screenHeight;

			if (index < 0)
				index = 0;
			var length = fronts.Length;
			if (index >= length)
				index = length - 1;
			
			var front = fronts[index];
			var test = tests[index];

			GUILayout.BeginArea(new Rect(0, 0, screenWidth, bottomLine));
			scroll = GUILayout.BeginScrollView(scroll);

			GUILayout.Label(test.Name, header);

			if (showNoise)
			{
				GUILayout.Label(front.NoiseTexture, GUILayout.Width(Width));
				GUILayout.Label("Sequence of 65536 random values.", style);
			}

			if (showCoords)
			{
				GUILayout.Label(front.CoordTexture, GUILayout.Width(Width));
				GUILayout.Label("Plot of 500000 random coordinates.", style);
			}

			if (showStats) 
				GUILayout.Label(test.Result, style);

			GUILayout.EndScrollView();
			GUILayout.EndArea();
			
			GUILayout.BeginArea(new Rect(0, bottomLine, screenWidth, menuHeight));
			
			GUILayout.BeginHorizontal();

			var height = GUILayout.Height(menuHeight);
			var maxWidth = GUILayout.MaxWidth(menuHeight);

			if (GUILayout.Button("<", button, height, maxWidth))
				index--;
			
			if (GUILayout.Button("Options", button, height))
				showOptions = !showOptions;

			if (GUILayout.Button(">", button, height, maxWidth))
				index++;

			GUILayout.EndHorizontal();
			
			GUILayout.EndArea();

			if (showOptions)
				GUILayout.Window(0, new Rect(0, 0, screenWidth, bottomLine), OptionsWindow, "Options", header);
		}

		private void OptionsWindow(int id)
		{
			optionsScroll = GUILayout.BeginScrollView(optionsScroll);
			showNoise = GUILayout.Toggle(showNoise, "Show Noise Image", button);
			showCoords = GUILayout.Toggle(showCoords, "Show Coordinates Plot", button);
			showStats = GUILayout.Toggle(showStats, "Show Statistics", button);
			GUILayout.EndScrollView();
		}
	}
}