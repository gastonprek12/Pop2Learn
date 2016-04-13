using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class ShieldEvent : InputEvent
    {

        public ShieldEvent()
            : base("Shield Event")
        {

        }

        public override void ExecuteAction()
        {
            ((ShieldInputInterface)(receiverObject)).ExecuteShield();
        }
    }
}