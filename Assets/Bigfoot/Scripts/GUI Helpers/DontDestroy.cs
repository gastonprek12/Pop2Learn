using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class DontDestroy : MonoBehaviour
    {

        // Use this for initialization
        void Awake()
        {
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}