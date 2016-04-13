using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class Follower : MonoBehaviour
    {

        public bool FollowOnX;
        public bool FollowOnY;
        public bool FollowOnZ;
        public GameObject ObjectToFollow;
        public Vector3 Offset;
        public bool ApplyMinValues = false;
        public Vector3 MinValues;
        public bool ApplyMaxValues = false;
        public Vector3 MaxValues;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Follow();
        }

        public void Follow()
        {
            float x;
            float y;
            float z;

            if (FollowOnX)
            {
                x = ObjectToFollow.transform.position.x;
                if (x < this.MinValues.x && ApplyMinValues)
                    x = this.MinValues.x;
                else if (x > this.MaxValues.x && ApplyMaxValues)
                    x = this.MaxValues.x;
            }
            else
            {
                x = gameObject.transform.position.x;
            }

            if (FollowOnY)
            {
                y = ObjectToFollow.transform.position.y;
                if (y < this.MinValues.y && ApplyMinValues)
                    y = this.MinValues.y;
                else if (y > this.MaxValues.y && ApplyMaxValues)
                    y = this.MaxValues.y;
            }
            else
            {
                y = gameObject.transform.position.y;
            }

            if (FollowOnZ)
            {
                z = ObjectToFollow.transform.position.z;
                if (z < this.MinValues.z && ApplyMinValues)
                    z = this.MinValues.z;
                else if (z > this.MaxValues.z && ApplyMaxValues)
                    z = this.MaxValues.z;
            }
            else
            {
                z = gameObject.transform.position.z;
            }

            gameObject.transform.position = new Vector3(x + Offset.x, y + Offset.y, z + Offset.z);
        }
    }

}