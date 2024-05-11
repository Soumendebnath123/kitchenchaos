using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO KitchenObjectSO;
    public override void Interact(player Player)
    {
        if (!Player.HaskitchenObject())
        {
            //Player is not carrying anything
            KitchenObject.SpawnKitchenObject(KitchenObjectSO, Player);
           
            
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }


    }

    
}


