using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public event EventHandler OnButtonClick;

    [SerializeField] private Transform controlTutorialImageTransform; 
    [SerializeField] private Transform keyboardTutorialP1ImageTransform; 
    [SerializeField] private Transform keyboardTutorialP2ImageTransform; 

    [Header("Player 1")]
    [SerializeField] private TextMeshProUGUI keyMoveUpText_1;
    [SerializeField] private TextMeshProUGUI keyMoveDownText_1;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText_1;
    [SerializeField] private TextMeshProUGUI keyMoveRightText_1;
    [SerializeField] private TextMeshProUGUI keyInteractText_1;
    [SerializeField] private TextMeshProUGUI keyInteractAlternateText_1;
    [SerializeField] private TextMeshProUGUI keyDashText_1;
    [SerializeField] private TextMeshProUGUI keyOpenRecipeText_1;
    
    [Header("Player 2")]
    [SerializeField] private TextMeshProUGUI keyMoveUpText_2;
    [SerializeField] private TextMeshProUGUI keyMoveDownText_2;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText_2;
    [SerializeField] private TextMeshProUGUI keyMoveRightText_2;
    [SerializeField] private TextMeshProUGUI keyInteractText_2;
    [SerializeField] private TextMeshProUGUI keyInteractAlternateText_2;
    [SerializeField] private TextMeshProUGUI keyDashText_2;
    [SerializeField] private TextMeshProUGUI keyOpenRecipeText_2;

    private void Start() 
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        
        UpdateVisual();
        
        LeanTween.moveY(controlTutorialImageTransform.GetComponent<RectTransform>(), -340, 0.5f).setEaseOutBack();
        LeanTween.moveY(keyboardTutorialP1ImageTransform.GetComponent<RectTransform>(), 125, 0.5f).setEaseOutBack();
        LeanTween.moveY(keyboardTutorialP2ImageTransform.GetComponent<RectTransform>(), 125, 0.5f).setEaseOutBack();
        Show();    
    }
    

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsRecipeTutorialActive())
        {
            Hide();
            OnButtonClick?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }
    
    private void UpdateVisual() 
    {
        keyMoveUpText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up_1);    
        keyMoveDownText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down_1);    
        keyMoveLeftText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left_1);    
        keyMoveRightText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right_1);    
        keyInteractText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_1);    
        keyInteractAlternateText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate_1);    
        keyDashText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.Dash_1);   
        keyOpenRecipeText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.OpenRecipe_1);   
        
        keyMoveUpText_2.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up_2);    
        keyMoveDownText_2.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down_2);    
        keyMoveLeftText_2.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left_2);    
        keyMoveRightText_2.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right_2);    
        keyInteractText_2.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_2);    
        keyInteractAlternateText_2.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate_2);    
        keyDashText_2.text = GameInput.Instance.GetBindingText(GameInput.Binding.Dash_2);   
        keyOpenRecipeText_2.text = GameInput.Instance.GetBindingText(GameInput.Binding.OpenRecipe_2);   
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
