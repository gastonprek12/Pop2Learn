using UnityEngine;
using System.Collections;

namespace Bigfoot
{
	public class IosSerializeHack : MonoBehaviour {

		void Awake()
		{
			#if UNITY_IOS || UNITY_IPHONE
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
			} 
			#endif
		}
	}
}