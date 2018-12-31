using UnityEngine;
using System.Collections;

public static class FacebookAndPlayFabInfo
{
    //Id do usuário no PlayFab
    public static string userPlayFabId = "";
    //Id do usuário no Facebook
    public static string userFacebookId = "";
    //Nome do usuário no Facebook
    public static string userName = "";
    //Indica se o usuário já está logado no PlayFab
    public static bool isLoggedOnPlayFab = false;
    //Indica se os dados do leaderboard já foram carregados.
    public static bool leaderboardLoaded = false;
    //Indica se os dados do leaderboard estão sendo carregados.
    public static bool leaderboardIsLoading = false;
}