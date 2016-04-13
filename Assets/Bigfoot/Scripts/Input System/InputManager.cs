using UnityEngine;
using System;
using System.Collections;

namespace Bigfoot
{

    public class InputManager : Singleton<InputManager>
    {

        public static Action<InputEvent> onMovementInputEvent;

        protected GeneralEventSystem generalEventSystem;

        void Start()
        {
            // You can use a plugin for touch inputs like EasyTouch for example. And listen to its events to handle movement.
            generalEventSystem = GeneralEventSystem.Instance;
        }

        void Update()
        {

            //Game Related Events should not trigger events from the input manager. It should warn the General Event Manager and it will trigger his own event. DON'T BREAK CHAIN
            /*if (Input.GetButtonDown("Pause"))
            {
                generalEventSystem.PauseGame();
            }
            else if (Input.GetButtonDown("Quit"))
            {
                generalEventSystem.QuitGame();
            }
            else if (Input.GetButtonDown("Restart"))
            {
                generalEventSystem.RestartGame();
            }

            //User input related events, should trigger events from here. Like moving, jumping or punching

            // Check the horizontal movement
            float horizontalInput = Input.GetAxis("Horizontal");
		
            if (horizontalInput < 0.0 && onMovementInputEvent != null) 
            {
                if(onMovementInputEvent != null)
                {
                    onMovementInputEvent(new MoveLeftEvent());	
                }
            }
            else if (horizontalInput > 0.0 && onMovementInputEvent != null) 
            {
                if(onMovementInputEvent != null)
                {
                    onMovementInputEvent(new MoveRightEvent());
                }
            }	*/

            //Same can be done with jump, duck, fire events, etc

            CustomUpdate();
        }

        public void MoveLeft()
        {
            if (onMovementInputEvent != null)
            {
                onMovementInputEvent(new MoveLeftEvent());
            }
        }

        public void MoveRight()
        {
            if (onMovementInputEvent != null)
            {
                onMovementInputEvent(new MoveRightEvent());
            }
        }

        public virtual void CustomUpdate()
        {
        }

        void Destroy()
        {

        }

    }
}