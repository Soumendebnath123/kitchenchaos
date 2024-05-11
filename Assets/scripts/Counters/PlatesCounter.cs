using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter

{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 3f;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4; 

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
    public override void Interact(player Player)
    {
        if(!Player.HaskitchenObject())
        {
            //player is empty handed
            if(platesSpawnedAmount > 0)
            {
                //There at least one plate here
                platesSpawnedAmount --;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, Player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
