using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Child of Rail-Manager.
/// Points:
/// p[0] = *Own transform*
/// p[1] = Start tangent
/// p[2] = End tangent
/// p[3] = End point
/// </summary>
public class BezierCurve : MonoBehaviour
{
	/// <summary>
	/// Points in local space.
	/// </summary>
	public Vector3[] p;

	/// <summary>
	/// Saves a world space point into 'p'
	/// converted to local space.
	/// </summary>
	public void SaveWorldPoint ( int p, Vector3 point )
	{
		this.p[p] = transform.InverseTransformPoint ( point );
	}

	/// <summary>
	/// Returns a point from 'p'
	/// in world space.
	/// </summary>
	public Vector3 GetWorldPoint ( int p )
	{
		return transform.TransformPoint ( this.p[p] );
	}
}
