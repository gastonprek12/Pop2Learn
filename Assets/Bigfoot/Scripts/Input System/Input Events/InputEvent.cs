using System.Collections;

namespace Bigfoot
{

    public abstract class InputEvent
    {
        // Description of what the imput does
        private string description;
        protected InputInterface receiverObject;

        public InputEvent(string description)
        {
            this.description = description;
        }

        // When event is called, it executes this method.
        public abstract void ExecuteAction();

        public void SetInputInterface(InputInterface receiverObject)
        {
            this.receiverObject = receiverObject;
        }

        public string GetDescription()
        {
            return description;
        }
    }
}
