using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class JumpEvent : InputEvent
    {

        public JumpEvent()
            : base("Jump Event")
        {

        }

        public override void ExecuteAction()
        {
            ((MovementInputInterface)(receiverObject)).ExecuteJump();
        }
    }
}