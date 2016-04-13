using System.Collections;

namespace Bigfoot
{
    public interface MovementInputInterface : InputInterface
    {
        void ExecuteMoveLeft();

        void ExecuteMoveRight();

        void ExecuteJump();
    }
}