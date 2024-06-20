using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    public event EventHandler OnFirstPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    [SerializeField] private KitchenObjectSO firstStepKitchenObjectSO;

    private float spawnPlateTimer;
    [SerializeField] private float spawnPlateTimerMax;
    private int platesSpawnedAmount;
    [SerializeField] private int platesSpawnedAmountMax;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;

            if(KitchenGameManager.Instance.IsGamePlaying() && platesSpawnedAmount < platesSpawnedAmountMax)
            {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {
            //Player is empty handed
            if(platesSpawnedAmount > 0)
            {
                //There's at least one plate here
                platesSpawnedAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);

                if(KitchenGameManager.Instance.GetWasPlayerFirstInteractedAcaiFruit() && !KitchenGameManager.Instance.GetWasPlayerFirstInteractedBowl())
                {
                    KitchenGameManager.Instance.SetWasPlayerFirstInteractedBowl(true);
                    OnFirstPlateRemoved?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        
        if(player.HasKitchenObject())
        {
            if(player.GetKitchenObject().GetKitchenObjectSO() == firstStepKitchenObjectSO) //Has Acai
            {
                if(platesSpawnedAmount > 0)
                {
                    //There's at least one plate here
                    platesSpawnedAmount--;
                    OnPlateRemoved?.Invoke(this, EventArgs.Empty);

                    if(KitchenGameManager.Instance.GetWasPlayerFirstInteractedAcaiFruit() && !KitchenGameManager.Instance.GetWasPlayerFirstInteractedBowl())
                    {
                        KitchenGameManager.Instance.SetWasPlayerFirstInteractedBowl(true);
                        OnFirstPlateRemoved?.Invoke(this, EventArgs.Empty);
                    }

                    player.GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                    
                    if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) 
                    { 
                        //K.O. é um Prato
                        plateKitchenObject.TryAddIngredient(firstStepKitchenObjectSO);
                    }
                }
            }
        }
    }

    public override void Interact_2(PlayerTwo playerTwo)
    {
        if(!playerTwo.HasKitchenObject())
        {
            //Player is empty handed
            if(platesSpawnedAmount > 0)
            {
                //There's at least one plate here
                platesSpawnedAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, playerTwo);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);

                if(KitchenGameManager.Instance.GetWasPlayerFirstInteractedAcaiFruit() && !KitchenGameManager.Instance.GetWasPlayerFirstInteractedBowl())
                {
                    KitchenGameManager.Instance.SetWasPlayerFirstInteractedBowl(true);
                    OnFirstPlateRemoved?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        if(playerTwo.HasKitchenObject())
        {
            if(playerTwo.GetKitchenObject().GetKitchenObjectSO() == firstStepKitchenObjectSO) //Has Acai
            {
                if(platesSpawnedAmount > 0)
                {
                    //There's at least one plate here
                    platesSpawnedAmount--;
                    OnPlateRemoved?.Invoke(this, EventArgs.Empty);

                    if(KitchenGameManager.Instance.GetWasPlayerFirstInteractedAcaiFruit() && !KitchenGameManager.Instance.GetWasPlayerFirstInteractedBowl())
                    {
                        KitchenGameManager.Instance.SetWasPlayerFirstInteractedBowl(true);
                        OnFirstPlateRemoved?.Invoke(this, EventArgs.Empty);
                    }

                    playerTwo.GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, playerTwo);
                    
                    if(playerTwo.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) 
                    { 
                        //K.O. é um Prato
                        plateKitchenObject.TryAddIngredient(firstStepKitchenObjectSO);
                    }
                }
            }
        }
    }
}
