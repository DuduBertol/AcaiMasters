using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    
    private void Start() 
    {
        PlayerOne.Instance.OnSelectedCounterChangedByPlayerOne += PlayerOne_OnSelectedCounterChangedByPlayerOne;
        PlayerTwo.Instance.OnSelectedCounterChangedByPlayerTwo += PlayerTwo_OnSelectedCounterChangedByPlayerTwo;
    }

    private void PlayerOne_OnSelectedCounterChangedByPlayerOne (object sender, PlayerOne.OnSelectedCounterChangedByPlayerOneEventArgs e)
    {
        if(e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            if(!PlayerOne.Instance.isSelecting && !PlayerTwo.Instance.isSelecting)
            {
                Hide();
            }
        }
    }

    private void PlayerTwo_OnSelectedCounterChangedByPlayerTwo (object sender, PlayerTwo.OnSelectedCounterChangedByPlayerTwoEventArgs e)
    {
        if(e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            if(!PlayerOne.Instance.isSelecting && !PlayerTwo.Instance.isSelecting)
            {
                Hide();
            }
        }
    }

    private void Show()
    {
        foreach(GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach(GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }

}
