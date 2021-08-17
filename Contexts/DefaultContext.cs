namespace Services.SequenceProcessor.Contexts
{
    public class DefaultContext : BaseContext
    {
        public DefaultContext(ISequenceProcessingService processor) 
            : base(processor)
        {
        }
    }
}