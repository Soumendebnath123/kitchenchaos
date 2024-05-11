using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private player Player;
    private float footstepTimer;
    private float footstepTimerMax = .1f;

    private void Awake()
    {
        Player = GetComponent<player>();
    }
    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;

            if (Player.IsWalking())
            {

                float volume = 1f;
                SoundManager.Instance.PlayFootstepsSound(Player.transform.position, volume);
            }
        }
    }
}
