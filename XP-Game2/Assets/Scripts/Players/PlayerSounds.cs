using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float footStepTimer;
    private float footStepTimerMax = .1f;


    private void Awake() 
    {
        player = GetComponent<Player>();
    }

    private void Start() 
    {
        Player.Instance.OnDash += Player_OnDash;    
    }

    private void Update() 
    {
        footStepTimer -= Time.deltaTime;
        if(footStepTimer < 0f)
        {
            footStepTimer = footStepTimerMax;

            if(player.IsWalking())
            {
                float volume = 0.25f;
                SoundManager.Instance.PlayFootstepSound(SoundManager.Instance.transform.position, volume);//player.transform.position, volume);
            }
            
        }    
    }
    private void Player_OnDash(object sender, System.EventArgs e)
    {
        if(player.IsWalking())
        {
            float volume = 1f;
            SoundManager.Instance.PlayDashSound(SoundManager.Instance.transform.position, volume);//player.transform.position, volume);
        }
    }
}
