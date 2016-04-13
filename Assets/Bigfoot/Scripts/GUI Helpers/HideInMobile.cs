using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class HideInMobile : MonoBehaviour
    {

        public bool AlsoDestroy = false;

        // Use this for initialization
        void Start()
        {
#if UNITY_WP8 || UNITY_ANDROID || UNITY_IOS
            if (AlsoDestroy)
                Destroy(gameObject);
            else
                gameObject.SetActive(false);
#endif
        }
    }
}