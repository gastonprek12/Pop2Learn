using UnityEngine;
using System.Collections;

namespace Bigfoot
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
		
		private static T s_Instance;
		
		public static T Instance
		{ 
			get {
				if (!s_Instance)
				{
					new GameObject().AddComponent<T>();
				}
				return s_Instance;
			}
		}
		
		public virtual void Awake ()
		{
			if (s_Instance != null)
			{
				//Debug.LogError ("There's already an instance of MyManager. Is it more than once in the scene?", this);
			}
			
			s_Instance = (T)(object)this;
		}
	}
}