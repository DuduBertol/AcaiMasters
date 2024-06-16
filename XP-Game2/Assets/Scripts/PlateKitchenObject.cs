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
    public event EventHandler<OnIngredientRemovedEventArgs> OnIngredientRemoved;
    public class OnIngredientRemovedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }


    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    public KitchenObjectSO firstStepKitchenObject; // Acai Fruta
    [SerializeField] private KitchenObjectSO secondStepKitchenObject; //Acai Gelado
    [SerializeField] private KitchenObjectSO thirdStepKitchenObject; //Acai Congelado

    [SerializeField] private PlateCompleteVisual bowlCompleteVisual;
    [SerializeField] private PlateIconsUI plateIconsUI;
    [SerializeField] private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake() 
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();    
    }


    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if(kitchenObjectSO != firstStepKitchenObject)
        {
            if(kitchenObjectSOList.Contains(secondStepKitchenObject))
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
            else if(kitchenObjectSO == secondStepKitchenObject || kitchenObjectSO == thirdStepKitchenObject)
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
            if(kitchenObjectSOList.Contains(firstStepKitchenObject) || kitchenObjectSOList.Contains(secondStepKitchenObject) || kitchenObjectSOList.Contains(thirdStepKitchenObject))
            {
                return false;
            }
            else
            {
                if(kitchenObjectSO == firstStepKitchenObject)
                {
                    kitchenObjectSOList.Add(kitchenObjectSO);

                    OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
                    {
                        kitchenObjectSO = kitchenObjectSO
                    });
                    if(OnIngredientAdded == null)
                    {
                        bowlCompleteVisual.AddPlateToAcai(); //ADICIONAR VISUAL
                        plateIconsUI.AddPlateToAcai();//ADICIONAR ICON
                    }
                    return true;
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
        }
    }

    public void TryRemoveIngredient(KitchenObjectSO kitchenObjectSO)
    {
        kitchenObjectSOList.Remove(kitchenObjectSO);

        OnIngredientRemoved?.Invoke(this, new OnIngredientRemovedEventArgs
        {
            kitchenObjectSO = kitchenObjectSO
        });
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
