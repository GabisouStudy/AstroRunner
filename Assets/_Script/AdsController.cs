using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour {
    private static Action Callback = null;
    private static bool isInit = false;
    private static AdsController instance = null;

	/// <summary>
	/// Esse método deve ser chamado ao iniciar o jogo, para que o sistema de ADS seja inicializado
	/// </summary>
    public static void Init()
    {
        if (!isInit)
        {
            if (instance == null)
                instance = new GameObject("AdsController").AddComponent<AdsController>();
            isInit = true;
        }
    }

	/// <summary>
	/// Este é o método para solicitar uma propaganda de vídeo com recompensa
	/// </summary>
	/// <param name="callback">Um método anônimo que será chamado caso o usuário assista todo o vídeo</param>
    public static void ShowRewardedAd(Action callback)
    {
#if UNITY_ANDROID || UNITY_IOS
        Callback = callback;
		if(!ShowRewardedUnity())
			Debug.Log("No rewards available :/ Try again later");
#else
        Callback();
#endif
    }

    private static bool ShowRewardedUnity()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (Advertisement.IsReady())
        {

            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(null, options);
            return true;
        }
        else
            return false;
#else
        return true;
#endif
    }

#if UNITY_ANDROID || UNITY_IOS
    private static void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                if(Callback != null)
                    Callback();
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                //No ads available
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
#endif
}
