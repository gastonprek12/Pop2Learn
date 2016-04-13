using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public static class MonoBehaviourExt
    {
        public static Coroutine<T> StartCoroutine<T>(this MonoBehaviour obj, IEnumerator coroutine)
        {
            Coroutine<T> coroutineObject = new Coroutine<T>();
            coroutineObject.coroutine = obj.StartCoroutine(coroutineObject.InternalRoutine(coroutine));
            return coroutineObject;
        }
    }

    public class Coroutine<T>
    {
        public T Value
        {
            get
            {
                if (e != null)
                {
                    throw e;
                }
                return returnVal;
            }
        }

        private T returnVal;
        public Coroutine coroutine;
        public System.Exception e;
        public bool isCancelled = false;

        public void Cancel()
        {
            isCancelled = true;
        }

        public IEnumerator InternalRoutine(IEnumerator coroutine)
        {
            while (true)
            {
                if (isCancelled)
                {
                    e = new CoroutineCancelledException();
                    yield break;
                }

                try
                {
                    if (!coroutine.MoveNext())
                    {
                        yield break;
                    }
                }
                catch (System.Exception e)
                {
                    this.e = e;
                    yield break;
                }

                object yielded = coroutine.Current;

                if (yielded != null && yielded.GetType() == typeof(T))
                {
                    returnVal = (T)yielded;
                    yield break;
                }
                else
                {
                    yield return coroutine.Current;
                }
            }
        }
    }

    public class CoroutineCancelledException : System.Exception
    {
        public CoroutineCancelledException()
            : base("Coroutine was cancelled")
        {

        }
    }
}

/// HOW TO USE!!!! ///
/// 
/*

IEnumerator Start()
{
    var routine = StartCoroutine<int>(SumValues(2,2)); //Store the routine so you can cancel it if you want
 *  yield return routine.coroutine; //Wait until it's finished
 *  Debug.Log(routine.Value); //Print the result that stored in the routine Value property
}
 * 
 * 
IEnumerator SumValues(int a, int b)
{
    yield return new WaitForSeconds(2);// do whatever you want to calculate the number etc
    yield return a + b; //If you return the value that our coroutine is expecting, it will get stored in the Value property, so by doing this we are setting it's value
}
*/
////