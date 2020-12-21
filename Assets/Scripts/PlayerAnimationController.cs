using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private MovementController playerMovement;
    private PlayerCombat playerCombat;
    private HealthSystem healthSystem;

    private void Awake()
    {
        //Get Components
        animator = GetComponent<Animator>();
        playerMovement = GetComponentInParent<MovementController>();
        playerCombat = GetComponentInParent<PlayerCombat>();
        healthSystem = GetComponentInParent<HealthSystem>();

        SubscribeListeners();
    }

    private void SubscribeListeners()
    {
        //Movement Listeners
        playerMovement.onJumpEvent += onJump;
        playerMovement.onMoveEvent += onMove;
        playerMovement.onCrouchEvent += onCrouch;
        //Combat Listeners
        playerCombat.onAttackEvent += onAttack;
        playerCombat.onSkill += onSkill;
        //Health Listeners
        healthSystem.onDamageTaken += onDamageTaken;
        healthSystem.onDeath += onDeath;
    }

    private void onSkill(int obj)
    {
        Debug.Log("onSkill_" + obj);
        //animator.SetTrigger("onSkill_" + obj);
        animator.Play("Skill_" + obj);
    }

    public void onAnimationEnded(int id)
    {
        playerCombat.afterSkillAnimation(id);
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
        playerMovement.onJumpEvent -= onJump;
        playerMovement.onMoveEvent -= onMove;
        playerMovement.onCrouchEvent -= onCrouch;
        //Combat Listeners
        playerCombat.onAttackEvent -= onAttack;
        //Health Listeners
        healthSystem.onDamageTaken -= onDamageTaken;
        healthSystem.onDeath -= onDeath;
    }
    #endregion
}
