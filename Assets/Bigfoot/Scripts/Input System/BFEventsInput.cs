using UnityEngine;
using System.Collections;

namespace Bigfoot
{

    public class BFEventsInput : Singleton<BFEventsInput>
    {

        /// <summary>
        /// The on move left event.
        /// </summary>
        public static System.Action OnMoveLeftEvent;

        public void MoveLeft()
        {
            if (OnMoveLeftEvent != null)
            {
                OnMoveLeftEvent();
            }
        }

        /// <summary>
        /// The on move right event.
        /// </summary>
        public static System.Action OnMoveRightEvent;

        public void MoveRight()
        {
            if (OnMoveRightEvent != null)
            {
                OnMoveRightEvent();
            }
        }

        /// <summary>
        /// The on move up event.
        /// </summary>
        public static System.Action OnMoveUpEvent;

        public void MoveUp()
        {
            if (OnMoveUpEvent != null)
            {
                OnMoveUpEvent();
            }
        }

        /// <summary>
        /// The on move up event.
        /// </summary>
        public static System.Action OnMoveDownEvent;

        public void MoveDown()
        {
            if (OnMoveDownEvent != null)
            {
                OnMoveDownEvent();
            }
        }
    }
}