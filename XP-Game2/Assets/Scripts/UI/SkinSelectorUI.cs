using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkinSelectorUI : MonoBehaviour
{
    public event EventHandler OnSumSkinSelection;
    public event EventHandler OnSubSkinSelection;

    [SerializeField] private int playerSkinIndex;
    [SerializeField] private int playerTwoSkinIndex;
    [SerializeField] private TextMeshProUGUI playerSkinNameText;
    [SerializeField] private TextMeshProUGUI playerTwoSkinNameText;
    [SerializeField] private List<string> skinNamesList = new List<string>();
    [SerializeField] private Transform playerReadyMark;
    [SerializeField] private Transform playerTwoReadyMark;
    [SerializeField] private List<GameObject> headSkinsPlayerList = new List<GameObject>();
    [SerializeField] private List<GameObject> bodySkinsPlayerList = new List<GameObject>();
    [SerializeField] private List<GameObject> headSkinsPlayerTwoList = new List<GameObject>();
    [SerializeField] private List<GameObject> bodySkinsPlayerTwoList = new List<GameObject>();

    private void Start() 
    {
        GameInput.Instance.OnSumSkinSelectAction += GameInput_OnSumSkinSelectAction;
        GameInput.Instance.OnSubSkinSelectAction += GameInput_OnSubSkinSelectAction;

        GameInput.Instance.OnSumSkinSelectAction_2 += GameInput_OnSumSkinSelectAction_2;
        GameInput.Instance.OnSubSkinSelectAction_2 += GameInput_OnSubSkinSelectAction_2;

        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        KitchenGameManager.Instance.OnGetReady += KitchenGameManager_OnGetReady;

        Hide();
        SkinVisualChanger();
    }

    private void KitchenGameManager_OnGetReady(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.GetIsPlayerReady())
        {
            playerReadyMark.gameObject.SetActive(true);   
        }
        else
        {
            playerReadyMark.gameObject.SetActive(false); 
        }
        if(KitchenGameManager.Instance.GetIsPlayerTwoReady())
        {
            playerTwoReadyMark.gameObject.SetActive(true);   
        }
        else
        {
            playerTwoReadyMark.gameObject.SetActive(false); 
        }
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsSkinSelectionActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void GameInput_OnSumSkinSelectAction(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsSkinSelectionActive())
        {
            OnSumSkinSelection?.Invoke(this, EventArgs.Empty);

            playerSkinIndex++;
            PlayerIndexCheck();
            SkinVisualChanger();
        }
    }
    private void GameInput_OnSubSkinSelectAction(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsSkinSelectionActive())
        {
            OnSubSkinSelection?.Invoke(this, EventArgs.Empty);

            playerSkinIndex--;
            PlayerIndexCheck();
            SkinVisualChanger();
        }
    }
    private void GameInput_OnSumSkinSelectAction_2(object sender, System.EventArgs e)
    {
        
        if(KitchenGameManager.Instance.IsSkinSelectionActive())
        {
            playerTwoSkinIndex++;
            PlayerTwoIndexCheck();
            SkinVisualChanger();
        }
    }
    private void GameInput_OnSubSkinSelectAction_2(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsSkinSelectionActive())
        {
            playerTwoSkinIndex--;
            PlayerTwoIndexCheck();
            SkinVisualChanger();
        }
    }

    private void Update() 
    {
        // HandleSumSub();   
    }

    private void PlayerIndexCheck()
    {
        if(playerSkinIndex < 0)
            {
                playerSkinIndex = skinNamesList.Count - 1;
            }
            if(playerSkinIndex > skinNamesList.Count - 1)
            {
                playerSkinIndex = 0;
            }
    }
    private void PlayerTwoIndexCheck()
    {
        if(playerTwoSkinIndex < 0)
            {
                playerTwoSkinIndex = skinNamesList.Count - 1;
            }
            if(playerTwoSkinIndex > skinNamesList.Count - 1)
            {
                playerTwoSkinIndex = 0;
            }
    }

    private void SkinVisualChanger()
    {
        for (int i = 0; i < headSkinsPlayerList.Count; i++)
        {
            if(playerSkinIndex == i)
            {
                playerSkinNameText.text = skinNamesList[i];
                headSkinsPlayerList[i].SetActive(true);
                bodySkinsPlayerList[i].SetActive(true);
            }
            else
            {
                headSkinsPlayerList[i].SetActive(false);
                bodySkinsPlayerList[i].SetActive(false);
            }
        }
        
        for (int i = 0; i < headSkinsPlayerTwoList.Count; i++)
        {
            if(playerTwoSkinIndex == i)
            {
                playerTwoSkinNameText.text = skinNamesList[i];
                headSkinsPlayerTwoList[i].SetActive(true);
                bodySkinsPlayerTwoList[i].SetActive(true);
            }
            else
            {
                headSkinsPlayerTwoList[i].SetActive(false);
                bodySkinsPlayerTwoList[i].SetActive(false);
            }
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
