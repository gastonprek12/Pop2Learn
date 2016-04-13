using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class RestartEvent : InputEvent
    {

        public RestartEvent()
            : base("Restart Event")
        {

        }

        public override void ExecuteAction()
        {
            ((GameInputInterface)receiverObject).ExecuteRestart();
        }
    }
}