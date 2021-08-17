using System;
using Services.SequenceProcessor.Steps.Dto;

namespace Services.SequenceProcessor.Steps.Handlers
{
    public interface IStepHandler
    {
        public Type DtoType { get; } 
        public void UnsafeExecute(ISequenceStepDto dto);
    }

    public interface IStepHandler<T> 
        : IStepHandler
        where T : ISequenceStepDto
    {
        public void Execute(T dto);
    }
}