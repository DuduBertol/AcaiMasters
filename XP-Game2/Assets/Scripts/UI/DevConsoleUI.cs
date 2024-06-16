using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DevConsoleUI : MonoBehaviour
{
    [Header("Leaderboard")]
    [SerializeField] Button getLeaderboardButton;
    

    [Header("Game Playing Timer Before Start")]
    [SerializeField] TextMeshProUGUI gamePlayingTimerBeforeStartText;
    [SerializeField] Button increase10GamePlayingBeforeStartTimer;
    [SerializeField] Button decrease10GamePlayingBeforeStartTimer;


    [Header("Game Playing Timer In-Game")]
    [SerializeField] TextMeshProUGUI gamePlayingTimerInGameText;
    [SerializeField] Button increase10GamePlayingInGameTimer;
    [SerializeField] Button decrease10GamePlayingInGameTimer;


    [Header("Recipe Life Timer")]
    [SerializeField] TextMeshProUGUI recipeLifeTimerMaxText;
    [SerializeField] Button increase10RecipeLifeTimer;
    [SerializeField] Button decrease10RecipeLifeTimer;

    private bool isEnable = false;

    private void Awake() 
    {
        //Leaderboard
        getLeaderboardButton.onClick.AddListener(() => {
            PlayfabManager.Instance.GetLeaderboard();
        });

        //Game Playing Timer Before Start
        increase10GamePlayingBeforeStartTimer.onClick.AddListener(() => {
            KitchenGameManager.Instance.gamePlayingTimerMax += 10f;
            gamePlayingTimerBeforeStartText.text = KitchenGameManager.Instance.gamePlayingTimerMax.ToString();
            KitchenGameManager.Instance.SetGamePlayingTimerMaxPlayerPrefs(KitchenGameManager.Instance.gamePlayingTimerMax);
        });
        decrease10GamePlayingBeforeStartTimer.onClick.AddListener(() => {
            KitchenGameManager.Instance.gamePlayingTimerMax -= 10f;
            gamePlayingTimerBeforeStartText.text = KitchenGameManager.Instance.gamePlayingTimerMax.ToString();
            KitchenGameManager.Instance.SetGamePlayingTimerMaxPlayerPrefs(KitchenGameManager.Instance.gamePlayingTimerMax);
        });

        //Game Playing Timer In-Game
        increase10GamePlayingInGameTimer.onClick.AddListener(() => {
            KitchenGameManager.Instance.gamePlayingTimer += 10f;
        });
        decrease10GamePlayingInGameTimer.onClick.AddListener(() => {
            KitchenGameManager.Instance.gamePlayingTimer -= 10f;
        });

        //Recipe Life Timer
        increase10RecipeLifeTimer.onClick.AddListener(() => {
            DeliveryManager.Instance.recipeLifeTimerMax += 10f;
            recipeLifeTimerMaxText.text = DeliveryManager.Instance.recipeLifeTimerMax.ToString();
            DeliveryManager.Instance.SetRecipeLifeTimerMaxPlayerPrefs(DeliveryManager.Instance.recipeLifeTimerMax);
        });
        decrease10RecipeLifeTimer.onClick.AddListener(() => {
            DeliveryManager.Instance.recipeLifeTimerMax -= 10f;
            recipeLifeTimerMaxText.text = DeliveryManager.Instance.recipeLifeTimerMax.ToString();
            DeliveryManager.Instance.SetRecipeLifeTimerMaxPlayerPrefs(DeliveryManager.Instance.recipeLifeTimerMax);
        });

    }

    private void Start() 
    {
        this.gameObject.SetActive(false);

        GameInput.Instance.OnOpenDevConsoleAction += GameInput_OnOpenDevConsoleAction;

        gamePlayingTimerBeforeStartText.text = KitchenGameManager.Instance.gamePlayingTimerMax.ToString();
        gamePlayingTimerBeforeStartText.text = KitchenGameManager.Instance.gamePlayingTimerMax.ToString();
        recipeLifeTimerMaxText.text = DeliveryManager.Instance.recipeLifeTimerMax.ToString();
    }

    private void GameInput_OnOpenDevConsoleAction(object sender, System.EventArgs e)
    {
        isEnable = !isEnable;

        if(isEnable)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Update() 
    {
        gamePlayingTimerInGameText.text = KitchenGameManager.Instance.gamePlayingTimer.ToString();    
    }


}
