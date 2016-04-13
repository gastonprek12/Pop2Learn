using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class HealEvent : InputEvent
    {

        public HealEvent()
            : base("Heal Event")
        {

        }

        public override void ExecuteAction()
        {
            ((HealInputInterface)(receiverObject)).ExecuteHeal();
        }
    }
}