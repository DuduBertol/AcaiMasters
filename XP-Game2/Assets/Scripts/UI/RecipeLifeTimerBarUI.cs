using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeLifeTimerBarUI : MonoBehaviour
{
    [SerializeField] private float recipeLifeTimer;
    [SerializeField] private float recipeLifeTimerMax;
    [SerializeField] private Color green;
    [SerializeField] private Color yellow;
    [SerializeField] private Color red;

    private void Awake() 
    {
        recipeLifeTimer = recipeLifeTimerMax;    
    }

    private void Update() 
    {
        recipeLifeTimer -= Time.deltaTime;
        UpdateBarVisual();
    }

    private void UpdateBarVisual()
    {
        if(recipeLifeTimer <= recipeLifeTimerMax) //100%
        {
            if(recipeLifeTimer <= recipeLifeTimerMax * 2/3) //66%
            {
                if(recipeLifeTimer <= recipeLifeTimerMax * 1/3) //33%
                {
                    // recipeLifeTimerBarUI.GetComponent<Image>().color = red;
                    return;
                }
                // recipeLifeTimerBarUI.GetComponent<Image>().color = yellow;
                return;
            }
            // recipeLifeTimerBarUI.GetComponent<Image>().color = green;
            return;
        }
        
        gameObject.GetComponent<Image>().fillAmount = recipeLifeTimer / recipeLifeTimerMax;
    }
}
