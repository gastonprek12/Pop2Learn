using System;

namespace Bigfoot
{
	public static class BFEventsStore {

		public static Action<Offer> OnTryPurchaseItem;
		public static Action<Item> OnItemPurchaseSucceed;
		public static Action<Item> OnItemPurchaseFailed;
		public static Action<Item> OnItemPurchaseCancelled;
		public static Action OnTransactionsRestored;

		public static void TryPurchase(Offer o)
		{
			if (OnTryPurchaseItem != null) 
			{
				OnTryPurchaseItem(o);
			}
		}

		public static void ItemPurchaseSucceed(Item item)
		{
			if (OnItemPurchaseSucceed != null) 
			{
				OnItemPurchaseSucceed(item);
			}
		}

		public static void ItemPurchaseFailed(Item item)
		{
			if (OnItemPurchaseFailed != null) 
			{
				OnItemPurchaseFailed(item);
			}
		}

		public static void ItemPurchaseCancelled(Item item)
		{
			if (OnItemPurchaseCancelled != null) 
			{
				OnItemPurchaseCancelled(item);
			}
		}

		public static void TransactionsRestored()
		{
			if(OnTransactionsRestored != null)
				OnTransactionsRestored();
		}
	}
}