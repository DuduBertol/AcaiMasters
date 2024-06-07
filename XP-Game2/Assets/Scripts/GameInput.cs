using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public static GameInput Instance {get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnDashAction;
    public event EventHandler OnInteractAction_2;
    public event EventHandler OnInteractAlternateAction_2;
    public event EventHandler OnDashAction_2;
    public event EventHandler OnOpenRecipe;
    public event EventHandler OnOpenRecipe_2;
    public event EventHandler OnPauseAction;
    public event EventHandler OnSubSkinSelectAction;
    public event EventHandler OnSumSkinSelectAction;
    public event EventHandler OnSubSkinSelectAction_2;
    public event EventHandler OnSumSkinSelectAction_2;
    
    public event EventHandler OnBindingRebind;


    public enum Binding
    {
        //Player 1
        Move_Up_1,
        Move_Down_1,
        Move_Left_1,
        Move_Right_1,
        Interact_1,
        InteractAlternate_1,
        Dash_1,
        Pause_1,
        // Gamepad_Interact_1,
        // Gamepad_InteractAlternate_1,
        // Gamepad_Pause_1,
        // Gamepad_Dash_1,

        //Player 2
        Move_Up_2,
        Move_Down_2,
        Move_Left_2,
        Move_Right_2,
        Interact_2,
        InteractAlternate_2,
        Dash_2,
        Pause_2,
    }



    private PlayerInputActions playerInputActions;

    private void Awake() 
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();

        if(PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Dash.performed += Dash_performed;
        playerInputActions.Player.OpenRecipe.performed += OpenRecipe_performed;
        playerInputActions.Player.SumSkinSelect.performed += SumSkinSelect_performed;
        playerInputActions.Player.SubSkinSelect.performed += SubSkinSelect_performed;

        playerInputActions.Player.Pause.performed += Pause_performed;

        playerInputActions.Player.Interact_2.performed += Interact_performed_2;
        playerInputActions.Player.InteractAlternate_2.performed += InteractAlternate_performed_2;
        playerInputActions.Player.Dash_2.performed += Dash_performed_2;
        playerInputActions.Player.OpenRecipe_2.performed += OpenRecipe_performed_2;
        playerInputActions.Player.SumSkinSelect_2.performed += SumSkinSelect_performed_2;
        playerInputActions.Player.SubSkinSelect_2.performed += SubSkinSelect_performed_2;
    
    }

    private void OnDestroy() 
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Dash.performed -= Dash_performed;
        playerInputActions.Player.OpenRecipe.performed -= OpenRecipe_performed;
        playerInputActions.Player.SumSkinSelect.performed -= SumSkinSelect_performed;
        playerInputActions.Player.SubSkinSelect.performed -= SubSkinSelect_performed;

        playerInputActions.Player.Pause.performed -= Pause_performed;

        playerInputActions.Player.Interact_2.performed -= Interact_performed_2;
        playerInputActions.Player.InteractAlternate_2.performed -= InteractAlternate_performed_2;
        playerInputActions.Player.Dash_2.performed -= Dash_performed_2;
        playerInputActions.Player.OpenRecipe_2.performed -= OpenRecipe_performed_2;
        playerInputActions.Player.SumSkinSelect_2.performed -= SumSkinSelect_performed_2;
        playerInputActions.Player.SubSkinSelect_2.performed -= SubSkinSelect_performed_2;

        playerInputActions.Dispose();
    }

    
    private void OpenRecipe_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnOpenRecipe?.Invoke(this, EventArgs.Empty);
    }
    private void OpenRecipe_performed_2(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnOpenRecipe_2?.Invoke(this, EventArgs.Empty);
    }
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }
    private void Dash_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnDashAction?.Invoke(this, EventArgs.Empty);
    }
    private void SumSkinSelect_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSumSkinSelectAction?.Invoke(this, EventArgs.Empty);
    }
    private void SubSkinSelect_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSubSkinSelectAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void SumSkinSelect_performed_2(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSumSkinSelectAction_2?.Invoke(this, EventArgs.Empty);
    }
    private void SubSkinSelect_performed_2(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSubSkinSelectAction_2?.Invoke(this, EventArgs.Empty);
    }

    private void Dash_performed_2(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnDashAction_2?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed_2(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction_2?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed_2(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction_2?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalizedPlayerOne()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
    public Vector2 GetMovementVectorNormalizedPlayerTwo()
    {
        Vector2 inputVector = playerInputActions.Player.Move_2.ReadValue<Vector2>();
 
        inputVector = inputVector.normalized;

        return inputVector;
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            //Player 1
            case Binding.Move_Up_1:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down_1:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left_1:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right_1:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString(); 
            case Binding.Interact_1:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate_1:
                return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Dash_1:
                return playerInputActions.Player.Dash.bindings[0].ToDisplayString();
            case Binding.Pause_1:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
            // case Binding.Gamepad_Interact_1:
            //     return playerInputActions.Player.Interact.bindings[1].ToDisplayString();
            // case Binding.Gamepad_InteractAlternate_1:
            //     return playerInputActions.Player.InteractAlternate.bindings[1].ToDisplayString();
            // case Binding.Gamepad_Dash_1:
            //     return playerInputActions.Player.Dash.bindings[1].ToDisplayString();
            // case Binding.Gamepad_Pause_1:
            //     return playerInputActions.Player.Pause.bindings[1].ToDisplayString();

            //Player 2
            case Binding.Move_Up_2:
                return playerInputActions.Player.Move_2.bindings[1].ToDisplayString();
            case Binding.Move_Down_2:
                return playerInputActions.Player.Move_2.bindings[2].ToDisplayString();
            case Binding.Move_Left_2:
                return playerInputActions.Player.Move_2.bindings[3].ToDisplayString();
            case Binding.Move_Right_2:
                return playerInputActions.Player.Move_2.bindings[4].ToDisplayString(); 
            case Binding.Interact_2:
                return playerInputActions.Player.Interact_2.bindings[0].ToDisplayString();
            case Binding.InteractAlternate_2:
                return playerInputActions.Player.InteractAlternate_2.bindings[0].ToDisplayString();
            case Binding.Dash_2:
                return playerInputActions.Player.Dash_2.bindings[0].ToDisplayString();
            case Binding.Pause_2:
                return playerInputActions.Player.Pause_2.bindings[0].ToDisplayString();
        }
    }

    public void RebingBinding(Binding binding, Action onActionRebound)
    {
        playerInputActions.Player.Disable();
        

        InputAction inputAction;
        int bindingIndex;
        switch (binding)
        {
            default:
            //Player 1
            case Binding.Move_Up_1:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down_1:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left_1:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right_1:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact_1:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate_1:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Dash_1:
                inputAction = playerInputActions.Player.Dash;
                bindingIndex = 0;
                break;
            case Binding.Pause_1:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
            // case Binding.Gamepad_Interact_1:
            //     inputAction = playerInputActions.Player.Interact;
            //     bindingIndex = 1;
            //     break;
            // case Binding.Gamepad_InteractAlternate_1:
            //     inputAction = playerInputActions.Player.InteractAlternate;
            //     bindingIndex = 1;
            //     break;
            // case Binding.Gamepad_Dash_1:
            //     inputAction = playerInputActions.Player.Dash;
            //     bindingIndex = 1;
            //     break;
            // case Binding.Gamepad_Pause_1:
            //     inputAction = playerInputActions.Player.Pause;
            //     bindingIndex = 1;
            //     break;

            //Player 2
            case Binding.Move_Up_2:
                inputAction = playerInputActions.Player.Move_2;
                bindingIndex = 1;
                break;
            case Binding.Move_Down_2:
                inputAction = playerInputActions.Player.Move_2;
                bindingIndex = 2;
                break;
            case Binding.Move_Left_2:
                inputAction = playerInputActions.Player.Move_2;
                bindingIndex = 3;
                break;
            case Binding.Move_Right_2:
                inputAction = playerInputActions.Player.Move_2;
                bindingIndex = 4;
                break;
            case Binding.Interact_2:
                inputAction = playerInputActions.Player.Interact_2;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate_2:
                inputAction = playerInputActions.Player.InteractAlternate_2;
                bindingIndex = 0;
                break;
            case Binding.Dash_2:
                inputAction = playerInputActions.Player.Dash_2;
                bindingIndex = 0;
                break;
            case Binding.Pause_2:
                inputAction = playerInputActions.Player.Pause_2;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback => {
                callback.Dispose();
                playerInputActions.Player.Enable();
                onActionRebound();

                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();

                OnBindingRebind?.Invoke(this, EventArgs.Empty);
            })
            .Start();
    }

}
