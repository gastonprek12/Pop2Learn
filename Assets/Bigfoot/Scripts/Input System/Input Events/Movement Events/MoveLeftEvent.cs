using System.Collections;

namespace Bigfoot
{
    public class MoveLeftEvent : InputEvent
    {

        public MoveLeftEvent()
            : base("Move Left Event")
        {

        }

        public override void ExecuteAction()
        {
            ((MovementInputInterface)receiverObject).ExecuteMoveLeft();
        }

    }
}