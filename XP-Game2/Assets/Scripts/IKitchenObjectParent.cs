using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetKitchenObjetFollowTransform();
    public void SetKitchenObject(KitchenObject kitchenObject);
    public KitchenObject GetKitchenObject();
    public void ClearKitchenObject();
    public bool HasKitchenObject();
}
