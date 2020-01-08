using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: make EntityAnimationController, and enemy/player derive from it
public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;
    private MovementController enemyMovement;
    private HealthSystem healthSystem;
    private IAttack enemyCombat;

    private void Awake()
    {
        //Get Components
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<MovementController>();
        healthSystem = GetComponent<HealthSystem>();
        enemyCombat = GetComponent<BasicEnemyAI>();

        SubscribeListeners();
    }

    private void SubscribeListeners()
    {
        //Movement Listeners
        enemyMovement.onJumpEvent += onJump;
        enemyMovement.onMoveEvent += onMove;
        enemyMovement.onCrouchEvent += onCrouch;
        //Combat Listeners
        enemyCombat.onAttackEvent += onAttack;
        //Health Listeners
        healthSystem.onDamageTaken += onDamageTaken;
        healthSystem.onDeath += onDeath;
    }

    #region Events
    private void onDeath()
    {
        animator.SetTrigger("onDeath");
    }

    private void onDamageTaken()
    {
        animator.SetTrigger("onDamageTaken");
    }

    private void onAttack()
    {
        animator.SetTrigger("onAttack");
    }

    private void onCrouch()
    {
        animator.SetTrigger("onCrouch");
    }

    private void onMove(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    private void onJump(bool isJumping)
    {
        animator.SetBool("isJumping", isJumping);
    }
    #endregion

    #region Enable/Disable
    private void OnEnable() => SubscribeListeners();
    private void OnDisable()
    {
        //Movement Listeners
        enemyMovement.onJumpEvent -= onJump;
        enemyMovement.onMoveEvent -= onMove;
        enemyMovement.onCrouchEvent -= onCrouch;
        //Combat Listeners
        //enemyCombat.onAttackEvent -= onAttack;
        //Health Listeners
        healthSystem.onDamageTaken -= onDamageTaken;
        healthSystem.onDeath -= onDeath;
    }
    #endregion
}
