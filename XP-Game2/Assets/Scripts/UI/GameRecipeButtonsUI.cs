using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRecipeButtonsUI : MonoBehaviour
{
    private const string FIRST_ATTEMPT = "FirstAttempt";
    [SerializeField] private Animator animator;
    

    private void Awake() 
    {
        
    }

    private void Start() 
    {
        GameInput.Instance.OnOpenRecipe += GameInput_OnOpenRecipe;    
        GameInput.Instance.OnOpenRecipe_2 += GameInput_OnOpenRecipe_2;
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;    
        Hide();  
    }
    
    private void GameInput_OnOpenRecipe(object sender, System.EventArgs e)
    {
        if(!KitchenGameManager.Instance.IsGamePlaying()) return;
        
        animator.SetTrigger(FIRST_ATTEMPT);
    }
    private void GameInput_OnOpenRecipe_2(object sender, System.EventArgs e)
    {
        if(!KitchenGameManager.Instance.IsGamePlaying()) return;
        
        animator.SetTrigger(FIRST_ATTEMPT);
    }
    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsGamePlaying())
        {
            Show();
        }
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
