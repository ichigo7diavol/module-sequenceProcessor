using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Services.SequenceProcessor.Exceptions;
using Services.SequenceProcessor.Steps.BasicSteps;
using Services.SequenceProcessor.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Services.SequenceProcessor.Editor.Tests
{
    public class SequenceProcessorTest
    {
        private TestingContext _context;
        private ISequenceProcessingService _sequenceProcessingService;
        
        
        private TestingContext CreateContext()
        {
            return new TestingContext();
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

            Task task;
            IEnumerator enumerator;
            try
            {
                task = _sequenceProcessingService.
                    AwaitableProcess(rootStepDto);
            
                enumerator = task.AsEnumerator(
                    () => UnityEngine.Object.DestroyImmediate(rootStepDto));
            }
            catch (Exception _)
            {
                if (rootStepDto != null)
                {
                    UnityEngine.Object.DestroyImmediate(rootStepDto);
                }
                throw;
            }
            while (enumerator.MoveNext())
            {
                yield return null;
            }
            Assert.AreEqual(afterExecutionValue1, testVariable1);
            Assert.AreEqual(afterExecutionValue2, testVariable2);
        }

        [UnityTest]
        public IEnumerator ContextualSequenceStepTest()
        {
            Task task;
            IEnumerator enumerator = null;

            bool isComplete = false;
            
            try
            {
                task = _sequenceProcessingService.
                    AwaitableProcess(new ActionContextualTestStepDto());
            
                enumerator = task.AsEnumerator();
            }
            catch (Exception _)
            {
                var exception = _;

                if (_ is AggregateException aggregateException)
                {
                    var flatten = aggregateException.Flatten();

                    exception = flatten.InnerExceptions.
                        FirstOrDefault(e => e is CantProvideContextException);
                }
                if (exception == null)
                {
                    Assert.Fail($"Must Throw Exception Of Type {nameof(CantProvideContextException)}");
                }
                isComplete = true;
            }
            if (!isComplete)
            {
                do
                {
                    try
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                    }
                    catch (Exception _)
                    {
                        var exception = _;

                        if (_ is AggregateException aggregateException)
                        {
                            var flatten = aggregateException.Flatten();

                            exception = flatten.InnerExceptions.
                                FirstOrDefault(e => e is CantProvideContextException);
                        }
                        if (exception == null)
                        {
                            Assert.Fail($"Must Throw Exception Of Type {nameof(CantProvideContextException)}");
                        }
                        isComplete = true;
                    }
                    yield return null;
                } 
                while (true);
            }
            if (!isComplete)
            {
                Assert.Fail($"Must Throw Exception Of Type {nameof(CantProvideContextException)}");
            }
        }
    }
}