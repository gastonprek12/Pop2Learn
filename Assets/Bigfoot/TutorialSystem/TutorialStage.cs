using UnityEngine;
using System.Collections;

namespace Bigfoot
{

    /// <summary>
    /// Add this to the clickable object (If it has any). And set up the parent object.
    /// This will automatically choose the parent as parent object if there isn't any. 
    /// This class will set every child widget of that parent with a high depth so it can be shown after the black layer
    /// </summary>
    public class TutorialStage : MonoBehaviour
    {

        public GameObject ParentObject;
        GameObject TutorialPrefab;
        public int stage;
        GameObject layer;
        bool isShowingTutorial;

        void Start()
        {
            if (ParentObject == null)
            {
                ParentObject = transform.parent.gameObject;
            }
        }

        void ShowTutorial(int s, GameObject prefab, string description)
        {
			if (ParentObject == null)
			{
				ParentObject = transform.parent.gameObject;
			}
			TutorialPrefab = prefab;
			if (stage == s)
            {
                isShowingTutorial = true;
                AddDepthRecursively(ParentObject, 900);
                layer = NGUITools.AddChild(ParentObject, TutorialPrefab);
                layer.GetComponentInChildren<UILabel>().text = description;
                layer.transform.position = Vector3.zero;
            }
        }

        void AddDepthRecursively(GameObject go, int amount)
        {
            var childs = go.GetComponentsInChildren<UIWidget>();
            for (int i = 0; i < childs.Length; i++)
            {
                childs[i].depth += amount;
                var greatsons = childs[i].GetComponentsInChildren<UIWidget>();
                if (greatsons.Length > 1 && i > 0)
                {
                    AddDepthRecursively(childs[i].gameObject, amount);
                }
            }
        }

        void OnEnable()
        {
            BFEventsTutorial.OnShowTutorial += ShowTutorial;
            BFEventsTutorial.StageBecameAvailable();
        }

        void OnDisable()
        {
            BFEventsTutorial.OnShowTutorial -= ShowTutorial;
        }

        void OnClick()
        {
            if (isShowingTutorial)
            {
                NGUITools.Destroy(layer);
                AddDepthRecursively(ParentObject, -900);
                BFEventsTutorial.StageCompleted(stage);
            }
        }
    }
}