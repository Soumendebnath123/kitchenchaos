using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeranimator : MonoBehaviour
{
    private const string IS_WALKING = "Iswalking";

    [SerializeField] private player player;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }
    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
