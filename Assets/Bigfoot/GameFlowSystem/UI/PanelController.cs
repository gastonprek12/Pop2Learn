using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Bigfoot
{

    public enum BFKPanelName
    {
		None,
        Shop,
        ExitPanel
    }

    [System.Serializable]
    public class Panel
    {
        public BFKPanelName Name;
        public GameObject PanelGO;
        public bool ShouldHideWhenShowingOtherPanel;
        public bool GlobalPosition;
        public Vector3 Position;

        public Panel(BFKPanelName name, GameObject panelGo)
        {
            this.Name = name;
            this.PanelGO = panelGo;
        }
    }

    public class PanelController : MonoBehaviour
    {
        /// <summary>
        /// If left empty, the parent will be this.gameobject
        /// </summary>
        public GameObject Parent;
        /// <summary>
        /// Panels prefabs with their ids to know which one to show/instantiate
        /// </summary>
        public Panel[] Panels;
        public BFKPanelName StartingPanel;
        /// <summary>
        /// List of panels already instantiated
        /// </summary>
        List<Panel> _panels;
        /// <summary>
        /// List of panels active at the moment
        /// </summary>
        List<Panel> _panelStack;
        GameObject _panelHiding;

        // Use this for initialization
        void Start()
        {
            _panels = new List<Panel>();
            _panelStack = new List<Panel>();
            ShowOrInstantiate(StartingPanel);
        }

        void OnEnable()
        {
            BFEventsGameFlow.OnShowPanel += ShowOrInstantiate;
            BFEventsGameFlow.OnBack += Back;
        }

        void OnDisable()
        {
            BFEventsGameFlow.OnShowPanel -= ShowOrInstantiate;
            BFEventsGameFlow.OnBack -= Back;
        }

        /// <summary>
        /// Shows or instantiates the panel, and adds it to the stack
        /// </summary>
        /// <param name="name"></param>
        public void ShowOrInstantiate(BFKPanelName name)
        {
            //Hide previous panels if needed
            if(_panelStack.Count > 0 && _panelStack.Last() != null)
            {
                Hide(_panelStack.Last().PanelGO);
            }

            //Then show the new one
            var oldPanel = _panels.Where(p => p.Name == name).FirstOrDefault();           
            if(oldPanel != null)
            {
                Show(oldPanel.PanelGO);
                _panelStack.Add(oldPanel);
            }
            else
            {
                //We have to look for the prefab
                var panel = Panels.Where(p => p.Name == name).FirstOrDefault();
                if(panel != null)
                {
                    //Once we find it we have to instantiate it depending on the set up
                    GameObject _instance;
                    if(Parent == null)
				        _instance = NGUITools.AddChild(gameObject, panel.PanelGO);
			        else
                        _instance = NGUITools.AddChild(Parent, panel.PanelGO);

			        if(panel.GlobalPosition)
				        _instance.transform.position = panel.Position;
			        else
				        _instance.transform.localPosition = panel.Position;

                    //Add the panel to the panel list so we don't instantiate it every time we need it
                    var newp = new Panel(name, _instance);
                    _panels.Add(newp);
                    _panelStack.Add(newp);
                    Show(newp.PanelGO);
                }
            }
        }

        /// <summary>
        /// Shows the panel but doesn't add it to the stack
        /// </summary>
        /// <param name="go"></param>
        void Show(GameObject go)
        {
            go.SetActive(true);
            var tweener = go.GetComponent<UITweener>();
            if (tweener != null)
                tweener.PlayForward();
        }
        
        /// <summary>
        /// Hides the object, but doesn't remove it from the stack
        /// </summary>
        /// <param name="go"></param>
        void Hide(GameObject go)
        {
            var tween = go.GetComponent<UITweener>();
            if (tween != null)
            {
                _panelHiding = go;
                tween.PlayReverse();
                Invoke("HideObject", tween.duration + tween.delay);
            }
            else
            {
                go.SetActive(false);
            }
        }

        void HideObject()
        {
            if (_panelHiding != null)
            {
                _panelHiding.SetActive(false);
                _panelHiding = null;
            }
        }

        /// <summary>
        /// Removes the panel from the stack and hides it. Then it shows the previous panel if there's any
        /// </summary>
        public void Back()
        {
            //Get the last panel
            var lastPanel = _panelStack.Last();
            if (lastPanel != null)
            {
                //Hide it
                Hide(lastPanel.PanelGO);
                //Remove it from the stack
                _panelStack.Remove(lastPanel);
                //Find previous active panel
                if(_panelStack.Count > 0)
                {
                    var next = _panelStack.Last();
                    //And show it
                    Show(next.PanelGO);
                }
            }
        }
    }
}