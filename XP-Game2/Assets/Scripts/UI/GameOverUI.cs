using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public event EventHandler OnButtonClick;

    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private TextMeshProUGUI recipesLostText;
    [SerializeField] private TextMeshProUGUI recipesTotalText;

    
    
    [SerializeField] private Button restartButton;
    [SerializeField] private Button submitDuoButton;
    [SerializeField] private Button reloadLeaderboardButton;

    
    [SerializeField] private RectTransform leaderboardRectTransform;

    private void Awake() 
    {
        restartButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
            OnButtonClick?.Invoke(this, EventArgs.Empty);
        });
        submitDuoButton.onClick.AddListener(() => {
            PlayfabManager.Instance.Register();
            OnButtonClick?.Invoke(this, EventArgs.Empty);
        });
        reloadLeaderboardButton.onClick.AddListener(() => {
            PlayfabManager.Instance.GetLeaderboard();
            OnButtonClick?.Invoke(this, EventArgs.Empty);
        });    
    }
    private void Start() 
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

        GameInput.Instance.OnGetLeaderboardAction += GameInput_OnGetLeaderboardAction;
        GameInput.Instance.OnSubmitLeaderboardAction += GameInput_OnSubmitLeaderboardAction;

        Hide();
    }

    private void GameInput_OnGetLeaderboardAction(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsGameOver())
        {
            PlayfabManager.Instance.GetLeaderboard();
            OnButtonClick?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnSubmitLeaderboardAction(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsGameOver())
        {
            PlayfabManager.Instance.Register();
            OnButtonClick?.Invoke(this, EventArgs.Empty);
        }
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsGameOver())
        {
            Show();
            
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();

            /* recipesLostText.text = "-" + DeliveryManager.Instance.GetFailedRecipesAmount().ToString();
            recipesTotalText.text = DeliveryManager.Instance.GetTotalRecipesAmount().ToString(); */
        }
        else
        {
            Hide();
        }
    }

    private void Update() 
    {

    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
