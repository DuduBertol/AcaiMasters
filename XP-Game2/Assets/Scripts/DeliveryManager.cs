using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;

    private void Awake() 
    {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();    
    }
    
    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //Has the same number of ingredients
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    //Cycling through all ingredients in the Recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    { 
                        //Cycling through all ingredients in the Plate
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            //Ingredient matches!
                            ingredientFound = true;
                            break; //vai parar o foreach aqui (independente de onde estiver na List)
                        }
                    }
                    if(!ingredientFound)
                    {
                        //This Recipe ingredient was not found on the Plate
                        plateContentsMatchesRecipe = false;

                    }
                }
                if(plateContentsMatchesRecipe)
                {
                    //Player delivered the correct recipe!
                    Debug.Log("Player delivered the correct recipe!");
                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);

                    return;
                }
            }
        }
        //No matches found!
        //Player did not deliver a correct recipe
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }
}
