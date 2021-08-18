using System;
using System.Threading.Tasks;
using Services.SequenceProcessor.Contexts;
using Services.SequenceProcessor.Contexts.Constraints;
using Services.SequenceProcessor.Steps;
using Services.SequenceProcessor.Steps.Dto;

namespace Services.SequenceProcessor.Editor.Tests
{
    public class TestConstraint 
        : BaseConstraint<TestContext>
    {
        public override bool IsCanProvideContext(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            return type != ContextType;
        }

        public override bool IsCanProvideContext<TContext>()
        {
            var type = typeof(TContext);
                
            return type != ContextType;
        }
    }

    public class TestContext 
        : BaseContext
    {
        public TestContext(ISequenceProcessingService processor) 
            : base(processor)
        {
        }
    }

    public class ActionContextualTestStepDto 
        : ISequenceStepDto 
    {
    }

    public class ActionContextualTestStep 
        : IContextualStep<TestContext, ActionContextualTestStepDto>
    {
        public Task Execute(TestContext context, ActionContextualTestStepDto dto)
        {
            return Task.CompletedTask;
        }
    }
}