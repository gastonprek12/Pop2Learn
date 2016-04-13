using UnityEngine;
using System.Collections;

namespace Bigfoot
{
	public class DesignEvent : MonoBehaviour {

		public string EventName;

		public float Value = 0f;

		public bool SendOnStart;
		
		public bool SendOnEnable;
		
		public bool SendOnDisable;
		
		public bool SendOnClick;

		void Start()
		{
			if(SendOnStart)
				SendEvent ();
		}

		void OnEnable()
		{
			if(SendOnEnable)
				SendEvent ();
		}

		void OnDisable()
		{
			if(SendOnDisable)
				SendEvent ();
		}

		void OnClick()
		{
			if(SendOnClick)
				SendEvent ();
		}

		public void SendEvent()
		{
			#if GAME_ANALYTICS
					if(Value == 0f)
						GA.API.Design.NewEvent (EventName);
					else
						GA.API.Design.NewEvent(EventName, Value);
			#else
				Debug.LogError("Put the GAME_ANALYTICS compiler flag for this to work");
			#endif
		}
	}
}
