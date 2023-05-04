using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement.OnStartWalking += PlayerMovement_OnStartWalking;
        playerMovement.OnStopWalking += PlayerMovement_OnStopWalking;
    }

    private void PlayerMovement_OnStopWalking(object sender, EventArgs e)
    {
        animator.SetBool("IsWalking", false);
    }

    private void PlayerMovement_OnStartWalking(object sender, EventArgs e)
    {
        animator.SetBool("IsWalking", true);
    }
}
