using UnityEngine;
using System.Collections;

namespace Bigfoot
{

    public class ScreenShake : Singleton<ScreenShake>
    {

        private float shake;
        private float shakeAmount = 0;
        private Coroutine<bool> _shakeRoutine;
        private bool _previousShakeEnded = false;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShakeCamera(Shake shake)
        {
            /*if (PlayerPrefs.GetInt(BFKPreferences.PREF_SCREENSHAKE_ON, 1) == 1)
            {
                if (!_previousShakeEnded && this.shakeAmount <= shake.shakeAmount)
                {
                    if (_shakeRoutine != null)
                        _shakeRoutine.Cancel();
                    //_shakeRoutine = StartCoroutine<bool>(ShakeCameraForSeconds(shake));
                }
                else if (_previousShakeEnded)
                {
                    _shakeRoutine = StartCoroutine<bool>(ShakeCameraForSeconds(shake));
                }
            }*/
        }

        public void StopShake()
        {
            _shakeRoutine.Cancel();
        }

        private IEnumerator ShakeCameraForSeconds(Shake shakeAnim)
        {
            shake = shakeAnim.ShakeDuration;
            shakeAmount = shakeAnim.shakeAmount;
            while (shake > 0)
            {
                if (Time.timeScale != 0)
                {
                    Vector3 v = Random.insideUnitSphere * shakeAmount;
                    transform.localPosition = new Vector3(v.x, v.y, -10);
                    shake -= Time.deltaTime;
                }

                yield return new WaitForEndOfFrame();
            }
            _previousShakeEnded = true;
        }
    }
}