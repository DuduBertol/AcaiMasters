using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeUI : MonoBehaviour
{
    [SerializeField] private Transform recipeTutorialTransfom;
    [SerializeField] private Transform backgroundTransfom;
    [SerializeField] private bool isEnable;

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        GameInput.Instance.OnOpenRecipe += GameInput_OnOpenRecipe;
        GameInput.Instance.OnOpenRecipe_2 += GameInput_OnOpenRecipe_2;

    }

    private void GameInput_OnOpenRecipe(object sender, System.EventArgs e)
    {
        if(!KitchenGameManager.Instance.IsGamePlaying()) return;

        isEnable =! isEnable;
        if(isEnable)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void GameInput_OnOpenRecipe_2(object sender, System.EventArgs e)
    {
        if(!KitchenGameManager.Instance.IsGamePlaying()) return;

        isEnable =! isEnable;
        if(isEnable)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsRecipeTutorialActive())
        {
            Show();
        }
        if(KitchenGameManager.Instance.IsSkinSelectionActive())
        {
            Hide();
        }
    }
    private void Show()
    {
        LeanTween.moveY(recipeTutorialTransfom.GetComponent<RectTransform>(), 0, 0.5f).setEaseOutBack();
        backgroundTransfom.gameObject.SetActive(true);
    }
    private void Hide()
    {
        LeanTween.moveY(recipeTutorialTransfom.GetComponent<RectTransform>(), -1000, 0.5f).setEaseOutBack();
        backgroundTransfom.gameObject.SetActive(false);
    }

}
