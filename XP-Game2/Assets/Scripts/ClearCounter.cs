using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{


    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private void Update() 
    {
         
    }

    public override void Interact(Player player)
    {
        if(!HasKitchenObject())
        {
            //There is no KitchenObject here
            if(player.HasKitchenObject())
            {
                //Is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Player not carrying anything
            }
        }
        else
        {
            //There is a KitchenObject here
            if(player.HasKitchenObject())
            {
                //Player is carring something
            }
            else
            {
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
