using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoSounds : MonoBehaviour
{
    private PlayerTwo playerTwo;
    private float footStepTimer;
    private float footStepTimerMax = .1f;


    private void Awake() 
    {
        playerTwo = GetComponent<PlayerTwo>();
    }

    private void Update() 
    {
        footStepTimer -= Time.deltaTime;
        if(footStepTimer < 0f)
        {
            footStepTimer = footStepTimerMax;

            if(playerTwo.IsWalking())
            {
                float volume = 1f;
                SoundManager.Instance.PlayFootstepSound(playerTwo.transform.position, volume);
            }
            
        }    
    }
}
