using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] ParticleSystem dashParticles; 
    private Animator animator;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
    }

    private void Start() 
    {
        Player.Instance.OnDash += Player_OnDash;
    }

    private void Update() 
    {
        animator.SetBool(IS_WALKING, Player.Instance.IsWalking());
    }

    private void Player_OnDash(object sender, System.EventArgs e)
    {
        dashParticles.Play();
    }
}
