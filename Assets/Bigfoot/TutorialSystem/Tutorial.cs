using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    [System.Serializable]
    public class TutorialConig
    {
        public string Name;
        public string SceneName;
        public string Description;
        public bool NextButtonInAnotherObject;
        public GameObject prefab;
        public bool CheckForKeyAsWell;
        public string Key;
    }

    public class Tutorial : Singleton<Tutorial>
    {

        public int stage;
        public TutorialConig[] Configs;

        public override void Awake()
        {
			base.Awake ();
            DontDestroyOnLoad(gameObject);
        }

        void OnEnable()
        {
            stage = PlayerPrefs.GetInt("Tutorial", 0);
            ShowTutorialIfNeeded(false);
            BFEventsTutorial.OnStageCompleted += StageCompleted;
            BFEventsTutorial.OnStageBecameAvailable += StageAvailable;
            //GeneralEventSystem.OnLevelFinishedLoadingEvent += LevelLoaded;
            //GeneralEventSystem.OnLevelFinishedLoadingWithConfigEvent += LevelLoadedWithConfig;
        }

        private void LevelLoadedWithConfig(ISceneDTO obj)
        {
            ShowTutorialIfNeeded(false);
        }

        private void LevelLoaded()
        {
            ShowTutorialIfNeeded(false);
        }

        void OnDisable()
        {
            BFEventsTutorial.OnStageCompleted -= StageCompleted;
            BFEventsTutorial.OnStageBecameAvailable -= StageAvailable;
            //GeneralEventSystem.OnLevelFinishedLoadingEvent -= LevelLoaded;
            //GeneralEventSystem.OnLevelFinishedLoadingWithConfigEvent -= LevelLoadedWithConfig;
        }

        private void StageAvailable()
        {
            ShowTutorialIfNeeded(true);
        }

        public void ShowTutorialIfNeeded(bool onlyIfLocal)
        {
            if (stage < Configs.Length)
            {
                var stageCfg = Configs[stage];
                if (stageCfg.SceneName == Application.loadedLevelName)
                {
                    //If we need to check for a key, and the key is not set up as 1 yet, then we can't show anything
                    if (stageCfg.CheckForKeyAsWell && PlayerPrefs.GetInt(stageCfg.Key, 0) != 1)
                        return;
                    //If the next button is in another object, that means the user has to click at something specific to continue, so we just throw the event
                    if (stageCfg.NextButtonInAnotherObject)
                    {
                        BFEventsTutorial.ShowTutorial(stage, stageCfg.prefab, stageCfg.Description);
                    }
                    else if (!onlyIfLocal)//But if that's not the case, we need to instantiate it ourselves in the UI hierarchy and let the user read until the text is finished or he skips it
                    {
                        ShowGlobalTutorial(stageCfg.prefab, stageCfg.Description);
                    }
                }
            }
        }

        void ShowGlobalTutorial(GameObject prefab, string description)
        {
            var tutoPanel = GameObject.Find("TutorialPanel");
            if (tutoPanel)
            {
                var go = NGUITools.AddChild(tutoPanel, prefab);
                go.GetComponent<TutorialGlobalStage>().stage = stage;
                go.GetComponentInChildren<UILabel>().text = description;
            }
            else
            {
                //Let the hacking begin
                StartCoroutine(FindPanelUntilYouFindItBitch(prefab, description));
            }
        }

        IEnumerator FindPanelUntilYouFindItBitch(GameObject prefab, string description)
        {
            yield return new WaitForEndOfFrame();
            while (GameObject.Find("TutorialPanel") == null)
            {
                yield return new WaitForSeconds(1);
            }
            ShowGlobalTutorial(prefab, description);
        }

        private void StageCompleted(int s)
        {
            if (s == stage)
            {
                stage = s + 1;
                PlayerPrefs.SetInt("Tutorial", stage);
                ShowTutorialIfNeeded(false);
            }
        }

    }

}