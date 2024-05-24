using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;
    [SerializeField] private Transform recipeLifeTimerBarTemplate;
    [SerializeField] private Transform recipeLifeTimerBarContainer;
    [SerializeField] private Color green;
    [SerializeField] private Color yellow;
    [SerializeField] private Color red;

    private void Awake() 
    {
        recipeTemplate.gameObject.SetActive(false);  
        recipeLifeTimerBarTemplate.gameObject.SetActive(false);  
    }

    private void Start() 
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }
    private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void Update() 
    {
        UpdateTimer();   
    }


    private void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            if(child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach(Transform child in recipeLifeTimerBarContainer)
        {
            if(child == recipeLifeTimerBarTemplate) continue;
            {
                DeliveryManager.Instance.recipeLifeTimerBarList.Remove(child);
                Destroy(child.gameObject);
            }
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {   
            //Recipe
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);

            //Timer
            Transform recipeLifeTimerBar = Instantiate(recipeLifeTimerBarTemplate, recipeLifeTimerBarContainer);
            DeliveryManager.Instance.recipeLifeTimerBarList.Add(recipeLifeTimerBar);
            recipeLifeTimerBar.gameObject.SetActive(true);
        }

    }

    private void UpdateTimer()
    {
        for (int i = 0; i < DeliveryManager.Instance.recipeLifeTimerBarList.Count; i++)
        {
            float recipeLifeTimerMax = DeliveryManager.Instance.recipeLifeTimerMax;
            
            DeliveryManager.Instance.recipeLifeTimerBarList[i].gameObject.GetComponent<Image>().fillAmount = DeliveryManager.Instance.recipeLifeTimerList[i] / recipeLifeTimerMax;

            if(DeliveryManager.Instance.recipeLifeTimerList[i] <= recipeLifeTimerMax) //100%
                {
                    if(DeliveryManager.Instance.recipeLifeTimerList[i] <= recipeLifeTimerMax * 2/3) //66%
                    {
                        if(DeliveryManager.Instance.recipeLifeTimerList[i] <= recipeLifeTimerMax * 1/3) //33%
                        {
                            DeliveryManager.Instance.recipeLifeTimerBarList[i].GetComponent<Image>().color = red;
                            continue;
                        }
                        DeliveryManager.Instance.recipeLifeTimerBarList[i].GetComponent<Image>().color = yellow;
                        continue;
                    }
                    DeliveryManager.Instance.recipeLifeTimerBarList[i].GetComponent<Image>().color = green;
                    continue;
                }
        }
    }
    
}
