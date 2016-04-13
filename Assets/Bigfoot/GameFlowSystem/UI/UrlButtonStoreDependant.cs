using UnityEngine;
using System.Collections;

namespace Bigfoot
{
	public class UrlButtonStoreDependant : MonoBehaviour {

		public string AndroidURL;

		public string IosURL;

		public string WP8URL;

		void OnClick()
		{
			#if UNITY_ANDROID
				Application.OpenURL(AndroidURL);
			#elif UNITY_IPHONE
				Application.OpenURL(IosURL);
			#elif UNITY_WP8
				Application.OpenURL(WP8URL);
			#endif
		}
	}
}