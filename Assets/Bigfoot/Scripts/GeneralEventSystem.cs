using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    public class GeneralEventSystem : Singleton<GeneralEventSystem>
    {
        /// <summary>
        /// Fired when a new Level is going to be load
        /// </summary>
        public static System.Action<string> OnLevelStartLoadingEvent;
        /// <summary>
        /// Fired when the level is loading. You can get the progress from the event
        /// </summary>
        public static System.Action<float> OnLevelLoadingEvent;
        /// <summary>
        /// Fired when the level finished loading
        /// </summary>
        public static System.Action OnLevelFinishedLoadingEvent;
        /// <summary>
        /// Fired when the level finished loading. Has a IlevelConfig inside
        /// </summary>
        public static System.Action<ISceneDTO> OnLevelFinishedLoadingWithConfigEvent;

        public static System.Action<string> OnButtonDown;

        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        //TODO: Replace this implementation of loading a scene, with smart coroutines. 
        //TODO: Find a way if it's possible to implement loadLevelAsync with additive scenes for free versions

        /// <summary>
        /// Loads the level. OnLevelStartLoadingEvent will be fired when it starts loading. OnLevelLoadingEvent will be fired when loading, with the progress inside the event.
        /// OnLevelFinishedLoadingEvent will be fired at last
        /// </summary>
        /// <param name="levelName">Level name.</param>
        public void LoadLevel(string levelName)
        {
            if (OnLevelStartLoadingEvent != null)
            {
                OnLevelStartLoadingEvent(levelName);
            }

            StartCoroutine(LoadLevelAsync(levelName));
        }

        private IEnumerator LoadLevelAsync(string levelName)
        {
            AsyncOperation levelLoad = Application.LoadLevelAsync(levelName);
            while (!levelLoad.isDone)
            {
                if (OnLevelLoadingEvent != null)
                {
                    OnLevelLoadingEvent(levelLoad.progress);
                }
                yield return new WaitForEndOfFrame();
            }

            if (OnLevelLoadingEvent != null)
            {
                OnLevelLoadingEvent(levelLoad.progress);
            }

            //TODO: Add levelLoad.allowSceneActivation = false. So we can load everything, and only start the scene manually after throwing OnLevelFinishedLoadingEvent

            if (OnLevelFinishedLoadingEvent != null)
            {
                OnLevelFinishedLoadingEvent();
            }
        }

        /// <summary>
        /// Loads the level with a ILevelconfig. OnLevelStartLoadingEvent will be fired when it starts loading. OnLevelLoadingEvent will be fired when loading, with the progress inside the event.
        /// OnLevelFinishedLoadingEvent will be fired at last. 
        /// </summary>
        /// <param name="levelName">Level name.</param>
        /// <param name="levelConfig">Level config.</param>
        public void LoadLevelWithConfig(string levelName, ISceneDTO sceneDTO)
        {
            if (OnLevelStartLoadingEvent != null)
            {
                OnLevelStartLoadingEvent(levelName);
            }

            StartCoroutine(LoadLevelAsyncWithConfig(levelName, sceneDTO));
        }

        private IEnumerator LoadLevelAsyncWithConfig(string levelName, ISceneDTO sceneDTO)
        {
            AsyncOperation levelLoad = Application.LoadLevelAsync(levelName);
            while (!levelLoad.isDone)
            {
                if (OnLevelLoadingEvent != null)
                {
                    OnLevelLoadingEvent(levelLoad.progress);
                }
                yield return new WaitForEndOfFrame();
            }

            if (OnLevelLoadingEvent != null)
            {
                OnLevelLoadingEvent(levelLoad.progress);
            }

            //TODO: Add levelLoad.allowSceneActivation = false. So we can load everything, and only start the scene manually after throwing OnLevelFinishedLoadingEvent

            if (OnLevelFinishedLoadingWithConfigEvent != null)
            {
                OnLevelFinishedLoadingWithConfigEvent(sceneDTO);
            }
        }
        
        public void InvokeMethod(string value, string param)
        {
            if (param == "")
                Invoke(value, 0f);
            else
                StartCoroutine(value, param);
        }
    }
}