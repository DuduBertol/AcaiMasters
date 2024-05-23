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
                Destroy(child.gameObject);
            }
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {   
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);

            Transform recipeLifeTimerBar = Instantiate(recipeLifeTimerBarTemplate, recipeLifeTimerBarContainer);
            DeliveryManager.Instance.recipeLifeTimerBarList.Add(recipeLifeTimerBar);
            recipeLifeTimerBar.gameObject.SetActive(true);

            /* foreach(Transform child in recipeLifeTimerBarContainer)
            {
                if(child == recipeLifeTimerBarTemplate) continue;
                {
                    DeliveryManager.Instance.recipeLifeTimerBarList.Add(child);
                }
            } */
            
            /* for(int i = 0; i < DeliveryManager.Instance.recipeLifeTimerBarList.Count; i++)
            {
                if(DeliveryManager.Instance.recipeLifeTimerBarList[i] == null)
                {
                    DeliveryManager.Instance.recipeLifeTimerBarList.RemoveAt(i);
                }
            } */
        }

    }

    


    private void Update() 
    {
        for (int i = 0; i < DeliveryManager.Instance.recipeLifeTimerBarList.Count; i++)
        {
            Transform recipeLifeTimerBar = DeliveryManager.Instance.recipeLifeTimerBarList[i];
            float recipeLifeTimer = DeliveryManager.Instance.recipeLifeTimerList[i];
            float recipeLifeTimerMax = DeliveryManager.Instance.recipeLifeTimerMax;
            

            recipeLifeTimerBar.gameObject.GetComponent<Image>().fillAmount = recipeLifeTimer / recipeLifeTimerMax;

            if(recipeLifeTimer <= recipeLifeTimerMax) //100%
                {
                    if(recipeLifeTimer <= recipeLifeTimerMax * 2/3) //66%
                    {
                        if(recipeLifeTimer <= recipeLifeTimerMax * 1/3) //33%
                        {
                            recipeLifeTimerBar.GetComponent<Image>().color = red;
                            return;
                        }
                        recipeLifeTimerBar.GetComponent<Image>().color = yellow;
                        return;
                    }
                    recipeLifeTimerBar.GetComponent<Image>().color = green;
                    return;
                }
        }
    }
    
}
