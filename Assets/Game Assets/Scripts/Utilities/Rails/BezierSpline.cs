using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BeizerSplines are the result of several
/// BeizerCurves put together.
/// These form the rails of both the player
/// and the camera.
/// </summary>
public class BezierSpline : MonoBehaviour
{
	public BezierCurve[] curves = new BezierCurve[0];

	public float T = 0;     // Progress in curves ( absolute )

	public bool debug;

	public Vector3 GetPoint ()
	{
		var curve = GetCurrentCurve ();
		var t = T - curve;

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

	public Vector3 GetDirectionPoint ()
	{
		var curve = GetCurrentCurve ();
		var t = T - curve;

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

	// Gets curve index from T
	private int GetCurrentCurve ()
	{
		var index = 0;
		var check = T;
		while ( check > 1 )
		{
			check -= 1;
			index++;
		}

		return index;
	}
}
