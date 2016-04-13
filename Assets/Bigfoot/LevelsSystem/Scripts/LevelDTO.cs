using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    [System.Serializable]
    public class LevelDTO : ISceneDTO
    {
		public enum Type{
			Train,
			Amateur,
			Pro,
			Challenge
		}
        /// <summary>
        /// Scene to load
        /// </summary>
        public string sceneName;
        /// <summary>
        /// Level Number for loading, saving, ordering, etc
        /// </summary>
        public int LevelNumber;
    
		public Type type;


		public int moneyReward;

		public int barReward;

		public int moneyBet;

		public int honorBet;

		public float sensitivity;
    }
}