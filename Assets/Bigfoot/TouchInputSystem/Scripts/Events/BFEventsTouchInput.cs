using System;

namespace Bigfoot
{
    public static class BFEventsTouchInput
    {
        public static Action<TouchInput> OnPress;
        public static Action<TouchInput> OnRelease;
        public static Action<TouchInput> OnClick;
        public static Action<StickTouchInput> OnStickMoved;
        public static Action<SwipeTouchInput> OnSwipe;
		public static Action<TiltInput> OnTilt;

        public static void Press(TouchInput t)
        {
            if (OnPress != null)
                OnPress(t);
        }
        public static void Release(TouchInput t)
        {
            if (OnRelease != null)
                OnRelease(t);
        }
        public static void Click(TouchInput t)
        {
            if (OnClick != null)
                OnClick(t);
        }

        public static void StickMoved(StickTouchInput t)
        {
            if (OnStickMoved != null)
                OnStickMoved(t);
        }

        public static void Swipe(SwipeTouchInput t)
        {
            if (OnSwipe != null)
                OnSwipe(t);
        }

		public static void Tilt(TiltInput t) {
			if (OnTilt != null) 
				OnTilt(t);
		}
    }
}

