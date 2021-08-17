using System;
using Services.SequenceProcessor.Steps;

namespace Services.SequenceProcessor.Contexts
{
    public interface IContextConstraint
    {
        public bool IsCanProvideContext(Type type);
        
        public bool IsCanProvideContext<TContext>() 
            where TContext : IContext;
    }
}