using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class SMB_DaveAttack : StateMachineBehaviour
{
	public override void OnStateExit ( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
	{
		base.OnStateExit ( animator, stateInfo, layerIndex );
		animator.ResetTrigger ( "Attack" );
	}
}
