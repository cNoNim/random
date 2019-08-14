using UnityEngine;

namespace Testing
{
    public abstract class RandomSequence : ScriptableObject, IRandomSequence
    {
        public abstract void Reset();

        public abstract uint Next();

        public abstract string Name { get; }
    }
    
    public abstract class RandomNumberGeneratorSequence<T> : RandomSequence
        where T : IRandomNumberGenerator, new()
    {
        private static T _wrapper;
        
        protected IRandomNumberGenerator Wrapper
        {
            get
            {
                if (_wrapper == null)
                    _wrapper = new T();
                return _wrapper;
            }
        }
    }

    public abstract class RandomNumberGeneratorSeedSequence<T> : RandomNumberGeneratorSequence<T>
        where T : IRandomNumberGenerator, new()
    {
        [SerializeField]
        private int _seed;

        private object generator;

        public override void Reset()
        {
            generator = Wrapper.Generator(_seed);
        }

        public override uint Next()
        {
            return Wrapper.Next(generator);
        }


        public override string Name => Wrapper.IsSupportsSeed ? $"{Wrapper.Name}\nnumbers # of seed {_seed}" : $"{Wrapper.Name}\nnumbers #";
    }

    public abstract class RandomNumberGeneratorVarySeedSequence<T> : RandomNumberGeneratorSequence<T>
        where T : IRandomNumberGenerator, new()
    {
        [SerializeField]
        private int _index;

        private int seed;

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
            var generator = Wrapper.Generator(seed);
            seed++;
            for (var i = 0; i < _index; i++)
                Wrapper.Next(generator);
            return Wrapper.Next(generator);
        }

        public override string Name => $"{Wrapper.Name}\n{_index.Ordinal()} number of seed #";
    }
    
    public abstract class HashSequence<T> : RandomSequence
        where T : IHashGenerator, new()
    {
        private static T _wrapper;
        
        protected IHashGenerator Wrapper
        {
            get
            {
                if (_wrapper == null)
                    _wrapper = new T();
                return _wrapper;
            }
        }
    }

    public abstract class HashSeedSequence<T> : HashSequence<T>
        where T : IHashGenerator, new()
    {
        [SerializeField]
        private int _seed;

        private object generator;

        private int index;

        private object Generator => generator ?? (generator = Wrapper.Generator(_seed));

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


        public override string Name => Wrapper.IsSupportsSeed ? $"{Wrapper.Name}\nnumbers # of seed {_seed}" : $"{Wrapper.Name}\nnumbers #";
    }

    public abstract class HashVarySeedSequence<T> : HashSequence<T>
        where T : IHashGenerator, new()
    {
        [SerializeField]
        private int _index;

        private int seed;

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
            var value = Wrapper.Hash(Wrapper.Generator(seed), _index);
            seed++;
            return value;
        }

        public override string Name => $"{Wrapper.Name}\n{_index.Ordinal()} number of seed #";
    }
}