using UnityEngine;
using System.Collections;

namespace Bigfoot
{

    /// <summary>
    /// Smart button. You must have a BoxCollider , or a BoxCollider2D, to make this work. If you don't have one, you will get an error warning you.
    /// Depending on the usage selected, the smart button will do different things:
    /// Default: Will trigger an event OnSmartButtonClickEvent, you can register to this event by doing "SmartButton.OnSmartButtonClickEvent += myHandlerMethod"
    /// URL: Opens a URL
    /// Scene Loader: Will open the scene with the specified Name (Remember to add that scene in the build settings)
    /// Panel Switcher: Will hide "OurPanel" using SetActive(false), and will show "GoToPanel" using SetActive(true)
    /// IAP Purchase: Will call our General Event System, with the specified ID, and will trigger a purchase event
    /// Exit: Will exit the application
    /// Coroutine Trigger: Will start a coroutine with the specified name, in the specified ComponentToUse
    /// </summary>
    public class SmartButton : MonoBehaviour
    {

        public enum Usage
        {
            Default,
            URL,
            SceneLoader,
            PanelSwitcher,
            IAPPurchase,
            Exit,
            CoroutineTrigger,
            GeneralEvent,
            MethodInvoker,
            Back,
            ShowPanel
        }

        public Usage UseAs;

        public string Value;
        public string Param;
        public MonoBehaviour ComponentToUse;
        public GameObject[] GoToPanel;
        public GameObject[] OurParentPanel;
        public bool InvertOnClickAgain = false;
        public BFKPanelName PanelToShow;

        private bool inCollider;
        private bool firstClick = true;

        #region IAP
        public BF_Item item;
        public BFKStoreIds storeId;
        public BFKCurrencyKeys payingWith;
        #endregion

        public static System.Action<SmartButton> OnSmartButtonClickEvent;

        // Use this for initialization
        void Start()
        {
            if (gameObject.GetComponent<Collider>() == null && gameObject.GetComponent<Collider2D>() == null)
            {
                Debug.LogError("Warning! Smart button might not work without a collider or collider2D attached to the GameObject");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Only calls the OnClick function. This is to support NGUI OnClick and Unity OnMouseDown at the same time
        /// </summary>
        /*void OnMouseDown()
        {
            OnPress(true);
        }*/

        void OnMouseUpAsButton()
        {
            OnClick();
        }

        /// <summary>
        /// When the user clicks, we do different things depending on the usage selected.
        /// </summary>
        public virtual void OnClick()
        {
            switch (UseAs)
            {
                case Usage.Default:
                    if (OnSmartButtonClickEvent != null)
                        OnSmartButtonClickEvent(this);
                    break;

                case Usage.URL:
                    Application.OpenURL(Value);
                    break;

                case Usage.SceneLoader:
                    GeneralEventSystem.Instance.LoadLevel(Value);
                    break;

                case Usage.PanelSwitcher:
                    foreach (GameObject toHide in OurParentPanel)
                    {
                        if (toHide)
                            toHide.SetActive(!firstClick);
                    }
                    foreach (GameObject toShow in GoToPanel)
                    {
                        if (toShow)
                            toShow.SetActive(firstClick);
                    }
                    if (InvertOnClickAgain)
                        firstClick = !firstClick;
                    break;

                case Usage.IAPPurchase:
                    // Value is the type of currency that is being used to buy. eg COIN, MONEY
                    Store.Instance.Purchase(storeId, payingWith);
                    break;

                case Usage.Exit:
                    Application.Quit();
                    break;

                case Usage.CoroutineTrigger:
                    ComponentToUse.StartCoroutine(Value);
                    break;

                case Usage.GeneralEvent:
                    GeneralEventSystem.Instance.InvokeMethod(Value, Param);
                    break;
                case Usage.MethodInvoker:
                    ComponentToUse.Invoke(Value, 0f);
                    break;
                case Usage.ShowPanel:
                    BFEventsGameFlow.ShowPanel(PanelToShow);
                    break;
                case Usage.Back:
                    BFEventsGameFlow.Back();
                    break;
            }
        }
    }
}