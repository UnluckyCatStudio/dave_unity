using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class SMB_DaveAttack : StateMachineBehaviour
{
	public int attackID;
	[Header("Replicate transition")]
	public float exitTime;
	public float duration;
	public float offset;

	[Header("Wait combo input")]
	[Range(0,1)] public float startWindow;	// normalized time

	// Combo bool is an instance,
	// so it's checked per attack
	public bool didCombo;

	public override void OnStateEnter ( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
	{
		base.OnStateEnter ( animator, stateInfo, layerIndex );

		// When entering an attack state, unlock Dave
		Game.dave.LockDave ( 1 );
	}

	public override void OnStateExit ( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
	{
		base.OnStateExit ( animator, stateInfo, layerIndex );

		if ( didCombo ) animator.SetTrigger   ( "Attack" );
		else			animator.ResetTrigger ( "Attack" );

		if ( !didCombo ) Game.dave.LockDave ( 0 );
		else			 didCombo = false;
	}

	public override void OnStateUpdate ( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
	{
		base.OnStateUpdate ( animator, stateInfo, layerIndex );

		if ( !didCombo && stateInfo.normalizedTime >= startWindow )
		{
			didCombo = Game.input.GetKey ( Key.Attack );
		}

		// If combo was called, keep Dave locked
		if ( didCombo && stateInfo.normalizedTime >= exitTime )
		{
			animator.CrossFade
				(
					"Sword_attack_" + (attackID+1).ToString (),
					duration,
					layerIndex,
					offset
				);

			Game.dave.LockDave ( 1 );
		}
	}
}
