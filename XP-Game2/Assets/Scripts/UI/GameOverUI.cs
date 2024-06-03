using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private TextMeshProUGUI recipesLostText;
    [SerializeField] private TextMeshProUGUI recipesTotalText;

    private void Start() 
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

        Hide();
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsGameOver())
        {
            Show();

            recipesDeliveredText.text = "+" + DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
            recipesLostText.text = "-" + DeliveryManager.Instance.GetFailedRecipesAmount().ToString();
            recipesTotalText.text = DeliveryManager.Instance.GetTotalRecipesAmount().ToString();
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
