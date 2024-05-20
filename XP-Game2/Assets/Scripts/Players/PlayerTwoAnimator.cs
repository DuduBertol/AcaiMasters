using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoAnimator : MonoBehaviour
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
        PlayerTwo.Instance.OnDash += PlayerTwo_OnDash;
    }

    private void Update() 
    {
        animator.SetBool(IS_WALKING, PlayerTwo.Instance.IsWalking());
    }

    private void PlayerTwo_OnDash(object sender, System.EventArgs e)
    {
        
        dashParticles.Play();
    }
}
