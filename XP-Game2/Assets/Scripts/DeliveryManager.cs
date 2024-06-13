using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }



    public List<float> recipeLifeTimerList;
    public List<Transform> recipeLifeTimerBarList;
    public float recipeLifeTimerMax = 24f;


    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] private Transform recipeLifeTimerBarTemplate;



    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int successfulRecipesAmount;
    private int failedRecipesAmount;

    private void Awake() 
    {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();
        recipeLifeTimerList = new List<float>();
    }
    
    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(KitchenGameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

                waitingRecipeSOList.Add(waitingRecipeSO);

                recipeLifeTimerList.Add(recipeLifeTimerMax);
                

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }

        RecipeLifeTimerCountdown();
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
                    successfulRecipesAmount++;

                    waitingRecipeSOList.RemoveAt(i);
                    recipeLifeTimerList.RemoveAt(i);
                    recipeLifeTimerBarList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        //No matches found!
        //Player did not deliver a correct recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        failedRecipesAmount++;
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }
    public int GetFailedRecipesAmount()
    {
        return failedRecipesAmount;
    }
    public int GetTotalRecipesAmount()
    {
        return successfulRecipesAmount - failedRecipesAmount;
    }

    private void RecipeLifeTimerCountdown()
    {
        for (int i = 0; i < recipeLifeTimerList.Count; i++)
        {
            recipeLifeTimerList[i] -= Time.deltaTime;

            if(recipeLifeTimerList[i] <= 0f)
            {
                OnRecipeFailed?.Invoke(this, EventArgs.Empty);
                waitingRecipeSOList.RemoveAt(i);
                recipeLifeTimerList.RemoveAt(i);
                recipeLifeTimerBarList.RemoveAt(i);
            }

        }
    }

    
}
