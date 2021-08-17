using Services.SequenceProcessor.Steps.Dto;

namespace Services.SequenceProcessor
{
    public interface ISequenceProcessingService
    {
        public void Process<T>(T sequenceDto)
            where T : ISequenceStepDto;
        
        public void Process(ISequenceStepDto sequenceDto);
    }
}