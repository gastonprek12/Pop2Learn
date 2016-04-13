using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class SwipeTouchInput : TouchInput 
    {
        public Vector2 SwipeDelta;

        public SwipeTouchInput(TouchInput t, Vector2 swipeDelta, Vector2 p)
        {
            SwipeDelta = swipeDelta;
            Key = t.Key;
			Position = p;
		
        }
    }
}