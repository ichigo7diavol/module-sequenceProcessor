using System;
using Services.SequenceProcessor.Steps.Dto;

namespace Services.SequenceProcessor.Steps.Handlers
{
    public class StepHandler<TDto>
        : IStepHandler<TDto>
        where TDto : ISequenceStepDto
    {
        private readonly IParameterizedStep<TDto> _step;
        
        public Type DtoType => typeof(TDto);
        
        public StepHandler(IParameterizedStep<TDto> step)
        {
            _step = step ?? throw new ArgumentNullException(nameof(step));
        }
    
        public void Execute(TDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException();
            }
            _step.Execute(dto);
        }

        public void UnsafeExecute(ISequenceStepDto dto)
        {
            if (!(dto is TDto concreteDto))
            {
                throw new InvalidOperationException(
                    $"Wrong Type Of DTO Met {dto.GetType()} But Expected {typeof(TDto)}");
            }
            Execute(concreteDto);
        }
    }
}