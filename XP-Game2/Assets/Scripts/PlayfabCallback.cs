using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayfabCallback : MonoBehaviour
{
    private float timer;
    private float timerMax = 2f;

    private void Update() 
    {
        timer += Time.deltaTime;

        if(timer > timerMax)
        {
            Debug.Log("Callback Leaderboard!");
            PlayfabManager.Instance.GetLeaderboard();
            Destroy(this.gameObject);
        }    
    }
}
