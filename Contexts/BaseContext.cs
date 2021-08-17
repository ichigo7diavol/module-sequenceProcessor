using System;
using Services.SequenceProcessor.Steps;

namespace Services.SequenceProcessor.Contexts
{
    public abstract class BaseContext : IContext
    {
        public ISequenceProcessingService Processor { get; }
        
        public BaseContext(ISequenceProcessingService processor)
        {
            Processor = processor ?? 
                throw new ArgumentNullException(nameof(processor));
        }
    }
}