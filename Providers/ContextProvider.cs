using System;
using System.Collections.Generic;
using System.Linq;
using Services.SequenceProcessor.Contexts.Constraints;
using Services.SequenceProcessor.Exceptions;
using Zenject;

namespace Services.SequenceProcessor.Steps.Providers
{
    public class ContextProvider : IContextProvider
    {
        private readonly IInstantiator _instantiator;
        private readonly IReadOnlyCollection<IContextConstraint> _constraints;

        private readonly Type _contextInterface = typeof(IContext);
        
        public ContextProvider(IInstantiator instantiator, 
            List<IContextConstraint> constraints)
        {
            _instantiator = instantiator ?? 
                throw new ArgumentNullException(nameof(instantiator));
            
            _constraints = constraints ?? 
                throw new ArgumentNullException(nameof(constraints));
        }

        public T Get<T>() 
            where T : IContext
        {
            if (!_constraints.All(c => c.IsCanProvideContext<T>()))
            {
                var type = typeof(T);
                
                throw new CantProvideContextException($"Can't Provide Context {type}", type);
            }
            return _instantiator.Instantiate<T>();
        }
        
        public IContext Get(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (!_contextInterface.IsAssignableFrom(type))
            {
                throw new ArgumentException($"Wrong Argument Type: {type}");
            }
            if (!_constraints.All(c => c.IsCanProvideContext(type)))
            {
                throw new CantProvideContextException($"Can't Provide Context {type}", type);
            }
            return (IContext)_instantiator.Instantiate(type);
        }
    }
}