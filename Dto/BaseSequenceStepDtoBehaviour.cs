using UnityEngine;

namespace Services.SequenceProcessor.Steps.Dto
{
    public abstract class BaseSequenceStepDtoBehaviour
        : MonoBehaviour, ISequenceStepDto
    {
        [SerializeField]
        public string StepId;
    }
}