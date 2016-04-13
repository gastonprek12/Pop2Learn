using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Bigfoot
{
	[System.Serializable]
	public class LocalUser
	{
		public int ID;

		public string Name;

		public int Level;

		public int Xp;

		public List<BF_Item> Items;

		public string FacebookID;
	}
}