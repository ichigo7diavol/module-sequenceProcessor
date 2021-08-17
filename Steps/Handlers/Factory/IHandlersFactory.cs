namespace Services.SequenceProcessor.Steps.Handlers.Factory
{
    public interface IHandlersFactory
    {
        public IStepHandler Create(ISequenceStep step);
    }
}