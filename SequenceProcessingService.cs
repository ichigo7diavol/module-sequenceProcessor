using System;
using Services.SequenceProcessor.Providers;
using Services.SequenceProcessor.Steps.Dto;

namespace Services.SequenceProcessor
{
    public class SequenceProcessingService 
        : ISequenceProcessingService
    {
        private readonly ISequenceStepProvider _sequenceProvider;
        
        public SequenceProcessingService(ISequenceStepProvider sequenceProvider)
        {
            _sequenceProvider = sequenceProvider ?? 
                throw new ArgumentNullException(nameof(sequenceProvider));
        }

        public void Process<T>(T sequenceDto)
            where T : ISequenceStepDto
        {
            if (sequenceDto == null)
            {
                throw new ArgumentNullException(nameof(sequenceDto));
            }
            var step = _sequenceProvider.GetStep<T>();
                
            step.Execute(sequenceDto);
        }

        public void Process(ISequenceStepDto sequenceDto)
        {
            if (sequenceDto == null)
            {
                throw new ArgumentNullException(nameof(sequenceDto));
            }
            var step = _sequenceProvider.GetStep(sequenceDto.GetType());
            
            step.UnsafeExecute(sequenceDto);
        }
    }
}