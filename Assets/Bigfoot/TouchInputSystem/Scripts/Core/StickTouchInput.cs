using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class StickTouchInput : TouchInput
    {
        public Vector3 Direction;

        public StickTouchInput(TouchInput t, Vector3 dir)
        {
            Key = t.Key;
            Direction = dir;
        }
    }
}