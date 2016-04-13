using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Bigfoot
{
    [RequireComponent(typeof(UIGrid))]
    public class GlobalLeaderboardController : MonoBehaviour
    {

        public GameObject PlayersScorePrefab;

        public string UrlRetrieveScores;

        UIGrid _scoreGrid;

        void Start()
        {
            _scoreGrid = GetComponent<UIGrid>();
            StartCoroutine(GetScoresFromServer());
        }

        IEnumerator GetScoresFromServer()
        {
            WWW www = new WWW(UrlRetrieveScores);

            yield return www;

            if (!string.IsNullOrEmpty(www.error))
                Debug.Log("You tried to fetch the scores for an URL which has the following error: " + www.error);
            else
                ParseScoresFromWWW(www.text);
        }

        void ParseScoresFromWWW(string scores)
        {
            var scoreList = (List<object>)MiniJSON.Json.Deserialize(scores);

            int i = 1;

            foreach (object score in scoreList)
            {
                var player = (Dictionary<string, object>)score;

                if (!string.IsNullOrEmpty(player["Name"].ToString()))
                {
                    var playerPrefab = NGUITools.AddChild(_scoreGrid.gameObject, PlayersScorePrefab);

                    var playerScore = playerPrefab.GetComponent<Score>();

                    if (playerScore != null)
                    {
                        playerScore.Name = player["Name"].ToString();
                        playerScore.PlayerScore = player["Score"].ToString();
                    }

                    _scoreGrid.Reposition();

                    i++;
                }
            }
        }


    }
}