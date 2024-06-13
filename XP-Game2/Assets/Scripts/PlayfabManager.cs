using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager Instance { get; private set; }

    public GameObject rowPrefab;
    public Transform rowsParent;
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] TextMeshProUGUI duoNameInput;
    [SerializeField] private RectTransform leaderboardRectTransform;
    [SerializeField] private RectTransform submitNameRectTransform;

    private void Awake() 
    {
        Instance = this;
    }

    private void Start()
    {

    }

    public void Register()
    {
        string tempEmail = duoNameInput.text;
        string tempEmailNoSpaces = duoNameInput.text.Replace(" ", string.Empty);
        string tempPassword = "123456";
        Debug.Log(tempEmailNoSpaces);

        var request = new RegisterPlayFabUserRequest
        {
            Email = tempEmailNoSpaces + "@acaimasters.com",
            Password = tempPassword,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);

    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        SendLeaderboard(DeliveryManager.Instance.GetTotalRecipesAmount());

        messageText.text = "Registrado com sucesso";
        Debug.Log("Regsiter and logged in!");

        SubmitName();
    }

    public void LoginButton()
    {
        string tempEmail = duoNameInput.text;
        string tempEmailNoSpaces = duoNameInput.text.Replace(" ", string.Empty);
        string tempPassword = "123456";

        var request = new LoginWithEmailAddressRequest
        {
            Email = tempEmailNoSpaces + "@acaimasters.com",
            Password = tempPassword,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSucces, OnError);
    }

    private void OnLoginSucces(LoginResult result)
    {
        SendLeaderboard(DeliveryManager.Instance.GetTotalRecipesAmount());

        messageText.text = "Logged In!";
        Debug.Log("Sucesso em logar/registrar conta");
        SubmitName();
    }
    private void OnError(PlayFabError error)
    {
        if(error.ErrorMessage == "Email address not available")
        {
            LoginButton();
        }

        // messageText.text = error.ErrorMessage;
        Debug.Log("Erro ao logar/registrar conta");   
        Debug.Log(error.GenerateErrorReport());
    }

    public void SubmitName()
    {
        string tempDisplayName = duoNameInput.text;

        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = tempDisplayName,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Nome atualizado!");
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "RecipesDelivered",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        GetLeaderboard();
        Debug.Log("Sucesso ao enviar para a leaderboard");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "RecipesDelivered",
            StartPosition = 0,
            MaxResultsCount = 10,
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        LeanTween.scale(submitNameRectTransform, new Vector3(0,0,0), 0.5f).setEaseOutExpo();
        LeanTween.scale(leaderboardRectTransform, new Vector3(1,1,1), 0.5f).setEaseInExpo();

        foreach(Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }

        foreach(var item in result.Leaderboard)
        {
            GameObject newGO = Instantiate(rowPrefab, rowsParent);
            TextMeshProUGUI[] texts = newGO.GetComponentsInChildren<TextMeshProUGUI>();
            Image[] image = newGO.GetComponentsInChildren<Image>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(item.Position + " | " + item.PlayFabId + " | " + item.StatValue);
        }
    }


    //OFF
    private void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier, 
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSucces, OnError);
    }
}