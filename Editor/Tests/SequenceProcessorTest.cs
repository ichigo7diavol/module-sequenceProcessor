using System;
using System.Collections;
using NUnit.Framework;
using Services.SequenceProcessor.Editor.Tests;
using Services.SequenceProcessor.Steps;
using Services.SequenceProcessor.Steps.BasicSteps;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Services.SequenceProcessor.Editor
{
    public class SequenceProcessorTest
    {
        private TestContext _context;
        private ISequenceProcessingService _sequenceProcessingService;
        
        
        private TestContext CreateContext()
        {
            return new TestContext();
        }

        private void ResolveBindings(DiContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }
            _sequenceProcessingService = container.
                Resolve<ISequenceProcessingService>();
        }

        [OneTimeSetUp]
        public void SequenceProcessorTestSimplePasses()
        {
            _context = CreateContext();
            
            _context.Initialize();

            ResolveBindings(_context.Container);
        }
        
        [UnityTest]
        public IEnumerator BasicListStepFunctionalTest()
        {
            var afterExecutionValue1 = 1;
            var afterExecutionValue2 = 2;

            var testVariable1 = 0;
            var testVariable2 = 0;
            
            var rootGameObject = new GameObject(nameof(BasicListStepFunctionalTest));
            var rootStepDto = rootGameObject.AddComponent<ListStepDto>();

            var actionTestDto1 = rootGameObject.AddComponent<ActionTestStepDto>();

            actionTestDto1.StepId = "FirstStep";
            
            actionTestDto1.TestAction =
                () => testVariable1 = afterExecutionValue1;
            
            var actionTestDto2 = rootGameObject.AddComponent<ActionTestStepDto>();
            
            actionTestDto2.StepId = "SecondStep";
            
            actionTestDto2.TestAction = 
                () => testVariable2 = afterExecutionValue2;
            
            rootStepDto.Steps.Add(actionTestDto1);
            rootStepDto.Steps.Add(actionTestDto2);
            
            try
            {
                Assert.DoesNotThrow(() => 
                    _sequenceProcessingService.Process(rootStepDto));
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(rootStepDto);
            }
            Assert.AreEqual(afterExecutionValue1, testVariable1);
            Assert.AreEqual(afterExecutionValue2, testVariable2);
                
            return null;
        }
    }
}