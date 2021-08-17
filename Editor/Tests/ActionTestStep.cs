using System;
using System.Threading.Tasks;
using Services.SequenceProcessor.Steps;
using Services.SequenceProcessor.Steps.Dto;

namespace Services.SequenceProcessor.Editor.Tests
{
    public class ActionTestStepDto : BaseSequenceStepDtoBehaviour
    {
        public Action TestAction;
    }
    
    public class ActionTestStep : IParameterizedStep<ActionTestStepDto>
    {
        public Task Execute(ActionTestStepDto dto)
        {
            dto.TestAction?.Invoke();

            return Task.CompletedTask;
        }
    }
}