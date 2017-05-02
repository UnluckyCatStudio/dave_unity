using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaveController : Kyru.etc.AnimatorController
{
	private CharacterController me;

	[Header("References")]
	public SkinnedMeshRenderer skin;
	public CamController cam;   // The camera pivot ( for relative movement )
	public Cloth scarf;
	public Vector3 scarfWind;

	// Movement
	[Header("Movement")]
	public bool	 canMove;		// Can the player move?
	public float speed;			// Movement speed multiplier

	[Header("Combat")]
	public  bool canCombat;
	public	bool canShoot;
	public  SwordController sword;

	//	[Header("IK")]
	//	public Transform    leftFoot;
	//	public Transform    rightFoot;

	// Animation params
	public bool  Moving			
	{
		get { return anim.GetBool ( "Moving" ); }
		set { anim.SetBool ( "Moving", value ); }
	}
	public bool  Sheathing		
	{
		get { return anim.GetBool ( "Sheathing" ); }
		set { anim.SetBool ( "Sheathing", value ); }
	}
	public bool  SwordOut		
	{
		get { return anim.GetBool ( "SwordOut" ); }
	}
	public bool  Attacking		
	{
		get { return anim.GetBool ( "Attacking" ); }
		set { anim.SetBool ( "Attacking", value ); }
	}
	public bool  Charging		
	{
		get { return anim.GetBool ( "Charging" ); }
		set { anim.SetBool ( "Charging", value ); }
	}
	public bool  Hitd			
	{
		get { return anim.GetBool ( "Hitd" ); }
		set { anim.SetBool ( "Hitd", value ); }
	}
	public bool  DealingDmg		
	{
		get { return anim.GetBool ( "DealingDmg" ); }
		set { anim.SetBool ( "DealingDmg", value ); }
	}
	public float RotationMul	
	{
		get { return anim.GetFloat ( "RotationMul" ); }
		set { anim.SetFloat ( "RotationMul", value ); }
	}

	void Update () 
	{
		#region ETC
		// Apply local-space wind to the scarf
		scarf.externalAcceleration = transform.TransformDirection ( scarfWind );

		// Only enable sword collider when attacking
		sword.edge.enabled = DealingDmg;

		// Make camera follow dave
		cam.FollowDave ();
		#endregion

		if ( Hitd ) return;

		#region MOVEMENT
		if ( canMove )
		{
			var movement = Vector3.zero;
			// Get movement relative to camera rotation
			if ( Game.input.GetKey ( Key.Forward ) )	movement += cam.transform.forward;
			if ( Game.input.GetKey ( Key.Backwards ) )  movement -= cam.transform.forward;
			if ( Game.input.GetKey ( Key.Left ) )		movement -= cam.transform.right;
			if ( Game.input.GetKey ( Key.Right ) )		movement += cam.transform.right;
			// Keep only direction of movement
			movement.Normalize ();

			if ( movement != Vector3.zero )
			{
				#region CORRECT ROTATION
				if ( !Charging )
				{
					var rotDir = Quaternion.LookRotation ( movement );
					var diff = Quaternion.Angle ( transform.rotation, rotDir );

					transform.rotation =
					Quaternion.Slerp
					(
						transform.rotation,
						rotDir,
						10 * Time.deltaTime   // bigger diff = faster rotation
						* RotationMul
					); 
				}
				#endregion

				me.Move ( movement * speed * Time.deltaTime );
				Moving = true;
			}
			else Moving = false;
		}
		#endregion

		#region COMBAT
		if ( canCombat )
		{
			#region SHEATHING
			if ( !Sheathing
				&& Game.input.GetKeyDown ( Key.Sword ) )
			{
				if ( !SwordOut ) anim.SetTrigger ( "Unsheathe" );
				else			 anim.SetTrigger ( "Sheathe" );

				Sheathing = true;
			}
			#endregion

			#region ATTACKING
			if
			(  SwordOut
			&& !Attacking
			&& !Charging
			&& !Sheathing )
			{
				if ( Game.input.GetKeyDown ( Key.Attack_single ) )
				{
					anim.SetTrigger ( "Attack-single" );
					Attacking = true;
				}
				else
				if ( Game.input.GetKeyDown ( Key.Attack_big ) )
				{
					anim.SetTrigger ( "Attack-big" );
					Attacking = true;
				}
			}


			#endregion

			#region CHARGE
			if
			(  !Sheathing
			&& !Attacking
			&& canShoot
			&& Game.input.GetKeyDown ( Key.Charge ) )
			{
				anim.SetTrigger ( "Charge" );
				Charging = true;
			}
			else
			if ( Game.input.GetKeyUp ( Key.Charge ) )
			{
				Charging = false;
			} 
			#endregion

			#region SHOT
			if
			(  Charging
			&& Game.input.GetKeyDown ( Key.Attack_single ) )
			{
				anim.SetTrigger ( "Shoot" );
				Charging = false;
			} 
			#endregion
		}
		#endregion
	}

	#region IK
	//	private void OnAnimatorIK ()
	//	{
	//		if ( activeIK )
	//		{
	//
	//		}
	//	} 
	#endregion

	#region FX
	protected override void Awake () 
	{
		base.Awake ();
		sword.transform.SetParent ( swordBackHolder );
		sword.transform.localPosition = Vector3.zero;
		sword.transform.localRotation = Quaternion.identity;
		Game.dave = this;
		me = GetComponent<CharacterController> ();
	}
	#endregion

	#region EVENTS
	[Header ("Animation References")]
	[SerializeField] Transform swordBackHolder;
	[SerializeField] Transform swordHandHolder;

	/// <summary>
	/// Attaches the sword to either
	/// the belt or the hand.
	///  0 = unsheathe
	/// !0 = sheathe
	/// </summary>
	private void Sheathe ( int unseathe ) 
	{
		if (locked) return;

		if ( unseathe == 0 )
		{
			sword.transform.SetParent ( swordHandHolder );
			sword.anim.SetTrigger ( "Fade-in" );
		}
		else
		{
			sword.transform.SetParent ( swordBackHolder );
			sword.anim.SetTrigger ( "Fade-out" );
		}

		sword.transform.localPosition = Vector3.zero;
		sword.transform.localRotation = Quaternion.identity;
	}

	private void Hit ( Vector3 point ) 
	{
		if ( Hitd ) return;
		// Front or Back ?
		var angle = Quaternion.Angle ( transform.localRotation, Quaternion.LookRotation ( point ) );

		if ( angle >= 50 )  anim.SetTrigger ( "Hit_Front" );
		else				anim.SetTrigger ( "Hit_Back" );

		Hitd = true;
		Moving = false;
		Attacking = false;
		anim.ResetTrigger ( "Attack-big" );
		anim.ResetTrigger ( "Attack-single" );
		anim.ResetTrigger ( "Charge" );
		anim.ResetTrigger ( "Shoot" );
		DealingDmg = false;
		Charging = false;
		Sheathing = false;
		anim.ResetTrigger ( "Sheathe" );
		anim.ResetTrigger ( "Unsheathe" );

		locked=true;
	}
	#endregion
}
