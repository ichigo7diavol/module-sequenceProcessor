using Services.SequenceProcessor.Steps.Dto;

namespace Services.SequenceProcessor.Steps
{
    public interface ISequenceStep { }

    public interface IParameterizedStep<TDto>
        : ISequenceStep
        where TDto : ISequenceStepDto
    {
        public void Execute(TDto dto);
    }
}