using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    [SerializeField] private TextMeshProUGUI timerText;
    /* private string secondString;
    private string minuteString; */

    private void Start() 
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;    

        Hide();
    }

    private void Update() 
    {
        timerImage.fillAmount = KitchenGameManager.Instance.GetGamePlayingTimerNormalized();
        // GetTimerSecToMinutes();
    }

    /* public void GetTimerSecToMinutes()
    {
        float gamePlayingTimer = KitchenGameManager.Instance.GetGamePlayingTimerNormalized();
        
        int minutes = Mathf.RoundToInt(gamePlayingTimer / 60);
        int seconds = Mathf.FloorToInt(gamePlayingTimer - minutes * 60);

        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        timerText.text = niceTime;
    } */

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsGamePlaying())
        {
            Show();
        }
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
