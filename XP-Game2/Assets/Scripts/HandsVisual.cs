using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandsVisual : MonoBehaviour
{
    public const string ON_PICKUP = "OnPickup";
    public const string ON_DROP = "OnDrop";

    
    public enum ChoosePlayer
    {
        Player,
        PlayerTwo,
    }
    [SerializeField] ChoosePlayer choosePlayer;
    
    private Animator animator;

    private void Awake() 
    {
        animator = GetComponent<Animator>();    
    }

    private void Start() 
    {
        // GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        // GameInput.Instance.OnInteractAction_2 += GameInput_OnInteractAction_2;

        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        PlayerTwo.Instance.OnPickedSomething += PlayerTwo_OnPickedSomething;

        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
    }

    private void BaseCounter_OnAnyObjectPlacedHere (object sender, System.EventArgs e)
    {
        if(choosePlayer == ChoosePlayer.Player)
        {
            animator.SetTrigger(ON_DROP);
        }
        else if (choosePlayer == ChoosePlayer.PlayerTwo)
        {
            animator.SetTrigger(ON_DROP);
        }
    }

    private void Player_OnPickedSomething (object sender, System.EventArgs e)
    {
        if(choosePlayer == ChoosePlayer.Player)
        {
            animator.SetTrigger(ON_PICKUP);
        }
    }
    private void PlayerTwo_OnPickedSomething (object sender, System.EventArgs e)
    {
        if(choosePlayer == ChoosePlayer.PlayerTwo)
        {
            animator.SetTrigger(ON_PICKUP);
        }
    }

}
