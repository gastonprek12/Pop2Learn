using UnityEngine;
using System.Collections;
using System;

namespace Bigfoot
{
	public static class BFEventsUser {

		public static Action<BFKItemKeys, int> OnItemAmountChanged;
		
		public static void ItemAmountChanged(BFKItemKeys key, int amount)
		{
			if (OnItemAmountChanged != null)
				OnItemAmountChanged(key, amount);
		}

		public static Action OnUserOutOfCurrency;
		
		public static void UserOutOfCurrency()
		{
			if (OnUserOutOfCurrency != null)
				OnUserOutOfCurrency();
		}
	}
}