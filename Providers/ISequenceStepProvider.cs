using System;
using Services.SequenceProcessor.Steps.Dto;
using Services.SequenceProcessor.Steps.Handlers;

namespace Services.SequenceProcessor.Providers
{
    public interface ISequenceStepProvider
    {
        public IStepHandler<T> GetStep<T>()
            where T : ISequenceStepDto;
        
        public IStepHandler GetStep(Type type);
    }
}