using UnityEngine;

namespace Bigfoot
{
    [ExecuteInEditMode]
    public class IsometricSpriteRenderer : MonoBehaviour
    {
        public bool ExecuteInUpdate = false;

        void Start()
        {
            GetComponent<Renderer>().sortingOrder = (int)(transform.position.y * -10);
        }

        void Update()
        {
            if (ExecuteInUpdate)
            {
                GetComponent<Renderer>().sortingOrder = (int)(transform.position.y * -10);
            }
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