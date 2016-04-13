using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class HideInWeb : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
#if UNITY_WEBPLAYER
		gameObject.SetActive(false);
#endif
        }

    }

}