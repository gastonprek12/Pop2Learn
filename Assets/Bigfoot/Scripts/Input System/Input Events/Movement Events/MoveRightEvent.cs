using System.Collections;

namespace Bigfoot
{
    public class MoveRightEvent : InputEvent
    {

        public MoveRightEvent()
            : base("Move Right Event")
        {

        }

        public override void ExecuteAction()
        {
            ((MovementInputInterface)receiverObject).ExecuteMoveRight();
        }

    }

}
