using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using PlayFab.MultiplayerModels;

public class Leaderboard : MonoBehaviour
{
    public GameObject leaderboardCanvas;
    public GameObject[] leaderboardEntries;

    public static Leaderboard instance;
    void Awake() { instance = this; }

    public void OnLoggedIn()
    {
        //leaderboardCanvas.SetActive(true);
        //DisplayLeaderboard();
        Debug.Log("Logged in.");
    }

    public void DisplayLeaderboard ()
    {
        Debug.Log("Displaying Leaderboard");

        GetLeaderboardRequest getLeaderboardRequest = new GetLeaderboardRequest
        {
            StatisticName = "HighestScore",
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(getLeaderboardRequest,
            result => UpdateLeaderboardUI(result.Leaderboard),
            error => Debug.Log(error.ErrorMessage)
        );
    }

    void UpdateLeaderboardUI(List<PlayerLeaderboardEntry> leaderboard)
    {
        Debug.Log("Updating Leaderboard UI");

        for (int x = 0; x < leaderboardEntries.Length; x++)
        {
            leaderboardEntries[x].SetActive(x < leaderboard.Count);
            if (x >= leaderboard.Count) continue;
            leaderboardEntries[x].transform.Find("PlayerName").GetComponent<TextMeshProUGUI>().text = (leaderboard[x].Position + 1) + ". " + leaderboard[x].DisplayName;

            leaderboardEntries[x].transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = ((int)leaderboard[x].StatValue).ToString();//("F2");
        }
    }

    public void SetLeaderboardEntry(int newScore)
    {
        bool useLegacyMethod = false;
        if (useLegacyMethod)
        {
            ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest
            {
                FunctionName = "UpdateHighScore",
                FunctionParameter = new { score = newScore }
            };
            PlayFabClientAPI.ExecuteCloudScript(request,
            result =>
            {
                Debug.Log(result);
                DisplayLeaderboard();
                Debug.Log(result.ToJson());
            },
            error =>
            {
                Debug.Log(error.ErrorMessage);
                Debug.Log("ERROR");
            }
            );
        }
        else
        {

            PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate { StatisticName = "HighestScore", Value = newScore },
                }
            },
            result => { Debug.Log("User statistics updated"); },
            error => { Debug.LogError(error.GenerateErrorReport()); }
            );
        }
    }
}
