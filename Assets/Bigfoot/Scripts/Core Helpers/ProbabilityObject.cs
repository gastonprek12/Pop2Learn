using System.Collections;
using UnityEngine;

namespace Bigfoot
{

    [System.Serializable]
    public class ProbabilityObject
    {

        // The object that is associated to the probability
        public GameObject WeightedObject;

        // The probability of the GameObject
        [Range(0, 1)]
        public float Weight;


        public ProbabilityObject(GameObject go, float w)
        {
            this.WeightedObject = go;
            this.Weight = w;
        }
    }
}