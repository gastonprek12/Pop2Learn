using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Bigfoot
{

    [CustomEditor(typeof(SmartButton))]
    [CanEditMultipleObjects]
    public class SmartButtonEditor : Editor
    {

        /// <summary>
        /// The smart button itself. Here we will have stored all of his variables
        /// </summary>
        private SmartButton smartButton;
        private bool collapsedToHide;
        private bool collapsedToShow;
        private int arraySizeToHide;
        private int arraySizeToShow;


        void Start()
        {

        }

        // Use this for initialization
        public override void OnInspectorGUI()
        {
            // Unity provides a target when overriding the inspector look, and it has always the same type we declare on CustmoEditor(typeof()) on top
            smartButton = (SmartButton)target;

            if (smartButton.OurParentPanel != null)
                arraySizeToHide = smartButton.OurParentPanel.Length;
            if (smartButton.GoToPanel != null)
                arraySizeToShow = smartButton.GoToPanel.Length;

            //We will use this variable, the selected enum in the smart button, not only to do different things in runtime, but also to show different inputs in the editor
            smartButton.UseAs = (SmartButton.Usage)EditorGUILayout.EnumPopup("Use As", smartButton.UseAs);

            switch (smartButton.UseAs)
            {

                //Default usage just triggers an event in case someone is listening.
                //TODO: Maybe add some string as parameter
                case SmartButton.Usage.Default:
                    break;

                //Just show a textfield so the user can enter the url we want to open
                case SmartButton.Usage.URL:
                    smartButton.Value = EditorGUILayout.TextField("URL", smartButton.Value);
                    break;

                //Same with the scenes
                //TODO: Maybe add load scene by number
                case SmartButton.Usage.SceneLoader:
                    smartButton.Value = EditorGUILayout.TextField("Scene Name", smartButton.Value);
                    break;

                //So here it gets tricky.
                case SmartButton.Usage.PanelSwitcher:


                    smartButton.InvertOnClickAgain = EditorGUILayout.Toggle("Invert On Click Again", smartButton.InvertOnClickAgain);

                    //Objects to hide Array
                    collapsedToHide = EditorGUILayout.Foldout(collapsedToHide, "Objects to Hide");
                    if (collapsedToHide)
                    {
                        arraySizeToHide = EditorGUILayout.IntField(arraySizeToHide);


                        if (arraySizeToHide > 0)
                        {
                            GameObject[] temp = new GameObject[arraySizeToHide];
                            if (smartButton.OurParentPanel == null)
                            {
                                smartButton.OurParentPanel = new GameObject[arraySizeToHide];
                            }

                            if (arraySizeToHide > smartButton.OurParentPanel.Length)
                            {
                                smartButton.OurParentPanel.CopyTo(temp, 0);
                            }
                            else
                            {
                                for (int i = 0; i < arraySizeToHide; i++)
                                {
                                    temp[i] = smartButton.OurParentPanel[i];
                                }
                            }

                            smartButton.OurParentPanel = new GameObject[arraySizeToHide];

                            temp.CopyTo(smartButton.OurParentPanel, 0);

                            for (int i = 0; i < arraySizeToHide; i++)
                            {
                                smartButton.OurParentPanel[i] = (GameObject)EditorGUILayout.ObjectField(smartButton.OurParentPanel[i], typeof(GameObject), true);
                            }
                        }
                        else
                        {
                            smartButton.OurParentPanel = null;
                        }
                    }

                    collapsedToShow = EditorGUILayout.Foldout(collapsedToShow, "Objects to Show");
                    if (collapsedToShow)
                    {
                        arraySizeToShow = EditorGUILayout.IntField(arraySizeToShow);

                        if (arraySizeToShow > 0)
                        {
                            GameObject[] temp = new GameObject[arraySizeToShow];

                            if (smartButton.GoToPanel == null)
                            {
                                smartButton.GoToPanel = new GameObject[arraySizeToShow];
                            }

                            if (arraySizeToShow > smartButton.GoToPanel.Length)
                            {
                                smartButton.GoToPanel.CopyTo(temp, 0);
                            }
                            else
                            {
                                for (int i = 0; i < arraySizeToShow; i++)
                                {
                                    temp[i] = smartButton.GoToPanel[i];
                                }
                            }

                            smartButton.GoToPanel = new GameObject[arraySizeToShow];

                            temp.CopyTo(smartButton.GoToPanel, 0);

                            for (int i = 0; i < arraySizeToShow; i++)
                            {
                                smartButton.GoToPanel[i] = (GameObject)EditorGUILayout.ObjectField(smartButton.GoToPanel[i], typeof(GameObject), true);
                            }
                        }
                        else
                        {
                            smartButton.GoToPanel = null;
                        }
                    }

                    //Obsolete. Now we'll use an array
                    /*//We tell the layout that the following fields should be in the same level
                    EditorGUILayout.BeginHorizontal();
                    //Adding a label
                    EditorGUILayout.LabelField("Our Parent Panel");
                    //Create a Game Object input. The user can drop game objects here, and we store them in OurPanel variable
                    smartButton.OurParentPanel = (GameObject)EditorGUILayout.ObjectField(smartButton.OurParentPanel,typeof(GameObject),true);
                    //Tell the layout that we can keep adding stuff in the next level
                    EditorGUILayout.EndHorizontal();

                    //Same with the go to panel
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Go to Panel");
                    smartButton.GoToPanel = (GameObject)EditorGUILayout.ObjectField(smartButton.GoToPanel,typeof(GameObject),true);
                    EditorGUILayout.EndHorizontal();

                    //Good things about customizing inspector views. We can add some clarifications
                    EditorGUILayout.LabelField("If Go to Panel is null, it will only hide Our Parent Panel");*/
                    break;

                case SmartButton.Usage.IAPPurchase:
                    //Simple text field for the item id we want to purchase
                    /*if(smartButton.item == null)
                        smartButton.item = new Item();

                    Item item = smartButton.item;*/

                    smartButton.storeId = (BFKStoreIds)EditorGUILayout.EnumPopup("Store Id", smartButton.storeId);
                    //item.Key = smartButton.storeId.ToString();
                    //item.AmountGiven = EditorGUILayout.IntField("Amount", item.AmountGiven);

                    smartButton.payingWith = (BFKCurrencyKeys)EditorGUILayout.EnumPopup("Paying with", smartButton.payingWith);
                    //smartButton.Value = smartButton.payingWith.ToString();
                    break;

                //Show nothing, just want to exit game
                case SmartButton.Usage.Exit:
                    break;

                //Here we will get a monobehaviour reference, and a string to now what coroutine to call
                //TODO: Maybe add some parameters to the coroutine.
                //TODO: Maybe add objects as parameters to the coroutine
                case SmartButton.Usage.CoroutineTrigger:

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Component with Coroutine");
                    //Just getting the Monobehaviour reference from our object input field
                    smartButton.ComponentToUse = (MonoBehaviour)EditorGUILayout.ObjectField(smartButton.ComponentToUse, typeof(MonoBehaviour), true);
                    EditorGUILayout.EndHorizontal();

                    //And the name of the coroutine
                    smartButton.Value = EditorGUILayout.TextField("Coroutine Name", smartButton.Value);
                    break;

                case SmartButton.Usage.GeneralEvent:
                    smartButton.Value = EditorGUILayout.TextField("Method Name", smartButton.Value);
                    smartButton.Param = EditorGUILayout.TextField("Parameter", smartButton.Param);
                    break;

                case SmartButton.Usage.MethodInvoker:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Component to use");
                    smartButton.ComponentToUse = (MonoBehaviour)EditorGUILayout.ObjectField(smartButton.ComponentToUse, typeof(MonoBehaviour), true);
                    EditorGUILayout.EndHorizontal();
                    smartButton.Value = EditorGUILayout.TextField("Method Name", smartButton.Value);
                    break;

                case SmartButton.Usage.Back:
                    break;

                case SmartButton.Usage.ShowPanel:
                    smartButton.PanelToShow = (BFKPanelName)EditorGUILayout.EnumPopup("Panel to show", smartButton.PanelToShow);
                    break;
            }

            //IF something has changed, we let unity know, so it makes us save the scene, and therefore, the smart button values will be saved
            if (GUI.changed)
                EditorUtility.SetDirty(target);
        }

    }

}