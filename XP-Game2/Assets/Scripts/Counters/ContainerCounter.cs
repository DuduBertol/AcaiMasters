using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter 
{

    public event EventHandler OnPlayerGrabbedObject;
    public event EventHandler OnPlayer2GrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {
            //Player is not carrying anything
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);            
            
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
        
        
    }
    public override void Interact_2(PlayerTwo playerTwo)
    {
        if(!playerTwo.HasKitchenObject())
        {
            //Player is not carrying anything
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, playerTwo);            
            
            OnPlayer2GrabbedObject?.Invoke(this, EventArgs.Empty);
        }
        
        
    }

}
