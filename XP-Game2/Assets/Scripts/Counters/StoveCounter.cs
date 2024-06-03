using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs: EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private void Start() 
    {
        state = State.Idle;    
    }

    private void Update() 
    {
        if(HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });

                    if(fryingTimer >= fryingRecipeSO.fryingTimerMax)
                    {
                        //Fried

                        if(GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                        {   
                            plateKitchenObject.TryRemoveIngredient(fryingRecipeSO.input);
                            plateKitchenObject.TryAddIngredient(fryingRecipeSO.output);
                        }

                        KitchenObjectSO whichIsInPlateKitchenObject = plateKitchenObject.GetKitchenObjectSOList()[0];

                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(whichIsInPlateKitchenObject);

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                        });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
            
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });

                    if(burningTimer >= burningRecipeSO.burningTimerMax)
                    {
                        //Fried
                        if(GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                        {   
                            plateKitchenObject.TryRemoveIngredient(burningRecipeSO.input);
                            plateKitchenObject.TryAddIngredient(burningRecipeSO.output);
                        }

                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player)
    {
        
        if(!HasKitchenObject()) 
        //There is no KitchenObject here
        {
            if(player.HasKitchenObject()) 
            //Player is carrying something
            {
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) 
                //Player is holding a plate
                {
                    KitchenObjectSO whichIsInPlateKitchenObject = plateKitchenObject.GetKitchenObjectSOList()[0];

                    if(HasRecipeWithInput(whichIsInPlateKitchenObject)) 
                    //Player carrying something that can be Freezed (Acai)
                    {
                        player.GetKitchenObject().SetKitchenObjectParent(this);

                        
                        fryingRecipeSO = GetFryingRecipeSOWithInput(whichIsInPlateKitchenObject);

                        state = State.Frying;
                        fryingTimer = 0f;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state = state
                        });
                    }
                }
            }
            else
            {
                //Player not carrying anything
            }
        }
        else //There is a KitchenObject here
        {
            if(player.HasKitchenObject())
            {
                //Player is carring something
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is holding a plate
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                        state = State.Idle;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
            else
            {
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
        /* BACKUP
            //There is no KitchenObject here
            if(player.HasKitchenObject())
            {
                //Player is carrying something
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //Player carrying something that can be Fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                        state = state
                    });
                }
            } 
            else
            {
                //Player not carrying anything
            }
        }
        else //There is a KitchenObject here
        {
            if(player.HasKitchenObject())
            {
                //Player is carring something
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is holding a plate
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                        state = State.Idle;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
            else
            {
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }*/
    }

    public override void Interact_2(PlayerTwo playerTwo)
    {
        
        if(!HasKitchenObject())
        {
            //There is no KitchenObject here
            if(playerTwo.HasKitchenObject())
            {
                //Player is carrying something
                if(HasRecipeWithInput(playerTwo.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //Player carrying something that can be Fried
                    playerTwo.GetKitchenObject().SetKitchenObjectParent(this);

                    
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                        state = state
                    });
                }
            }
            else
            {
                //Player not carrying anything
            }
        }
        else
        {
            //There is a KitchenObject here
            if(playerTwo.HasKitchenObject())
            {
                //Player is carring something
                if(playerTwo.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is holding a plate
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                        state = State.Idle;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
            else
            {
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(playerTwo);

                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if(fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if(fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if(burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }

    public bool IsFried()
    {
        return state == State.Fried;
    }
}
