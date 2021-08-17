using System;
using Services.SequenceProcessor.Steps.Dto;
using Services.SequenceProcessor.Steps.Providers;

namespace Services.SequenceProcessor.Steps.Handlers
{
    public class ContextualStepHandler<TContext, TDto>
        : IStepHandler<TDto>
        where TDto : ISequenceStepDto
        where TContext : class, IContext
    {
        private readonly IContextProvider _contextProvider;
        private readonly IContextualStep<TContext, TDto> _step;

        public Type DtoType => typeof(TDto);

        public ContextualStepHandler(IContextualStep<TContext, TDto> step,
            IContextProvider provider)
        {
            _step = step ?? throw new ArgumentNullException(nameof(step));

            _contextProvider = provider ?? 
                               throw new ArgumentNullException(nameof(provider));
        }

        public void Execute(TDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException();
            }
            var context = _contextProvider.Get<TContext>();
            
            _step.Execute(context, dto);
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