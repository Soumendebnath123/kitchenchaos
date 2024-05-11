using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour,IKitchenObjectParent
{

    public static event EventHandler OnAnyObjectPlacedHere;
    

    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }
    [SerializeField] private Transform CounterTopPoint;

    private KitchenObject kitchenObject;
    public virtual void Interact(player Player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }
    public virtual void InteractAlternate(player Player)
    {
       // Debug.LogError("BaseCounter.InteractAlternate();");ss
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return CounterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearkitchenObject()
    {
        kitchenObject = null;
    }
    public bool HaskitchenObject()
    {
        return kitchenObject != null;
    }
}