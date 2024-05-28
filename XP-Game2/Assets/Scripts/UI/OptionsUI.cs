using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance {get; private set;}

    [Header("General")]
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    
    [SerializeField] private Transform pressToRebingKeyTransform;

    [Header("Player One")]
    //Button
    [SerializeField] private Button moveUpButton_1;
    [SerializeField] private Button moveDownButton_1;
    [SerializeField] private Button moveLeftButton_1;
    [SerializeField] private Button moveRightButton_1;
    [SerializeField] private Button interactButton_1;
    [SerializeField] private Button interactAlternateButton_1;
    [SerializeField] private Button dashButton_1;
    [SerializeField] private Button pauseButton_1;
    [SerializeField] private Button gamepadInteractButton_1;
    [SerializeField] private Button gamepadInteractAlternateButton_1;
    [SerializeField] private Button gamepadDashButton_1;
    [SerializeField] private Button gamepadPauseButton_1;

    //Text
    [SerializeField] private TextMeshProUGUI moveUpText_1;
    [SerializeField] private TextMeshProUGUI moveDownText_1;
    [SerializeField] private TextMeshProUGUI moveLeftText_1;
    [SerializeField] private TextMeshProUGUI moveRightText_1;
    [SerializeField] private TextMeshProUGUI interactText_1;
    [SerializeField] private TextMeshProUGUI interactAlternateText_1;
    [SerializeField] private TextMeshProUGUI dashText_1;
    [SerializeField] private TextMeshProUGUI pauseText_1;
    [SerializeField] private TextMeshProUGUI gamepadInteractText_1;
    [SerializeField] private TextMeshProUGUI gamepadInteractAlternateText_1;
    [SerializeField] private TextMeshProUGUI gamepadDashText_1;
    [SerializeField] private TextMeshProUGUI gamepadPauseText_1;



    [Header("Player Two")]
    //Button
    [SerializeField] private Button moveUpButton_2;
    [SerializeField] private Button moveDownButton_2;
    [SerializeField] private Button moveLeftButton_2;
    [SerializeField] private Button moveRightButton_2;
    [SerializeField] private Button interactButton_2;
    [SerializeField] private Button interactAlternateButton_2;
    [SerializeField] private Button dashButton_2;
    [SerializeField] private Button pauseButton_2;

    //Text
    [SerializeField] private TextMeshProUGUI moveUpText_2;
    [SerializeField] private TextMeshProUGUI moveDownText_2;
    [SerializeField] private TextMeshProUGUI moveLeftText_2;
    [SerializeField] private TextMeshProUGUI moveRightText_2;
    [SerializeField] private TextMeshProUGUI interactText_2;
    [SerializeField] private TextMeshProUGUI interactAlternateText_2;
    [SerializeField] private TextMeshProUGUI dashText_2;
    [SerializeField] private TextMeshProUGUI pauseText_2;
    


    private Action onCloseButtonAction;

    private void Awake() 
    {
        Instance = this;

        soundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });  
        closeButton.onClick.AddListener(() => {
            Hide();
            onCloseButtonAction();
        });

        //Player 1
        moveUpButton_1.onClick.AddListener(() => { RebingBinding(GameInput.Binding.Move_Up_1); });
        moveDownButton_1.onClick.AddListener(() => { RebingBinding(GameInput.Binding.Move_Down_1); });
        moveLeftButton_1.onClick.AddListener(() => { RebingBinding(GameInput.Binding.Move_Left_1); });
        moveRightButton_1.onClick.AddListener(() => { RebingBinding(GameInput.Binding.Move_Right_1); });
        interactButton_1.onClick.AddListener(() => { RebingBinding(GameInput.Binding.Interact_1); });
        interactAlternateButton_1.onClick.AddListener(() => { RebingBinding(GameInput.Binding.InteractAlternate_1); });
        dashButton_1.onClick.AddListener(() => { RebingBinding(GameInput.Binding.Dash_1); });
        pauseButton_1.onClick.AddListener(() => { RebingBinding(GameInput.Binding.Pause_1); });
        gamepadInteractButton_1.onClick.AddListener(() => { RebingBinding(GameInput.Binding.Gamepad_Interact_1); });
        gamepadInteractAlternateButton_1.onClick.AddListener(() => { RebingBinding(GameInput.Binding.Gamepad_InteractAlternate_1); });
        gamepadDashButton_1.onClick.AddListener(() => { RebingBinding(GameInput.Binding.Gamepad_Dash_1); });
        gamepadPauseButton_1.onClick.AddListener(() => { RebingBinding(GameInput.Binding.Gamepad_Pause_1); });

        //Player 2
        moveUpButton_2.onClick.AddListener(() => { RebingBinding(GameInput.Binding.Move_Up_2); });
        moveDownButton_2.onClick.AddListener(() => { RebingBinding(GameInput.Binding.Move_Down_2); });
        moveLeftButton_2.onClick.AddListener(() => { RebingBinding(GameInput.Binding.Move_Left_2); });
        moveRightButton_2.onClick.AddListener(() => { RebingBinding(GameInput.Binding.Move_Right_2); });
        interactButton_2.onClick.AddListener(() => { RebingBinding(GameInput.Binding.Interact_2); });
        interactAlternateButton_2.onClick.AddListener(() => { RebingBinding(GameInput.Binding.InteractAlternate_2); });
        dashButton_2.onClick.AddListener(() => { RebingBinding(GameInput.Binding.Dash_2); });
        pauseButton_2.onClick.AddListener(() => { RebingBinding(GameInput.Binding.Pause_2); });
    }

    private void Start() 
    {
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        Hide(); 

        UpdateVisual(); 

        HidePressToRebingKey();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();

    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        //Player 1
        moveUpText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up_1);
        moveDownText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down_1);
        moveLeftText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left_1);
        moveRightText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right_1);
        interactText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_1);
        interactAlternateText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate_1);
        dashText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.Dash_1);
        pauseText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause_1);
        gamepadInteractText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact_1);
        gamepadInteractAlternateText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate_1);
        gamepadDashText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Dash_1);
        gamepadPauseText_1.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause_1);

        //Player 2
        moveUpText_2.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up_2);
        moveDownText_2.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down_2);
        moveLeftText_2.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left_2);
        moveRightText_2.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right_2);
        interactText_2.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_2);
        interactAlternateText_2.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate_2);
        dashText_2.text = GameInput.Instance.GetBindingText(GameInput.Binding.Dash_2);
        pauseText_2.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause_2);
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;

        gameObject.SetActive(true);
        soundEffectsButton.Select();
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebingKey()
    {
        pressToRebingKeyTransform.gameObject.SetActive(true);
    }
    private void HidePressToRebingKey()
    {
        pressToRebingKeyTransform.gameObject.SetActive(false);
    }

    private void RebingBinding(GameInput.Binding binding)
    {
        ShowPressToRebingKey();
        GameInput.Instance.RebingBinding(binding, () => {
            HidePressToRebingKey();
            UpdateVisual();
        });
    }
}
