using System;
using System.Collections;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Services.SequenceProcessor.Utils
{
    public static class TaskUtils
    {
        public static IEnumerator AsIEnumerator<T>(this Task<T> task, 
            Action onError = null)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }
            if (task.IsFaulted)
            {
                onError?.Invoke();
                
                ExceptionDispatchInfo.
                    Capture(task.Exception).
                    Throw();
            }
            yield return null;
        }
        
        public static IEnumerator AsIEnumerator(this Task task, 
            Action onError = null)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }
            if (task.IsFaulted)
            {
                onError?.Invoke();
             
                ExceptionDispatchInfo.
                    Capture(task.Exception).
                    Throw();
            }
            yield return null;
        }
    }
}