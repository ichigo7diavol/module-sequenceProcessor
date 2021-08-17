using System;
using System.Threading.Tasks;
using Services.SequenceProcessor.Steps.Dto;

namespace Services.SequenceProcessor.Steps.Handlers
{
    public interface IStepHandler
    {
        public Type DtoType { get; } 
        
        public Task UnsafeExecute(ISequenceStepDto dto);
    }

    public interface IStepHandler<T> 
        : IStepHandler
        where T : ISequenceStepDto
    {
        public Task Execute(T dto);
    }
}