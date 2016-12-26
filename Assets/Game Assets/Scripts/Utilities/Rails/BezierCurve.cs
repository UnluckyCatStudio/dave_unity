using UnityEngine;

[System.Serializable]
public struct BezierCurve
{
	public Transform parent;

	// p[0] = Start point
	// p[1] = Start tangent
	// p[2] = End tangent
	// p[3] = End point
	public Vector3[] _p;    // local postition
	public Vector3[] p		// correct position ( => gloabal )
	{
		get
		{
			var temp = new Vector3[4];
			for ( int i = 0; i != 4; i++ ) temp[i] = parent.TransformPoint ( _p[i] );
			return temp;
		}
		set { _p = value; }
	}

	// If true,
	// connected curves mirror
	// this one's tangents.
	// ( curve smoothness )
	public bool mirror;
}
