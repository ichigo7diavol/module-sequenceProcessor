using System.Threading.Tasks;
using Services.SequenceProcessor.Steps.Dto;

namespace Services.SequenceProcessor.Steps
{
    public interface ISequenceStep { }

    public interface IParameterizedStep<TDto>
        : ISequenceStep
        where TDto : ISequenceStepDto
    {
        public Task Execute(TDto dto);
    }
}