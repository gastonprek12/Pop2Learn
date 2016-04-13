using System.Collections;

namespace Bigfoot
{
    public class GameOverEvent : InputEvent
    {

        public GameOverEvent()
            : base("Game Over Event")
        {

        }

        public override void ExecuteAction()
        {
            ((GameInputInterface)receiverObject).ExecuteGameOver();
        }

    }
}