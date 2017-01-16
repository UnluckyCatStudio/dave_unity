using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The object this script is attached to
/// becomes a Spline manager.
/// It will contain several objects each of them
/// being a Bezier curve that states the rotation
/// of Dave at every moment.
/// The natural transform of each child curve
/// will set the local space of the algoirthm.
/// </summary>
///
public class RailManager : MonoBehaviour
{
	public BezierCurve[] curves = new BezierCurve[0];
	public int curve = 0; // Current curve

	public Vector3 GetPoint ( float t )
	{
		var x = 1 - t;
		var p0 = curves[curve].p[0];
		var p1 = curves[curve].p[1];
		var p2 = curves[curve].p[2];
		var p3 = curves[curve].p[3];

		return
			p0 * x * x * x +
			3 * p1 * t * x * x +
			3 * p2 * t * t * x +
			p3 * t * t * t;
	}

	public Vector3 GetDirectionPoint ( float t )
	{ 
		float x = 1f - t;
		var p0 = curves[curve].p[0];
		var p1 = curves[curve].p[1];
		var p2 = curves[curve].p[2];
		var p3 = curves[curve].p[3];

		return
			3f * x * x * ( p1 - p0 ) +
			6f * x * t * ( p2 - p1 ) +
			3f * t * t * ( p3 - p2 );
	}
}