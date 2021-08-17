using System.Threading.Tasks;
using Services.SequenceProcessor.Steps.Dto;

namespace Services.SequenceProcessor.Steps
{
    public interface IContextualStep<TContext, TDto> 
        : ISequenceStep
        where TDto : ISequenceStepDto 
        where TContext : IContext
    {
        public Task Execute(TContext context, TDto dto);
    }
}