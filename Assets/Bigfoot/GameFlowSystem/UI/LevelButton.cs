using UnityEngine;
using System.Collections;
namespace Bigfoot
{
    /// <summary>
    /// Load Level button. Must have a collider attached to work. The idea isn't for you to use this class, but to inherit from it, and override OnClick event, and send the corresponding levelConfig of your game.
    /// </summary>
    public class LevelButton : MonoBehaviour
    {
        public LevelDTO Config;
        
        [HideInInspector]
        public int LevelNumber
        {
            get
            {
                return Config.LevelNumber;
            }
        }

        void OnMouseDown()
        {
            OnClick();
        }

        /// <summary>
        /// Don't use this. Override this method and fill the levelconfig information as you please.
        /// </summary>
        public virtual void OnClick()
        {
			GeneralEventSystem.Instance.LoadLevelWithConfig(Config.sceneName, Config);
        }
    }

}