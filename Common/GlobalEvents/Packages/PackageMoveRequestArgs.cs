namespace Common.GlobalEvents.Packages
{
    public class PackageMoveRequestArgs
    {
        public int SourceIndex { get; }
        public int TargetIndex { get; }

        public PackageMoveRequestArgs(int sourceIndex, int targetIndex)
        {
            SourceIndex = sourceIndex;
            TargetIndex = targetIndex;
        }
    }
}
