using System;
using System.Threading.Tasks;
using Services.SequenceProcessor.Providers;
using Services.SequenceProcessor.Steps.Dto;
using UnityEngine;

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

        public async void Process(ISequenceStepDto sequenceDto)
        {
            try
            {
                await AwaitableProcess(sequenceDto);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
        public async void Process<T>(T sequenceDto)
            where T : ISequenceStepDto
        {
            try
            {
                await AwaitableProcess(sequenceDto);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public async Task AwaitableProcess(ISequenceStepDto sequenceDto)
        {
            if (sequenceDto == null)
            {
                throw new ArgumentNullException(nameof(sequenceDto));
            }
            var step = _sequenceProvider.GetStep(sequenceDto.GetType());

            await step.UnsafeExecute(sequenceDto);
        }
        
        public async Task AwaitableProcess<T>(T sequenceDto) 
            where T : ISequenceStepDto
        {
            if (sequenceDto == null)
            {
                throw new ArgumentNullException(nameof(sequenceDto));
            }
            var step = _sequenceProvider.GetStep<T>();

            await step.Execute(sequenceDto);
        }
    }
}