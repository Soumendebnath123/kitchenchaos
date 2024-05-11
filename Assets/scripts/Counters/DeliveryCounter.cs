using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{

    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public override void Interact(player Player)
    {
        if( Player .HaskitchenObject())
        {
            if(Player .GetKitchenObject ().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                //Only accept plates

                DeliveryManager.Instance.DeliveryRecipe(plateKitchenObject);
                Player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
