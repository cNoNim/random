using UnityEngine;

namespace Testing
{
	public abstract class RandomSequence : ScriptableObject, IRandomSequence
	{
		public abstract void Reset();

		public abstract uint Next();

		public abstract string Name { get; }
	}

	public abstract class RandomNumberGeneratorSequence<T, TS> : RandomSequence
		where T : IRandomNumberGenerator<TS>, new()
	{
		private static T _wrapper;

		protected IRandomNumberGenerator<TS> Wrapper
		{
			get
			{
				if (_wrapper == null)
					_wrapper = new T();
				return _wrapper;
			}
		}
	}

	public abstract class RandomNumberGeneratorSeedSequence<T, TS> : RandomNumberGeneratorSequence<T, TS>
		where T : IRandomNumberGenerator<TS>, new()
	{
		[SerializeField]
		private int _seed;

		private TS generator;


		public override string Name => Wrapper.IsSupportsSeed
			? $"{Wrapper.Name}\nnumbers # of seed {_seed}"
			: $"{Wrapper.Name}\nnumbers #";

		public override void Reset()
		{
			generator = Wrapper.Create(_seed);
		}

		public override uint Next()
		{
			return Wrapper.Next(generator);
		}
	}

	public abstract class RandomNumberGeneratorVarySeedSequence<T, TS> : RandomNumberGeneratorSequence<T, TS>
		where T : IRandomNumberGenerator<TS>, new()
	{
		[SerializeField]
		private int _index;

		private int seed;

		public override string Name => $"{Wrapper.Name}\n{_index.Ordinal()} number of seed #";

		private void Awake()
		{
			Reset();
		}

		public override void Reset()
		{
			seed = 0;
		}

		public override uint Next()
		{
			var generator = Wrapper.Create(seed);
			seed++;
			for (var i = 0; i < _index; i++)
				Wrapper.Next(generator);
			return Wrapper.Next(generator);
		}
	}

	public abstract class HashSequence<T, TS> : RandomSequence
		where T : IHashGenerator<TS>, new()
	{
		private static T _wrapper;

		protected IHashGenerator<TS> Wrapper
		{
			get
			{
				if (_wrapper == null)
					_wrapper = new T();
				return _wrapper;
			}
		}
	}

	public abstract class HashSeedSequence<T, TS> : HashSequence<T, TS>
		where T : IHashGenerator<TS>, new()
	{
		[SerializeField]
		private int _seed;

		private TS generator;

		private int index;
		private bool initialized;

		private TS Generator
		{
			get
			{
				if (initialized)
					return generator;
				initialized = true;
				return generator = Wrapper.Create(_seed);
			}
		}


		public override string Name => Wrapper.IsSupportsSeed
			? $"{Wrapper.Name}\nnumbers # of seed {_seed}"
			: $"{Wrapper.Name}\nnumbers #";

		public override void Reset()
		{
			index = 0;
		}

		public override uint Next()
		{
			var value = Wrapper.Hash(Generator, index);
			index++;
			return value;
		}
	}

	public abstract class HashVarySeedSequence<T, TS> : HashSequence<T, TS>
		where T : IHashGenerator<TS>, new()
	{
		[SerializeField]
		private int _index;

		private int seed;

		public override string Name => $"{Wrapper.Name}\n{_index.Ordinal()} number of seed #";

		private void Awake()
		{
			Reset();
		}

		public override void Reset()
		{
			seed = 0;
		}

		public override uint Next()
		{
			var value = Wrapper.Hash(Wrapper.Create(seed), _index);
			seed++;
			return value;
		}
	}
}
