using System.Collections;

namespace Bigfoot
{
    public interface GameInputInterface : InputInterface
    {
        void ExecutePause();

        void ExecuteGameOver();

        void ExecuteRestart();
    }
}