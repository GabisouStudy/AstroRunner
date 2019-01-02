using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
public class PlayServices : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate(succes => { });

    }

    public static void UnlockAnchievment(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }

    public static void IncrementAchievment(string id, int steps)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, steps, success => { });
    }

    public static void ShowAchievments()
    {
        Social.ShowAchievementsUI();
    }

    public static void PostScore(long score, string leaderBoard)
    {
        Social.ReportScore(score, leaderBoard, (success => { }));
    }

    public static void ShowLeaderboard(string leaderboard)
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboard);
    }

    public static long GetPlayerScore(string leaderboard)
    {
        long score = 0;
        PlayGamesPlatform.Instance.LoadScores(leaderboard, LeaderboardStart.PlayerCentered, 1, LeaderboardCollection.Public,
            LeaderboardTimeSpan.AllTime, (LeaderboardScoreData data) => { score = data.PlayerScore.value; });
        return score;
    }

}
