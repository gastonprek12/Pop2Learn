
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//using Facebook.MiniJSON;
using Bigfoot;
#if PRIME31
using Prime31;
using Prime31.WinPhoneStore;
#endif
#if SOOMLA
using Soomla;
#endif

namespace Bigfoot
{

    /// <summary>
    /// The store. This class will have the offers for the game and will act as intermediary between all of the other stores. Depending on the platform, different stores will be initialized or called. You can add or remove those anytime you want and your currencies, offers, etc won't be harmed
    /// </summary>
    public class Store : Singleton<Store>
    {

        public Offer[] Offers;

#if UNITY_EDITOR || UNITY_WEBPLAYER
        public bool DebugModeEnable = false;
        public bool ReturnSucessfulOnBuy = false;
        private Item _itemBeingBought;
#endif

        public bool ClearAllData = false;

#if SOOMLA
	private BFSoomlaStore soomlaStore;
	public bool InitializeStoreControler = true;
#endif

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(this);
            //Init soomla. We delegate android and ios buys to soomla, but we won't do anything else from here, just the initiation
#if SOOMLA
		soomlaStore = new BFSoomlaStore(Offers, InitializeStoreControler);	
		soomlaStore.StartMobile();
		soomlaStore.StartAndroid();
		if(ClearAllData)
			soomlaStore.ResetAll();
       
#endif
        }

        [ContextMenu("Clear all store data")]
        public void ClearAllStoreData()
        {
#if SOOMLA
		soomlaStore.ResetAll();
#endif
        }

        void OnDestroy()
        {
            //Destroy soomla with us
#if SOOMLA
		soomlaStore.Destroy();
#endif
        }

        /// <summary>
        /// When the user is trying to buy something, this method will be called. Depending on the platform, different stores will handle the operation.
        /// </summary>
        /// <param name="item">Item id.</param>
        /// <param name="currency">Currency used for buying</param>
        public void Purchase(BFKStoreIds key, BFKCurrencyKeys currency)
        {
            //First we get the offer that is trying to be bought
            Offer o = GetOffer(key.ToString());
            if (o != null)
            {
                BFEventsStore.TryPurchase(o);
            }
            //Then we have to check if the purchase should be handle by us (purchase with currency) or by the platform store

            //If he is buying with currency
            if (currency != BFKCurrencyKeys.MONEY)
            {
                Item purchasedItem = new Item();
                purchasedItem.Key = o.ItemId.ToString();
                purchasedItem.Quantity = o.Amount;
                BFEventsStore.ItemPurchaseSucceed(purchasedItem);
            }
            else //If he is buying with money, depending on the store, we will try to buy it differently
            {
#if SOOMLA && !UNITY_EDITOR
			// Soomla handles android and ios
			soomlaStore.Purchase(key , currency);
#elif UNITY_EDITOR

                //Testing values
                if (DebugModeEnable)
                {
                    Item it = new Item();
                    Debug.Log("Debug Mode enable: trying to buy item: " + key.ToString());
                    if (ReturnSucessfulOnBuy)
                    {
                        if (o == null)
                        {
                            Debug.Log(string.Format("The offer with key {0} doesn't exist. Please add it to the store Offers", key));
                        }
                        else
                        {
                            Debug.Log("Item Bought, sending success event");
                            it.Key = o.ItemId.ToString();
                            it.Quantity = o.Amount;
                            BFEventsStore.ItemPurchaseSucceed(it);
                        }
                    }
                    else
                    {
                        Debug.Log("Item buy failed on purpose for debug, sending failure event");
                        BFEventsStore.ItemPurchaseFailed(it);
                    }
                }
                else
                {
                    Debug.Log("Debug Mode is disabled. Please enabled it from the Store script and select if you want to return a purchase as successful or not. Jojo. I hope Gaston reads this someday. Surprise!");
                }

#elif FACEBOOK

			//Facebook
			if(o == null)
			{
				DebugHelper.Log(string.Format("The offer with key {0} doesn't exist. Please add it to the store Offers", item.Key));
			}
			else
			{
				_itemBeingBought = new Item();
				_itemBeingBought.Key = o.ItemId.ToString();
				_itemBeingBought.Quantity = o.Amount;
				FB.Canvas.Pay(product: Config.FacebookIAPUrl + item.Key + ".html",
				              action: "purchaseitem",
				              quantity: 1,
				              callback: PayCallback); 
			}
#elif PRIME31
            Item i = new Item();
            i.Key = o.ItemId.ToString();
            i.Quantity = o.Amount;
            string storeId = Config.GetBundlePrefix() + "." + o.StoreId.ToString();
            if(o.Type == IAPType.NONCONSUMABLE)
            {
                var license = Prime31.WinPhoneStore.Store.getProductLicense( storeId);
                if(license == null)
                {
                    //Probably doesn't have inet or something
                    BFStoreEvents.ItemPurchaseCancelled(i);
                    Debug.LogError("License is null. Check internet connection. Check that the product "+storeId+ " exists in the windows store dashboard");
                    return;
                }
                if(license.isActive)//User already owns the item
                {
                    //We trigger the event as if he just bought it
                    BFStoreEvents.ItemPurchaseSucceed(i);
                    return;
                }
            }

            Prime31.WinPhoneStore.Store.loadListingInformation(listingInfo =>
            {
                Prime31.WinPhoneStore.Store.requestProductPurchase(storeId, (receipt, error) =>
                {
                    // we will either have a receipt or an error
                    if (receipt != null)
                    {
                        BFStoreEvents.ItemPurchaseSucceed(i);
                        Prime31.WinPhoneStore.Store.reportProductFulfillment(storeId);
                    }
                    else if (error != null)
                    {
                        BFStoreEvents.ItemPurchaseFailed(i);
                        Debug.LogError(error);
                    }
                });
            });
#endif
            }
        }

#if FACEBOOK 

	void PayCallback (FBResult result)
	{
		if(result != null)
		{
			string s = string.Format("console.log({0});", result.Text);
			Application.ExternalEval(s);
			var response = Json.Deserialize(result.Text) as Dictionary<string, object>;
			if(Convert.ToString(response["status"]) == "completed")
			{
				string s2 = string.Format("console.log(Success! Adding {0} for {1});", _itemBeingBought.Key, _itemBeingBought.Quantity);
				Application.ExternalEval(s2);
				if(OnItemPurchaseSucceeded != null)
				{
					OnItemPurchaseSucceeded(_itemBeingBought);
				}
			}
			else
			{
				if(OnItemPurchaseFailed != null)
				{
					OnItemPurchaseFailed(_itemBeingBought);
				}
			}
		}
		else
		{
			string s2 = string.Format("console.log(Failed , result is null);");
			Application.ExternalEval(s2);
			if(OnItemPurchaseFailed != null)
			{
				OnItemPurchaseFailed(_itemBeingBought);
			}
		}
	}
#endif

        public void GetNonConsumableItemBalance(BFKStoreIds key)
        {
#if SOOMLA && (UNITY_ANDROID || UNITY_IOS)
		soomlaStore.GetItemBalance(key);
#endif
        }

        public void RestoreTransactions()
        {
#if SOOMLA && (UNITY_ANDROID || UNITY_IOS)
		soomlaStore.RestoreTransactions();
#endif
        }

        private Offer GetOffer(string key)
        {
            foreach (Offer o in Offers)
            {
                if (o.StoreId.ToString() == key)
                    return o;
            }

            return null;
        }

        void OnItemCollected(BF_Item item)
        {
#if SOOMLA
//		soomlaStore.ItemCollected(item);
#endif
        }

        void OnItemUsed(BF_Item obj)
        {
#if SOOMLA
	//	soomlaStore.ItemUsed(item);
#endif
        }
    }
}