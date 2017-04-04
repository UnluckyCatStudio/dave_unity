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
	public  SwordController sword;

//	[Header("IK")]
//	public Transform    leftFoot;
//	public Transform    rightFoot;
	
	// Animation params
	private bool canRotate;			/// Is Dave able to rotate?
	private bool sheathing;			/// Is Dave currently un/sheathing with the sword?
	private bool swordOut;			/// Is sword unsheathed?
	private bool attacking;			/// Is Dave performing an attack?
	private bool holdingBoomerang;	/// Is Dave aming the boomerang?

	void Update () 
	{
		#region SCARF
		// Apply local-space wind to the scarf
		scarf.externalAcceleration =
			transform.TransformDirection ( scarfWind );
		#endregion

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
				if ( canRotate )
				{
					var rotDir = Quaternion.LookRotation ( movement );
					var diff = Quaternion.Angle ( transform.rotation, rotDir );

					transform.rotation =
					Quaternion.Slerp
					(
						transform.rotation,
						rotDir,
						10 * Time.deltaTime   // bigger diff = faster rotation
					); 
				}
				#endregion

				me.Move ( movement * speed * Time.deltaTime );
				anim.SetBool ( "Moving", true );
			}
			else anim.SetBool ( "Moving", false );
		}
		#endregion

		#region COMBAT
		if ( canCombat )
		{
			#region SHEATHING
			if ( !sheathing
				&& Game.input.GetKeyDown ( Key.Sword ) )
			{
				if ( !swordOut ) anim.SetTrigger ( "Unsheathe" );
				else			 anim.SetTrigger ( "Sheathe" );

				anim.SetBool ( "Sheathing", true );
			}
			#endregion

			#region ATTACKING
			if ( swordOut
				&& !sheathing )
			{
				if ( Game.input.GetKeyDown ( Key.Attack_single ) )
					anim.SetTrigger ( "Attack" );


				anim.SetBool ( "Attacking", true );
			}

			/// Boomerang shot
			if ( !sheathing
				&& Game.input.GetKeyDown ( Key.Boomerang ) )
			{
				anim.SetTrigger ( "HoldBoomerang" );
				anim.SetBool ( "HoldingBoomerang", true );
//				cam.FocusBoomerang ();
			}
			#endregion
		}
		#endregion

		#region ANIMATOR CHECK
		// Some script variables are checked against
		// animator parameters to ensure there is no
		// conflict between values.
		canRotate = anim.GetBool ( "CanRotate" );
		sheathing = anim.GetBool ( "Sheathing" );
		swordOut  = anim.GetBool ( "SwordOut" );
		attacking = anim.GetBool ( "Attacking" );
		holdingBoomerang = anim.GetBool ( "HoldingBoomerang" );
		//anim.SetBool ( "Grounded", IsGrounded () );
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
	[SerializeField] float fallingThreshold;
	float lastTimeOnGround;
	/// <summary>
	/// Checks if Dave has been in Air-time
	/// for too long, in that case Falling animation
	/// will play.
	/// </summary>
	bool IsGrounded () 
	{
		if ( me.isGrounded )
		{
			lastTimeOnGround = Time.time;
			return true;
		}
		else
		{
			if ( Time.time >= lastTimeOnGround + fallingThreshold )
				return false;
			else
				return true;
		}
	}

	protected override void Awake () 
	{
		base.Awake ();
		Game.dave = this;
		me = GetComponent<CharacterController> ();
	}
	#endregion

	#region EVENTS
	[Header ("Animation references")]
	[SerializeField] Transform swordBeltHolder;
	[SerializeField] Transform swordHandHolder;

	/// <summary>
	/// Attaches the sword to either
	/// the belt or the hand.
	///  0 = unsheathe
	/// !0 = sheathe
	/// </summary>
	private void Sheathe ( int unseathe ) 
	{
		if ( unseathe == 0 )
		{
			sword.transform.SetParent ( swordHandHolder );
			sword.anim.SetTrigger ( "Fade-in" );
			//...
			sword.Fade ( true );
		}
		else
		{
			sword.transform.SetParent ( swordBeltHolder );
			sword.anim.SetTrigger ( "Fade-out" );
			//...
			sword.Fade ( false );
		}

		sword.transform.localPosition = Vector3.zero;
		sword.transform.localRotation = Quaternion.identity;
	}
	#endregion
}
