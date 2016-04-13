using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class CircularAnimation : MonoBehaviour
    {

        public Transform CenterOfRotation;
        public GameObject[] ObjectsToRotate;
        public float DegreesPerSecond = 60;
        /// <summary>
        /// 1 Clockwise -1 CounterClockwise
        /// </summary>
        public int ClockDirection;

        // Use this for initialization
        void Start()
        {
            StartCoroutine(Rotate(true));
        }

        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator Rotate(bool clockwise)
        {
            while (true)
            {
                foreach (GameObject go in ObjectsToRotate)
                {
                    go.transform.RotateAround(CenterOfRotation.position, new Vector3(0, 0, 1), DegreesPerSecond * Time.deltaTime * ClockDirection);
                    go.transform.rotation = Quaternion.identity;
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}