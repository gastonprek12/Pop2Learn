using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class InputController : MonoBehaviour
    {

        Ray ray;

        RaycastHit hit;

        ClickState clickState;

        Vector3 lastPosition;

        Vector3 inputPosition;

        public float MovingHorizontalMinTime = 0.15f;

        public float RotatingMinTime = 0.15f;
#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8
        bool hasMoved = false;
#else
    bool isRotating = false;
#endif
        bool isMovingHorizontal = false;


        BFEventsInput events;

        enum ClickState
        {
            BEGIN,
            MOVE,
            END,
            DEFAULT
        }

        // Use this for initialization
        void Start()
        {
            events = BFEventsInput.Instance;
        }

        // Update is called once per frame
        void Update()
        {
#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8
            if (Input.touchCount <= 0)
                return;
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                clickState = ClickState.BEGIN;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                clickState = ClickState.MOVE;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                clickState = ClickState.END;
            }
            else
            {
                clickState = ClickState.DEFAULT;
            }

            inputPosition = touch.position;

            if (clickState != ClickState.DEFAULT)
            {
                //Change ray accordingly
                ray = Camera.main.ScreenPointToRay(inputPosition);
                if (Physics.Raycast(ray, out hit, 100))
                {
                    GameObject gameObjectHit = hit.collider.gameObject;
                    if (gameObjectHit == null)
                        return;

                    if (clickState == ClickState.BEGIN)
                        lastPosition = inputPosition;

                    if (clickState == ClickState.MOVE && !hasMoved)
                    {
                        if (!isMovingHorizontal)
                        {
                            isMovingHorizontal = true;
                            Vector3 delta = inputPosition - lastPosition;
                            if (delta.x < 0)
                                events.MoveLeft();
                            else if (delta.x > 0)
                                events.MoveRight();
                            lastPosition = inputPosition;
                            Invoke("MovingHorizontalDelay", MovingHorizontalMinTime);
                        }
                    }

                    if (clickState == ClickState.END)
                    {
                        if (!hasMoved)
                        {
                            Debug.Log("Clicked ");
                        }
                        else
                        {
                            hasMoved = false;
                        }
                    }
                }
            }
#else
				float x = Input.GetAxis("Horizontal");
				if(x != 0 && !isMovingHorizontal)
				{
					isMovingHorizontal = true;
					if(x > 0)
						events.MoveRight();
					else
						events.MoveLeft();
					Invoke("MovingHorizontalDelay", MovingHorizontalMinTime);
				}

				float y = Input.GetAxis("Vertical");
				if(y > 0 && !isRotating)
				{
					isRotating = true;
					events.MoveUp();
					Invoke("RotatingDelay", RotatingMinTime);
				}
				if(y < 0)
					events.MoveDown();
				
#endif
        }

        void MovingHorizontalDelay()
        {
            isMovingHorizontal = false;
        }

        void RotatingDelay()
        {
#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8
#else
		isRotating = false;
#endif
        }
    }
}