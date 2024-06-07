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
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;    
        GameInput.Instance.OnInteractAction_2 += GameInput_OnInteractAction_2;    
    }
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(!KitchenGameManager.Instance.IsGamePlaying()) return;
        
        animator.SetTrigger(FIRST_ATTEMPT);
    }
    private void GameInput_OnInteractAction_2(object sender, System.EventArgs e)
    {
        if(!KitchenGameManager.Instance.IsGamePlaying()) return;
        
        animator.SetTrigger(FIRST_ATTEMPT);
    }
}
