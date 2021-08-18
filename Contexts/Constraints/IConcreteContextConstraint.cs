using Services.SequenceProcessor.Steps;

namespace Services.SequenceProcessor.Contexts.Constraints
{
    public interface IConcreteContextConstraint<T>
        : IContextConstraint
        where T : IContext
    {
    }
}