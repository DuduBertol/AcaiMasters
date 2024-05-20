using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisualPlayerTwo : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void Start() 
    {
        PlayerTwo.Instance.OnSelectedCounterChanged += PlayerTwo_OnSelectedCounterChanged;    
    }

    private void PlayerTwo_OnSelectedCounterChanged (object sender, PlayerTwo.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
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
