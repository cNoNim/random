namespace Testing
{
    public interface IRandomSequence {
        uint Next ();
        void Reset ();
        string Name { get; }
    }
}