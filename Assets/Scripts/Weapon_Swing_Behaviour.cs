using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Swing_Behaviour : StateMachineBehaviour
{
    int damage;

    Transform wepPos;

    [SerializeField]
    LayerMask enemyLayer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wepPos = animator.transform;
        damage = animator.GetComponent<Weapon>().Damage;

        animator.GetComponent<SpriteRenderer>().color = Color.red;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //RaycastHit2D[] hits = Physics2D.CircleCastAll(wepPos.position, 1f, Vector2.right, 0.3f, enemyLayer);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(wepPos.position, new Vector2(2, 0.3f), 0f, Vector2.right, 0.3f, enemyLayer);

        foreach (var enemy in hits)
        {
            enemy.transform.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<SpriteRenderer>().color = Color.white;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
