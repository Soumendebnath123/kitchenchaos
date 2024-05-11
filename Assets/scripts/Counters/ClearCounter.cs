using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO KitchenObjectSO;
    public override void Interact(player Player)
    {      
      if(!HaskitchenObject())
        {
            //There is no KitchenObject here
            if(Player.HaskitchenObject ())
            {
                //Player is carrying something
                Player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Player not carrying anything
            }
        }
      else     
        {
            //There is KitchenObject here
            if(Player.HaskitchenObject ())
            {
                //Player is carrying something
                if (Player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    //Player is not carrying plate but something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))

                    {
                        //counter is holding a plate
                        if (plateKitchenObject.TryAddIngredient(Player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            Player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
                
            }
            else
            {
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(Player);
            }
        }
    }

  
}