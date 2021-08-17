using System;

namespace Services.SequenceProcessor.Steps.Providers
{
    public interface IContextProvider
    {
        public T Get<T>() where T : IContext;
        public IContext Get(Type type);
    }
}