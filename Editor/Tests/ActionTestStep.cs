using System;
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
        public void Execute(ActionTestStepDto dto)
        {
            dto.TestAction?.Invoke();
        }
    }
}