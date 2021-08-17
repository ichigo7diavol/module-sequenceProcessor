using System;
using System.Collections.Generic;
using Services.SequenceProcessor.Contexts;
using Services.SequenceProcessor.Steps.Dto;
using UnityEngine;

namespace Services.SequenceProcessor.Steps.BasicSteps
{
    public class ListStepDto 
        : BaseSequenceStepDtoBehaviour
    {
        [SerializeField] 
        public List<BaseSequenceStepDtoBehaviour> Steps = 
            new List<BaseSequenceStepDtoBehaviour>();
    }

    public class ListStep 
        : IContextualStep<DefaultContext, ListStepDto>
    {
        public void Execute(DefaultContext context, ListStepDto dto)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));   
            }
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));   
            }
            foreach (var stepDto in dto.Steps)
            {
                context.Processor.Process((ISequenceStepDto)stepDto);
            }
        }
    }
}