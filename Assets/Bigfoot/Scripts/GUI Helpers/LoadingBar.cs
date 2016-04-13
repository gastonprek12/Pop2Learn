using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class LoadingBar : MonoBehaviour
    {

        public GameObject Background;
        public GameObject Foreground;

        public bool SimulateLoading;
        public float SimulationDuration = 10f;

        private float xScale;

        void Awake()
        {
            xScale = Background.transform.localScale.x;
        }

        // Use this for initialization
        void Start()
        {

            if (SimulateLoading)
                StartCoroutine(SimulateLoadingForSeconds(SimulationDuration));
        }

        private IEnumerator SimulateLoadingForSeconds(float secs)
        {
            float simDur = SimulationDuration;
            while (simDur > 0)
            {
                simDur -= Time.deltaTime;
                SetProgress(simDur / SimulationDuration);
                yield return new WaitForEndOfFrame();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetProgress(float progress)
        {
            Background.transform.localScale = new Vector3((1 - progress) * xScale, transform.localScale.y, 1);
        }
    }
}