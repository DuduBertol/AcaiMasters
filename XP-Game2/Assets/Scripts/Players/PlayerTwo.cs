using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTwo : Player
{
    public event EventHandler<OnSelectedCounterChangedByPlayerTwoEventArgs> OnSelectedCounterChangedByPlayerTwo;
    public class OnSelectedCounterChangedByPlayerTwoEventArgs: EventArgs
    {
        public BaseCounter selectedCounter;
    }

    public static PlayerTwo Instance;

    public BaseCounter selectedCounter;
    public bool isSelecting;

    [SerializeField] private LayerMask countersLayerMask;

    private void Awake() 
    {
        Instance = this;    
    }

    public override void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalizedPlayerTwo();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        
        if (!canMove)
        {
            //Cannot move towards moveDir
            
            //Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                //Can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                //Cannot move only on the X

                //Attemp only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove =  (moveDir.z < -.5f || moveDir.z > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    //Can move only on the Z
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot move in any direction
                }
            }
        }

        if(canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public override void HandleInteraction()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalizedPlayerTwo();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if(moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if(Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //Has ClearCounter
                UnityEngine.Debug.Log("Player 1 - Find a Counter!");

                if(baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                    isSelecting = true;
                }
            }
            else
            {
                SetSelectedCounter(null);
                isSelecting = false;
            }
        }
        else
        {
            SetSelectedCounter(null);
            isSelecting = false;
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChangedByPlayerTwo?.Invoke(this, new OnSelectedCounterChangedByPlayerTwoEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}