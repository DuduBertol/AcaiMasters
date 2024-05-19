using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    //Static pertence a classe
    //public static Player Instance { get; private set; }


    
    [SerializeField] private Transform kitchenObjectHoldPoint;

    //private LayerMask countersLayerMask;
    public float moveSpeed = 7f;
    public bool isWalking;
    public Vector3 lastInteractDir;
    // public BaseCounter selectedCounter;
    
    private KitchenObject kitchenObject;

    private void Awake() 
    {
        /* if(Instance != null)
        {
            UnityEngine.Debug.Log("There is more than one Player instance");
        }
        Instance = this;  */   
    }


    private void Start() 
    {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e)
    {
        if(!KitchenGameManager.Instance.IsGamePlaying()) return;

        // if(selectedCounter != null)
        {
            // selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(!KitchenGameManager.Instance.IsGamePlaying()) return;
        
        // if(selectedCounter != null)
        {
            // selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public virtual void HandleInteraction()
    {
        
    }

    public virtual void HandleMovement()
    {
        
    }

    public Transform GetKitchenObjetFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public virtual void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
