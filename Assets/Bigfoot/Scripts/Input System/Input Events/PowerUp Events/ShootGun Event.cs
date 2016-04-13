using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class ShootGunEvent : InputEvent
    {

        public ShootGunEvent()
            : base("Shoot Gun Event")
        {

        }

        public override void ExecuteAction()
        {
            ((GunInputInterface)(receiverObject)).ExecuteShootGun();
        }
    }
}