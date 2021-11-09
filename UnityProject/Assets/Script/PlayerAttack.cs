using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : StateMachineBehaviour
{
    //Set attack animation, prepare for next move in case of combo and reset variables
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Attack", false);
        PlayerActions Player = GameObject.Find("kachujin").GetComponent<PlayerActions>();
        Player.IsAttacking = true;
        Player.CanHit = true;
        Player.Combo = Player.NextCombo;
        animator.SetFloat("Combo", Player.Combo);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerActions Player = GameObject.Find("kachujin").GetComponent<PlayerActions>();
        Player.IsAttacking = false;
        Player.CanHit = false;
    }
}
