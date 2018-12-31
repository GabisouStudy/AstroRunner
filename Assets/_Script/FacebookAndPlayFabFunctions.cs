using UnityEngine;
using System.Collections;
using Facebook.Unity;
using UnityEngine.UI;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using Facebook.MiniJSON;
using System;

public class FacebookAndPlayFabFunctions : MonoBehaviour
{
    //Title Id do seu jogo no site do PlayFab
    public string gameTitleId = "";

    void Start()
    {
        //Usado para inicializar o sdk do facebook.
        FB.Init(InitCallback, null, null);
        //Usado para indicar ao sdk do PlayFab o Id do seu jogo.
        PlayFabSettings.TitleId = gameTitleId;
    }

    private void InitCallback()
    {
        //Verifica se foi possível inicializar o sdk do facebook.
        if (FB.IsInitialized)
        {
            //Ativa o sdk do facebook no jogo.
            FB.ActivateApp();
        }
    }

    //Usado para realizar o login no facebook e no playfab ao mesmo tempo.
    public void Login()
    {
        // Usado para verificar se o usuário já está logado, caso não esteja ele tenta logar.
        if (!FB.IsLoggedIn)
        {
            //Cria a lista de permissões que o aplicativo utilizará, essas são as permissões padrão.
            List<string> permissions = new List<string>() { "public_profile", "email", "user_friends" };

            //Utiliza o SDK do Facebook para realizar o login, utilizando as permissões e indicando a função de callback.
            FB.LogInWithReadPermissions(permissions, LoginFacebookCallBack);
        }
    }
    public void SetUserData(int value)
    {
   
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate { StatisticName = "Score", Value = value},
            }
        },

        result => { Debug.Log("User statistics updated"); },
        error => { Debug.LogError(error.GenerateErrorReport()); });
    }
    public void GetLeaderboards()
    {

        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest { StatisticName = "Score", StartPosition = 0, MaxResultsCount = 10 }, (GetLeaderboardResult r) =>
        {

            var leaderboard = r.Leaderboard;
            foreach (var item in leaderboard)
            {
                Debug.Log("Nome: " + item.DisplayName);
                Debug.Log("Score: " + item.StatValue);
            }

        }, null);

       
    }

    void OnGetStatistics(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        
        foreach (var eachStat in result.Statistics)
        {
            Debug.Log(result.Request);
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
        }
      
    }

    private void LoginFacebookCallBack(ILoginResult loginResult)
    {
        //Caso o resultado seja nulo, deu algum erro no login.
        if (loginResult == null)
        {
            Debug.Log("Não foi possível logar no facebook.");
            return;
        }

        //Verifica se o retorno não foi um erro, ou algum tipo de cancelamento caso não seja nenhum desses tipos indica
        //que foi possível realizar o login com o facebook com sucesso.
        if (string.IsNullOrEmpty(loginResult.Error) && !loginResult.Cancelled && !string.IsNullOrEmpty(loginResult.RawResult))
        {
            Debug.Log("Sucesso ao Logar no Facebook.");

            //Aqui verificamos se o usuário já está logado no PlayFab e caso não esteja tenta realizar o login.
            if (!FacebookAndPlayFabInfo.isLoggedOnPlayFab)
            {
                //O PlayFab possuí vários tipos de login, neste artigo vamos utilizar o Login com o facebook
                //então nessa parte configuramos uma chamada para o PlayFab se Logar através dos dados do usuário
                //no facebook.
                LoginWithFacebookRequest loginFacebookRequest = new LoginWithFacebookRequest()
                {
                    //Indica o TitleId do seu jogo no PlayFab.
                    TitleId = PlayFabSettings.TitleId,
                    //Indica o token de acesso do usuário no Facebook, esse token só é gerado se o usuário já estiver
                    //logado no facebook
                    AccessToken = Facebook.Unity.AccessToken.CurrentAccessToken.TokenString,
                    //Indica para criar uma conta automaticamente dentro do seu jogo no PlayFab para este usuário, 
                    //caso ele já não possua uma
                    CreateAccount = true
                };

                //Utiliza o SDK do PlayFab para realizar o login, utilizando a chamada e indicando as funções de callback
                //de sucesso e de error.
                PlayFabClientAPI.LoginWithFacebook(loginFacebookRequest, PlayFabLoginSucessCallBack, PlayFabErrorCallBack);
            }
        }
        else
            Debug.Log("Não foi possível logar no facebook.");
    }

    private void PlayFabLoginSucessCallBack(PlayFab.ClientModels.LoginResult playfabLoginResult)
    {
        Debug.Log("Sucesso ao Logar no PlayFab.");

        //Armazena o Id do PlayFab na classe estática e com isso é possível utilizar esses dados em outros scripts.
        FacebookAndPlayFabInfo.userPlayFabId = playfabLoginResult.PlayFabId;

        //Utiliza o SDK do Facebook para consultar os dados, indicando a função de callback. O valor "/me" indica
        //que estou buscando os dados do usuário que está logado. O valor HttpMethod.GET indica que a nossa chamada ao
        //facebook tem a intenção de somente coletar dados.
        FB.API("/me", HttpMethod.GET, CollectLoggedUserInfoCallback);
    }

    private void CollectLoggedUserInfoCallback(IGraphResult result)
    {
        //Caso o resultado seja nulo, deu algum erro ao coletar os dados.
        if (result == null)
        {
            Debug.Log("Não foi possível coletar os dados do usuário no Facebook.");
            return;
        }

        //Verifica se o retorno não foi um erro, ou algum tipo de cancelamento caso não seja nenhum desses tipos indica
        //que foi possível coletar os dados do facebook com sucesso.
        if (string.IsNullOrEmpty(result.Error) && !result.Cancelled && !string.IsNullOrEmpty(result.RawResult))
        {
            Debug.Log("Sucesso ao coletar os dados da conta do usuário no Facebook");

            try
            {
                //A resposta do Facebook vem em formato de Json e com isso nós convertemos o Json para um Dictionary
                //para ser mais facil de coletar os dados
                Dictionary<string, object> dict = Json.Deserialize(result.RawResult) as Dictionary<string, object>;
                string userFacebookName = dict["name"] as string;
                string userFacebookId = dict["id"] as string;

                //Exibe o Json de resposta no console da Unity
                Debug.Log(dict.ToJson());

                //Chamada usada para atualizar o nome do Usuario no playFab com o id do Facebook. Isto irá permitir 
                //que futuramente seja possível coletar as informações desse usuário ao exibir o resultado do Leaderboard.
                //Neste ponto foi utilizado o Id do Facebook ao invés do Nome do usuário no Facebook porque o campo
                //Display Name no PlayFab deve ser único e não passar de 25 caracters. Sendo assim o Id do facebook
                //nos atende muito bem pois ele é um id único por usuário e não ultrapassa esse limite.
                UpdateUserTitleDisplayNameRequest request = new UpdateUserTitleDisplayNameRequest();
                request.DisplayName = userFacebookName;

                PlayFabClientAPI.UpdateUserTitleDisplayName(request, UpdateUserTitleDisplayNameSucessCallback, PlayFabErrorCallBack);

                //Atualiza as informações da classe estática indicando que o usuário está logado no PlayFab
                //e informando o Id do facebook e o nome do usuário. Com isso é possível utilizar esses dados em outros scripts.
                FacebookAndPlayFabInfo.isLoggedOnPlayFab = true;
                FacebookAndPlayFabInfo.userFacebookId = userFacebookId;
                FacebookAndPlayFabInfo.userName = userFacebookName;
            }
            //Usado caso o Facebook não tenha retornado o id ou o nome do usuário.
            catch (KeyNotFoundException e)
            {
                Debug.Log("Não foi possível coletar os dados do usuário no Facebook. Erro: " + e.Message);
            }
        }
        else
            Debug.Log("Não foi possível coletar os dados do usuário no Facebook.");
    }

    private void UpdateUserTitleDisplayNameSucessCallback(UpdateUserTitleDisplayNameResult result)
    {
        //Exibe no console da Unity que atualizou o campo com sucesso.
      
        Debug.Log("O campo Display Name do usuário no PlayFab foi atualizado com sucesso.");
    }

    private void PlayFabErrorCallBack(PlayFabError playfabError)
    {
        //Exibe no console da Unity as informações do erro.
        Debug.Log(playfabError.ErrorMessage);
        Debug.Log(playfabError.ErrorDetails);
    }
}
