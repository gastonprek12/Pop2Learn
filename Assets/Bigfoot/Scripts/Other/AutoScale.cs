using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    [ExecuteInEditMode]
    public class AutoScale : MonoBehaviour
    {

        public float FullScaleYValue = -6.405651f;
        public float PercentagePerOneYUnit = 0.05f;
        public bool ApplyOnUpdate = false;
        [HideInInspector]
        public float ScaleFactor;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (ApplyOnUpdate)
                ApplyScale();
        }

        public void ApplyScale()
        {
            transform.localScale = Vector3.one;
            if (transform.position.y >= FullScaleYValue)
                ScaleFactor = 1 - (PercentagePerOneYUnit * Mathf.Abs(FullScaleYValue - transform.position.y));
            else
                ScaleFactor = 1;

            transform.localScale = new Vector3(transform.localScale.x * ScaleFactor, transform.localScale.y * ScaleFactor, transform.localScale.z * ScaleFactor);
            GetComponent<Renderer>().sortingOrder = (int)(transform.position.y * -10);
        }
    }
}