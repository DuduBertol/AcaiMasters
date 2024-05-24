using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance {get; private set;}

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    
    public event EventHandler OnGetReady;

    private enum State
    {
        WaitingToStart,
        SkinSelection,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    [SerializeField] private State state;

    [SerializeField] private Transform virtualCamera;
    [SerializeField] private Transform camPosGame;
    [SerializeField] private Transform camPosSkinSelector;
    [SerializeField] private float camSpeed;
    [SerializeField] private float gamePlayingTimerMax;
    private float gamePlayingTimer;
    private float countdownToStartTimer = 3f;
    private bool isGamePaused = false;
    [SerializeField] private bool isPlayerReady;
    [SerializeField] private bool isPlayerTwoReady;

    private void Awake() 
    {
        state = State.WaitingToStart;
        Instance = this;
    }

    private void Start() 
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnInteractAction_2 += GameInput_OnInteractAction_2;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(state == State.SkinSelection)
        {
            //Player Selecionou SKIN
            ToggleReadyPlayer();
            OnGetReady?.Invoke(this, EventArgs.Empty);

            if(isPlayerReady && isPlayerTwoReady)
            {
                state = State.CountdownToStart;
                OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
        } 

        if(state == State.WaitingToStart)
        {
            state = State.SkinSelection;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        } 
    }
    private void GameInput_OnInteractAction_2(object sender, System.EventArgs e)
    {
        if(state == State.SkinSelection)
        {
            //Player 2 Selecionou SKIN
            ToggleReadyPlayerTwo();
            OnGetReady?.Invoke(this, EventArgs.Empty);

            if(isPlayerReady && isPlayerTwoReady)
            {
                state = State.CountdownToStart;
                OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        if(state == State.WaitingToStart)
        {
            state = State.SkinSelection;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    
    private void GameInput_OnPauseAction(object sender, System.EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update() 
    {
        switch (state)
        {
            case State.WaitingToStart:
                break;
            case State.SkinSelection:
                virtualCamera.localPosition = Vector3.Slerp(virtualCamera.position, camPosSkinSelector.position, Time.deltaTime * camSpeed);
                break;
            case State.CountdownToStart:
                virtualCamera.position = Vector3.Slerp(virtualCamera.position, camPosGame.position, Time.deltaTime * camSpeed);
                countdownToStartTimer -= Time.deltaTime;
                if(countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if(gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                
                break;
        }    
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public bool IsSkinSelectionActive()
    {
        return state == State.SkinSelection;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if(isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
        
    }

    private void ToggleReadyPlayer()
    {
        isPlayerReady = !isPlayerReady;
    }
    private void ToggleReadyPlayerTwo()
    {
        isPlayerTwoReady = !isPlayerTwoReady;
    }
    public bool GetIsPlayerReady()
    {
        return isPlayerReady;
    }
    public bool GetIsPlayerTwoReady()
    {
        return isPlayerTwoReady;
    }
}
