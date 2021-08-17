using System;
using Services.SequenceProcessor.Steps;
using Services.SequenceProcessor.Steps.Handlers;
using Services.SequenceProcessor.Steps.Providers;

namespace Services.SequenceProcessor.Utils
{
    public static class Constants
    {
        public static readonly Type ContextualStepInterface = 
            typeof(IContextualStep<,>);

        public static readonly Type ParameterizedStepInterface = 
            typeof(IParameterizedStep<>);
        
        public static readonly Type ContextProvider = 
            typeof(IContextProvider);

        public static readonly Type ContextualStepHandler =
            typeof(ContextualStepHandler<,>);
        
        public static readonly Type ParameterizedStepHandler =
            typeof(StepHandler<>);
        
        public const int ParameterizedStepDtoArgumentIndex = 0;
        public const int ContextualStepDtoArgumentIndex = 1;
        
        public const int ContextualStepContextArgumentIndex = 0;
        
        public const int ContextualCtorArgumentsCount = 2;
        public const int ParametrizedCtorArgumentsCount = 1;
    }
}