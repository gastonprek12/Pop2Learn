using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    [ExecuteInEditMode()]
    public class SetRenderingLayer : MonoBehaviour
    {
        public string sortingLayer;
        public int sortingOrder;

        void Start()
        {
            this.GetComponent<Renderer>().sortingLayerName = sortingLayer;
            this.GetComponent<Renderer>().sortingOrder = sortingOrder;
        }
    }
}