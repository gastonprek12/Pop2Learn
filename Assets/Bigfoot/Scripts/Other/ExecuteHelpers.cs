using UnityEngine;
using System.Collections;
using System;

namespace Bigfoot
{
    public class ExecuteHelpers : MonoBehaviour
    {

        [ContextMenu("Delete Player Prefs")]
        void DeletePlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        [ContextMenu("Bring to Front")]
        void BringToFront()
        {
            GetComponent<Renderer>().sortingOrder = 200;
        }

        [ContextMenu("Bring to Back")]
        void BringToBack()
        {
            GetComponent<Renderer>().sortingOrder = -200;
        }

    }
}