#if SOOMLA
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;

namespace Bigfoot 
{
	public class BFSoomlaStore: IStoreAssets {

		private Offer[] offers;

		private bool InitializeController;

		public BFSoomlaStore(Offer[] offers, bool InitController)
		{
            if (offers == null)
            {
                Debug.Log("You are intializing the store with no offers");
            }
			this.offers = offers;
			InitializeController = InitController;
			SetUpCurrenciesAndItems();
		}

		// We should use ~SoomlaStore :P 
		public void Destroy()
		{
			#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
			//StoreEvents.OnMarketPurchase -= onMarketPurchase;
			//StoreEvents.OnMarketPurchaseCancelled -= onMarketCancelled;
			#endif
			
			#if UNITY_ANDROID && !UNITY_EDITOR
			// Stop Iab Service
			//StoreController.StopIabServiceInBg();
			#endif
		}

		#region Init
		
	#if UNITY_ANDROID || UNITY_IOS
		
		public void StartMobile(){

			if(InitializeController)
			{
				// Initialize Store
				SoomlaStore.Initialize(this);
			}

			// Subscribe to IAP Events
			StoreEvents.OnMarketPurchase += onMarketPurchase;
			StoreEvents.OnMarketPurchaseCancelled += onMarketCancelled;
			StoreEvents.OnItemPurchased += OnItemPurchased;
			StoreEvents.OnRestoreTransactionsFinished += OnRestoreFinished;

			/*if(PlayerPrefs.GetInt("FirstTimePlayed",0) == 0)
				{
					StoreInventory.GiveItem ("currency_candy", 1);
					_localInfo.AmountCurrency += 1;
					PlayerPrefs.SetInt("FirstTimePlayed",1);
				}*/
		}
		
		public void StartAndroid(){
			#if UNITY_ANDROID && !UNITY_EDITOR
			// Start Iab Service
			SoomlaStore.StartIabServiceInBg();
			#endif
		}
		
		public void StartIOS(){
			#if UNITY_IPHONE && !UNITY_EDITOR
			
			#endif
		}
	#endif

		#endregion

		#region Currency, packages, etc

		private void SetUpCurrenciesAndItems()
		{
			//Currency setup (Money, Coins, etc)
			SetUpCurrencies();
			//Lives, bears, consumables in general
			SetUpPurchasableItems();
			//TODO: Categories
		}

		private VirtualCurrency[] virtualCurrencies;
		private VirtualCurrencyPack[] virtualCurrencyPacks;
		private VirtualGood[] virtualGoods;

		private void SetUpCurrencies()
		{
			virtualCurrencies = new VirtualCurrency[System.Enum.GetValues(typeof(BFKCurrencyKeys)).Length];
			int i = 0;
			foreach(BFKCurrencyKeys key in System.Enum.GetValues(typeof(BFKCurrencyKeys)))
			{
				virtualCurrencies[i] = new VirtualCurrency(
					key.ToString(),
					key.ToString(),
					key.ToString()
					);
				i++;
			}
		}

		public VirtualCurrency[] GetCurrencies() {
			return virtualCurrencies;
		}

		private void SetUpPurchasableItems()
		{
			List<VirtualCurrencyPack> packs = new List<VirtualCurrencyPack>();
			List<VirtualGood> goods = new List<VirtualGood>();

			//First we set up every simple good
			#region Single Good Set Up
			
			//First, for every possible type of good, we have to create a virtual single use good
			foreach(BFKItemKeys constant in System.Enum.GetValues(typeof(BFKItemKeys)))
			{
				VirtualGood newGood = new SingleUseVG(
					constant.ToString(),                    // name
					constant.ToString(),      // description
					constant.ToString(),      // internal item id of the good : gun
					new PurchaseWithMarket(constant.ToString(),0) // Won't be ueed to buy
					);
				goods.Add(newGood);
			}
			#endregion

			//For every offer
			foreach( Offer o in offers)
			{
				#region Currency Packs Set Up

				bool isCurrency = false;
				foreach(BFKCurrencyKeys currkey in System.Enum.GetValues(typeof(BFKCurrencyKeys)))
				{
					if(currkey.ToString() == o.ItemId.ToString())
					{
						isCurrency = true;
					}
				}
				//If the offer is a currency offer (ie user wants to buy coins)
				if(isCurrency)
				{
					//Then we start iterating on every type of paying method we have
					foreach( Value v in o.Costs)
					{
						//IF the user is paying with money
						if(v.Currency == BFKCurrencyKeys.MONEY)
						{
							// we create a virtualCurrencyPack that is going to be purchased with the market
							VirtualCurrencyPack newPack = new VirtualCurrencyPack(
								o.Name,                    // name
								o.Description,      // description
								o.StoreId.ToString(),                    // internal item id : coins_100
								o.Amount,                             // number of currencies in the pack : 100
								v.Currency.ToString(),         // the currency associated with this pack : COINS
								new PurchaseWithMarket(BFKConfig.GetBundlePrefix() + "." + o.StoreId.ToString(), v.Cost)// com.bigfoot.amy.coins_1 , 0.99
								);
							packs.Add(newPack);//Add it to the currency pack list
						}
						else
						{
							//if he is paying with other currency
							VirtualCurrencyPack newPack = new VirtualCurrencyPack(
								o.Name,                    // name
								o.Description,      // description
								o.StoreId.ToString() + "." + v.Currency.ToString(),  // internal item id  : coins_1000
								o.Amount,                             // number of currencies in the pack : 1000
								v.Currency.ToString(),         // the currency associated with this pack : diamonds
								new PurchaseWithVirtualItem(v.Currency.ToString(), (int)v.Cost)// diamonds , 10
								);
							
							packs.Add(newPack);//Add it to the currency pack list
						}
					}
				}
				#endregion
				#region Goods Set Up
				else if(IsNonConsumable(o))
				{
					foreach( Value v in o.Costs)
					{
						if(v.Currency == BFKCurrencyKeys.MONEY)
						{
                            LifetimeVG newNonConsumable = new LifetimeVG(
								o.Name,
								o.Description,
								o.StoreId.ToString(),
								new PurchaseWithMarket(BFKConfig.GetBundlePrefix() + "." + o.StoreId.ToString(), v.Cost)
								);

                            goods.Add(newNonConsumable);
						}
						else
						{
                            LifetimeVG newNonConsumable = new LifetimeVG(
								o.Name,
								o.Description,
								o.StoreId.ToString(),
								new PurchaseWithVirtualItem(v.Currency.ToString(), (int)v.Cost)
								);

                            goods.Add(newNonConsumable);                            
						}
					}
				}
				else
				{
					#region Good Packs Set Up

					foreach( Value v in o.Costs)
					{
						//If the user is paying with money
						if(v.Currency == BFKCurrencyKeys.MONEY)
						{
							VirtualGood newGood = new SingleUsePackVG(
								o.ItemId.ToString(),
								o.Amount,
								o.Name,                    // name
								o.Description,      // description
								o.StoreId.ToString(),      // item id
								new PurchaseWithMarket(BFKConfig.GetBundlePrefix() + "." + o.StoreId.ToString(), v.Cost)
								);
							goods.Add(newGood);
						}
						else
						{
							VirtualGood newGood = new SingleUsePackVG(
								o.ItemId.ToString(),
								o.Amount,
								o.Name,                    // name
								o.Description,      // description
								o.StoreId.ToString() + "." + v.Currency.ToString(),      // item id
								new PurchaseWithVirtualItem(v.Currency.ToString(), (int)v.Cost)
								);
							goods.Add(newGood);
						}
					}

					#endregion
				}
				#endregion
			}

			virtualCurrencyPacks = packs.ToArray();
			virtualGoods = goods.ToArray();
		}

		void OnRestoreFinished (bool obj)
		{
			BFEventsStore.TransactionsRestored ();
		}

		private bool IsNonConsumable(Offer o)
		{
            if(o.Type == BFKIAPType.NONCONSUMABLE)
			    return true;
            return false;
        }

		public VirtualCurrencyPack[] GetCurrencyPacks() {
			return virtualCurrencyPacks;
		}
		
		public int GetVersion() {
            return BFKConfig.StoreVersionNumber;
		}

		public VirtualGood[] GetGoods() {
			return virtualGoods;
		}

		public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory(
			"General", new List<string>(new string[] {})
			);
		
		public VirtualCategory[] GetCategories() {
			return new VirtualCategory[]{GENERAL_CATEGORY};
		}

		#endregion

		#region Purchase Handlers

		/// <summary>
		/// When the user wants to buy something, this method will be called.
		/// </summary>
		/// <param name="item">Item to buy.</param>
		/// <param name="currency">Currency used to buy.</param>
		/// <param name="onSuccess">On success delegate. This MUST be called after a sucessful purchase</param>
		/// <param name="OnFailure">On failure delegate. This has to be called if the purchase failed</param>
		public void Purchase (BFKStoreIds key, BFKCurrencyKeys currency)
		{
            Item item = new Item();
            item.Key = key.ToString();
			if(currency != BFKCurrencyKeys.MONEY)
			{
				item.Key += "." + currency;
			}

			Debug.Log("===================================");
			Debug.Log(string.Format("ABOUT TO PURCHASE {0} with currency : {1}",item.Key,currency));
			Debug.Log("===================================");

			StoreInventory.BuyItem (item.Key);
		}

        public void RestoreTransactions ()
        {
            SoomlaStore.RestoreTransactions();
        }

		public int GetItemBalance(BFKStoreIds key)
        {
			return StoreInventory.GetItemBalance (key.ToString ());
        }

        private void onMarketPurchase(PurchasableVirtualItem pvi, string payload, Dictionary<string, string> extras)
        {
			Offer o = GetOffer(pvi.ItemId);
			if(o == null)
			{
				Debug.Log(string.Format("The offer with key {0} doesn't exist. Please check that soomla has the same key as the store for this purchase", pvi.ItemId));
			}

			if (o != null) {

				bool isCurrency = false;
				foreach(BFKCurrencyKeys currkey in System.Enum.GetValues(typeof(BFKCurrencyKeys)))
				{
					if(currkey.ToString() == o.ItemId.ToString())
					{
						isCurrency = true;
					}
				}

				//If the offer is a currency offer (ie user wants to buy coins)
				if(isCurrency)
				{
					Debug.Log("===================================");
					Debug.Log(string.Format("Previous Currency amount : {0}", StoreInventory.GetItemBalance(o.ItemId.ToString())));
					Debug.Log("===================================");
					StoreInventory.GiveItem(o.ItemId.ToString(), o.Amount);
				}

				Debug.Log("===================================");
				Debug.Log(string.Format("New amount : {0}", StoreInventory.GetItemBalance(o.ItemId.ToString())));
				Debug.Log("===================================");

				Item i = new Item ();
				i.Key = o.ItemId.ToString ();
				i.Quantity = o.Amount;

				Bigfoot.BFEventsStore.ItemPurchaseSucceed(i);
			}
		}
		
		public void onMarketCancelled(PurchasableVirtualItem pvi) {
			Debug.Log("===================================");
			Debug.Log("CANCELLED!");
			Debug.Log("===================================");
			Offer o = GetOffer(pvi.ItemId);
			if(o == null)
			{
				Debug.Log(string.Format("The offer with key {0} doesn't exist. Please check that soomla has the same key as the store for this purchase", pvi.ItemId));
			}

			if (o != null) {
					Item i = new Item ();
					i.Key = o.ItemId.ToString ();
					i.Quantity = o.Amount;
				BFEventsStore.ItemPurchaseCancelled(i);
			}
		}

		public void OnItemPurchased(PurchasableVirtualItem pvi, string payload)
		{

			Debug.Log("XXXXXX pvi id : " + pvi.ItemId);
			int LastIndexOfDot = pvi.ItemId.LastIndexOf(".");
			if(LastIndexOfDot < 0)
			{
				//This was purchased with the market. Handle it here

				return;
			}
			else // Purchased with currency. Handle here
			{
				pvi.ItemId = pvi.ItemId.Substring(0, LastIndexOfDot);		
				Debug.Log("XXXXXX new pvi id : " + pvi.ItemId);
				Offer o = GetOffer(pvi.ItemId);
				if(o == null)
				{
					Debug.Log(string.Format("The offer with key {0} doesn't exist. Please check that soomla has the same key as the store for this purchase", pvi.ItemId));
				}
				if (o != null) {
					
					Debug.Log("===================================");
					Debug.Log(string.Format("Successfully bought {0}. Previous amount : {1}",o.ItemId, StoreInventory.GetItemBalance(o.ItemId.ToString())));
					Debug.Log("===================================");
					
					//StoreInventory.GiveItem(o.ItemId.ToString(), o.Amount);
					
					Debug.Log("===================================");
					Debug.Log(string.Format("New amount : {0}", StoreInventory.GetItemBalance(o.ItemId.ToString())));
					Debug.Log("===================================");
					
					Item i = new Item ();
					i.Key = o.ItemId.ToString ();
					i.Quantity = o.Amount;
					
					BFEventsStore.ItemPurchaseSucceed(i);
				}
			}
		}

		#endregion
		
		private Offer GetOffer(string key)
		{
			foreach (Offer o in offers)
			{
				if(o.StoreId.ToString() == key)
					return o;
			}
			
			return null;
		}

		public void ResetAll()
		{
			Debug.Log("****DELETING STORE DATA****");
			PlayerPrefs.DeleteAll();
			foreach(BFKItemKeys constant in System.Enum.GetValues(typeof(BFKItemKeys)))
			{
				StoreInventory.TakeItem(constant.ToString(), StoreInventory.GetItemBalance(constant.ToString())); 
				Debug.Log(string.Format("{0} balance : {1}",constant.ToString(), StoreInventory.GetItemBalance(constant.ToString())));
			}
			foreach(BFKCurrencyKeys key in System.Enum.GetValues(typeof(BFKCurrencyKeys)))
			{
				StoreInventory.TakeItem(key.ToString(), StoreInventory.GetItemBalance(key.ToString())); 
				Debug.Log(string.Format("{0} balance : {1}",key.ToString(), StoreInventory.GetItemBalance(key.ToString())));
			}
			Debug.Log("****STORE DATA DELETED****");
		}

		/*public void ItemCollected(Item item)
		{
			StoreInventory.GiveItem(item.Key, 
		}

		public void ItemUsed(Item item)
		{

		}*/
    }
}
#endif
