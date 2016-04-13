using System.Collections;

namespace Bigfoot
{
    public class PauseEvent : InputEvent
    {

        public PauseEvent()
            : base("Pause Event")
        {

        }

        public override void ExecuteAction()
        {
            ((GameInputInterface)receiverObject).ExecutePause();
        }

    }

}