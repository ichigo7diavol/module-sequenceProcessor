using System;
using System.Collections.Generic;
using Services.SequenceProcessor.Steps;
using Services.SequenceProcessor.Steps.Dto;
using Services.SequenceProcessor.Steps.Handlers;
using Services.SequenceProcessor.Steps.Handlers.Factory;

namespace Services.SequenceProcessor.Providers
{
    public class SequenceStepProvider 
        : ISequenceStepProvider
    {
        private readonly Dictionary<Type, IStepHandler> _cache = 
            new Dictionary<Type, IStepHandler>();

        private readonly IHandlersFactory _handlersFactory;
        
        public SequenceStepProvider(List<ISequenceStep> steps, 
            IHandlersFactory handlersFactory)
        {
             if (steps == null)
            {
                throw new ArgumentNullException(nameof(steps));
            }
            _handlersFactory = handlersFactory ?? 
                throw new ArgumentNullException(nameof(handlersFactory));
                
            foreach (var step in steps)
            {
                if (step == null)
                {
                    throw new InvalidOperationException(
                        "Found Empty Step In Passed Steps Collection");
                }
                var handler = _handlersFactory.Create(step);
                
                _cache.Add(handler.DtoType, handler);
            }
        }

        public IStepHandler<T> GetStep<T>()
            where T : ISequenceStepDto
        {
            var type = typeof(T);
            
            if (_cache.TryGetValue(type, out var value))
            {
                return value as IStepHandler<T>;
            }
            throw new InvalidOperationException($"Can't Provide Step With DTO Type: {type}");
        }
        
        public IStepHandler GetStep(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (_cache.TryGetValue(type, out var value))
            {
                return value;
            }
            throw new InvalidOperationException($"Can't Provide Step With DTO Type: {type}");
        }
    }
}