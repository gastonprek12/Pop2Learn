using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bigfoot
{
    public class ProbabilityHelper
    {

        private List<ProbabilityObject> objects;

        public void Reset()
        {
            this.objects = new List<ProbabilityObject>();
        }

        public ProbabilityHelper()
        {
            this.objects = new List<ProbabilityObject>();
        }

        public ProbabilityHelper(IEnumerable<ProbabilityObject> objects)
        {
            this.objects = objects.ToList();
        }

        public void AddObject(ProbabilityObject obj)
        {
            objects.Add(obj);
        }

        // Returns a ProbabilityObject, based on their associated weights
        public ProbabilityObject Choose()
        {
            float sumOfProbs = objects.Select(x => x.Weight).Sum();
            float randomFloat = Random.Range(0.0f, sumOfProbs);
            float partialSum = 0;
            foreach (ProbabilityObject obj in objects)
            {
                if (randomFloat <= obj.Weight + partialSum)
                    return obj;
                else
                    partialSum += obj.Weight;
            }
            return null;
        }

        // Returns if something is true or not, based on the probability of the parameter
        public static bool GetProbability(float probability)
        {
            float randomFloat = Random.Range(0.0f, 100);
            if (randomFloat <= probability)
                return true;
            else return false;
        }

    }
}