using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public IKitchenObjectParent KitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO; // Use lowercase "kitchenObjectSO"
    }
    public void SetKitchenObjectParent(IKitchenObjectParent KitchenObjectParent)
    {
        if(this.KitchenObjectParent != null)
        {
            this.KitchenObjectParent.ClearkitchenObject();
        }
        this.KitchenObjectParent = KitchenObjectParent;
        if(KitchenObjectParent.HaskitchenObject())
        {
            Debug.LogError("IKitchenObjectParent already has a kitchenObject!");
        }
        KitchenObjectParent.SetKitchenObject(this);

        transform.parent = KitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return KitchenObjectParent;
    }
    public void DestroySelf()
    {
        KitchenObjectParent.ClearkitchenObject();

        Destroy(gameObject);
    }
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if(this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {

        Transform KitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = KitchenObjectTransform.GetComponent<KitchenObject>();
        
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }
}