using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class SmartTouch : MonoBehaviour {

        [Header("Triggers")]
        public bool OnPressed;
        public bool OnReleased;
        public bool OnClicked;
        public bool Stick;
        public bool SwipeScreen;
		public bool Tilt;
        [Header("Touch Info")]
        public TouchInput BFTouchInput;
        UI2DSprite _sp;
        Vector2 buttonPosition, buttonPositionInitial;
        bool IsPressed = false;
        Vector3 _stickStartingPosition;
        Camera _nguiCam;
        void Start()
        {
            _sp = GetComponent<UI2DSprite>();
            _stickStartingPosition = transform.position;
            _nguiCam = GetComponentInParent<Camera>();
        }

        public void Update()
        {
            if(Stick)
            {
                if(IsPressed)
                {
                    Vector3 dir = transform.position - _stickStartingPosition;
                    dir = Vector3.Normalize(dir);
                    BFEventsTouchInput.StickMoved(new StickTouchInput(BFTouchInput,dir));
                }
                else
                {
                    BFEventsTouchInput.StickMoved(new StickTouchInput(BFTouchInput, Vector3.zero));
                    transform.localPosition = Vector3.zero;
                }
            }
            if(SwipeScreen)
            {
                if(IsPressed)
                {
#if UNITY_EDITOR
                    //If it's moving
                    if (Input.GetMouseButton(0))
                    {
                        //Get the touched object
                        var ray = _nguiCam.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit,100))
                        {
                            SmartTouch touchedObject = hit.collider.GetComponent<SmartTouch>();
                            //If that object it's this one, then the user is draggin in our swipe screen area
                            if (touchedObject != null && touchedObject == this)
                            {
                                BFEventsTouchInput.Swipe(new SwipeTouchInput(BFTouchInput, Vector2.zero, Vector2.zero));                                
                            }
                        }
                    } 
#else
                    //For each touch
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        var t = Input.GetTouch(i);
                        //If it's moving
                        if (t.phase == TouchPhase.Moved)
                        {
                            //Fix for controls being upside down. WTF ricky right?
                            var upsideDownTouch = new Vector2(Input.touches[i].position.x, Screen.height - Input.touches[i].position.y);
                            //Get the touched object
                            var ray = _nguiCam.ScreenPointToRay(t.position);
                            RaycastHit hit;
                            if(Physics.Raycast(ray, out hit,100))
                            {
                                SmartTouch touchedObject = hit.collider.GetComponent<SmartTouch>();
                                //If that object it's this one, then the user is draggin in our swipe screen area
                                if (touchedObject != null && touchedObject == this)
                                {
                                    BFEventsTouchInput.Swipe(new SwipeTouchInput(BFTouchInput, t.deltaPosition, t.position));
                                }
                            }
                        }
                    }    
#endif
                }
                else
                {

					// BFEventsTouchInput.Swipe(new SwipeTouchInput(BFTouchInput, Vector2.zero, Vector2.zero));
				}
			}
			if (Tilt) {
#if !UNITY_EDITOR
				BFEventsTouchInput.Tilt(new TiltInput(Input.acceleration));
#endif
			}
        }

        void OnPress(bool pressed)
        {
		#if !UNITY_EDITOR
            if (pressed)
            {
                if (OnPressed && Input.touchCount > 0) {
					BFTouchInput.Position = Input.touches[0].position;
                    BFEventsTouchInput.Press(BFTouchInput);
				}
                IsPressed = true;
            }
            else
            {
                if (IsPressed && OnReleased) {
					BFTouchInput.Position = Input.touches[0].position;
                    BFEventsTouchInput.Release(BFTouchInput);
				}
                IsPressed = false;
            }
	#endif
        }

        void OnClick()
        {
            if (OnClicked)
                BFEventsTouchInput.Click(BFTouchInput);
        }
    
    }
}
