using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }


    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    [SerializeField] private KitchenObjectSO requiredKitchenObject; //Acai Gelado
    [SerializeField] private KitchenObjectSO uniqueKitchenObject; // Acai Fruta

    [SerializeField] private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake() 
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();    
    }


    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if(kitchenObjectSO != uniqueKitchenObject)
        {
            if(kitchenObjectSOList.Contains(requiredKitchenObject))
            {
                if(!validKitchenObjectSOList.Contains(kitchenObjectSO))
                {
                    //Not a valid ingredient
                    return false;
                }
                if(kitchenObjectSOList.Contains(kitchenObjectSO))
                {
                    //Already has this type
                    return false;
                }
                else
                {
                    kitchenObjectSOList.Add(kitchenObjectSO);

                    OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
                    {
                        kitchenObjectSO = kitchenObjectSO
                    });

                    return true;
                }
            }
            else if(kitchenObjectSO == requiredKitchenObject)
            {
                kitchenObjectSOList.Add(kitchenObjectSO);

                OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
                {
                    kitchenObjectSO = kitchenObjectSO
                });

                return true;
            }
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);

                OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
                {
                    kitchenObjectSO = kitchenObjectSO
                });

                return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
