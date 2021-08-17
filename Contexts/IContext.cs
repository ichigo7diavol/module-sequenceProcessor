namespace Services.SequenceProcessor.Steps
{
    public interface IContext
    {
        public ISequenceProcessingService Processor { get; }
    }
}