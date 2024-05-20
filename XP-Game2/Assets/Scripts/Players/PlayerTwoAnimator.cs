using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private PlayerTwo playerTwo
; 
    private Animator animator;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
    }

    private void Update() 
    {
        animator.SetBool(IS_WALKING, playerTwo.IsWalking());
    }
}
