using System;
using Services.SequenceProcessor.Steps;

namespace Services.SequenceProcessor.Contexts.Constraints
{
    public abstract class BaseConstraint<T> 
        : IConcreteContextConstraint<T>
        where T : IContext
    {
        protected readonly Type ContextType = typeof(T);

        public abstract bool IsCanProvideContext(Type type);

        public abstract bool IsCanProvideContext<TContext>() 
            where TContext : IContext;
    }
}