using UnityEngine;
using UnityEditor;

[CustomEditor ( typeof ( BezierSpline ) )]
public class Editor_BezierSpline : Editor
{
	public override void OnInspectorGUI ()
	{
		// Spline editing
		var spline = target as BezierSpline;

		if ( sCurve != -1 )
		{
			//	spline.curves[selectedCurve].p[selectedPoint] = EditorGUILayout.Vector3Field ( "Point", spline.curves[selectedCurve].p[selectedCurve] );
			spline.curves[sCurve].mirror = EditorGUILayout.ToggleLeft ( "Mirror", spline.curves[sCurve].mirror );
		}

		// Buttons
		//if ( GUILayout.Button ( "Align all point to " ))

		if ( IsPoint ( sPoint ) == false )
			if ( GUILayout.Button ( "Tangent to point" ) )
				TangentToPoint ( sPoint );

		if ( GUILayout.Button ( "New curve" ) )
			NewCurve ( false );

		if ( sCurve != -1 )
			if ( GUILayout.Button ( "Delete last curve" ) )
				DeleteLastCurve ();

		if ( GUILayout.Button ( "Reset spline" ) )
			NewCurve ( true );

		// Debug
		spline.debug = EditorGUILayout.ToggleLeft ( "Debug", spline.debug );
	}

	#region FX

	private void NewCurve ( bool reset )
	{
		var spline = target as BezierSpline;

		// Check if spline is empty or has been reset
		// In that case spawn 1 random curve
		if ( reset )
		{
			var tr = spline.transform;
			var pRef = Vector3.zero;

			spline.curves = new BezierCurve[]
			{
				new BezierCurve
				{
					parent = tr,
					mirror = true,
					p = new Vector3[]
					{
						pRef,
						pRef - tr.right * .3f + tr.up * .3f,
						pRef - tr.right * .7f + tr.up * .3f,
						pRef - tr.right
					}
				}
			};

			sCurve = -1;
			SceneView.RepaintAll ();
			return;
		}

		// Create new array
		var newArray = new BezierCurve[spline.curves.Length + 1];
		for ( int i = 0; i != newArray.Length - 1; i++ )
			newArray[i] = spline.curves[i];

		// Calculate new curve based on last one
		var last = spline.curves[spline.curves.Length - 1];
		var direction = last._p[3] - last._p[0];
		var newCurve = new BezierCurve
		{
			parent = spline.transform,
			mirror = true,
			p = new Vector3[]
			{
				last._p[3],
				last._p[1] + direction,
				last._p[2] + direction,
				last._p[3] + direction
			}
		};

		// Set the array
		newArray[newArray.Length - 1] = newCurve;
		spline.curves = newArray;

		// By default, mirror new curve
		var a = spline.curves[spline.curves.Length - 2].p[2];
		var b = spline.curves[spline.curves.Length - 2].p[3];
		spline.curves[spline.curves.Length - 1].p[1] = 2*b - a;

		SceneView.RepaintAll ();
	}

	private void TangentToPoint ( int index )
	{
		var spline = target as BezierSpline;
		spline.curves[index]._p[sPoint] = spline.curves[index].p[GetTangentPoint ( sPoint )];

		SceneView.RepaintAll ();
	}

	private void DeleteLastCurve ()
	{
		var spline = target as BezierSpline;

		if ( spline.curves.Length <= 1 )
		{
			NewCurve ( true );
			return;
		}

		// Create new array but remove last curve
		var newArray = new BezierCurve[spline.curves.Length - 1];
		for ( int i = 0; i != newArray.Length; i++ )
			newArray[i] = spline.curves[i];

		sCurve--;
		spline.curves = newArray;
		SceneView.RepaintAll ();
	}

	#endregion

	private void OnSceneGUI ()
	{
		var spline = target as BezierSpline;

		// Check if spline is empty or not enough
		if ( spline.curves.Length < 1 )
			NewCurve ( true );

		// For every point in the spline:
		for ( int i = 0; i != spline.curves.Length; i++ )
		{
			spline.curves[i].parent = spline.transform;

			DrawBeizers ( i, spline );
			DrawLines ( i, spline );
			Handles.color = Color.black;
			DrawButtons ( i, spline );
			Handles.color = Color.white;
			DrawGizmos ( i, spline );
		}
	}

	#region HELPERS

	private int sCurve = -1;
	private int sPoint = -1;

	private bool? IsPoint ( int p )
	{
		if ( sCurve == -1 || sPoint == -1 )
			return null;

		if ( p == 0 || p == 3 )
			return true;
		else
			return false;
	}

	private int GetTangent ( int p )
	{
		if ( sCurve == -1 || sPoint == -1 )
			return -1;

		switch ( p )
		{
		case 0:
			return 1;
		case 3:
			return 2;

		default:
			throw new UnityException ( "point does not exist" );
		}
	}

	private int GetTangentPoint ( int p )
	{
		if ( sCurve == -1 || sPoint == -1 )
			return -1;

		switch ( p )
		{
		case 1:
			return 0;
		case 2:
			return 3;

		default:
			throw new UnityException ( "point does not exist" );
		}
	}

	private int GetInverseTangent ( int p )
	{
		if ( sCurve == -1 || sPoint == -1 )
			return -1;

		switch ( p )
		{
		case 1:
			return 2;
		case 2:
			return 1;

		default:
			throw new UnityException ( "point does not exist" );
		}
	}

	private void DrawButtons ( int index, BezierSpline spline )
	{
		// Handle size control
		float size;

		// Point - Start
		size = HandleUtility.GetHandleSize ( spline.curves[index].p[0] );
		if ( Handles.Button ( spline.curves[index].p[0], Quaternion.identity, size * .10f, size * .12f, Handles.DotCap ) )
		{
			sCurve = index;
			sPoint = 0;
			Repaint ();

			if ( spline.debug ) Debug.Log ( "Selected p0 - " + spline.curves[index].p[sPoint] );
		}
		// Point - Start tangent
		size = HandleUtility.GetHandleSize ( spline.curves[index].p[1] );
		if ( Handles.Button ( spline.curves[index].p[1], Quaternion.identity, size * .10f, size * .12f, Handles.SphereCap ) )
		{
			sCurve = index;
			sPoint = 1;
			Repaint ();

			if ( spline.debug ) Debug.Log ( "Selected p0 - " + spline.curves[index].p[sPoint] );
		}
		// Point - End
		size = HandleUtility.GetHandleSize ( spline.curves[index].p[2] );
		if ( Handles.Button ( spline.curves[index].p[2], Quaternion.identity, size * .10f, size * .12f, Handles.SphereCap ) )
		{
			sCurve = index;
			sPoint = 2;
			Repaint ();

			if ( spline.debug ) Debug.Log ( "Selected p0 - " + spline.curves[index].p[sPoint] );
		}
		// Point - End tangent
		size = HandleUtility.GetHandleSize ( spline.curves[index].p[3] );
		if ( Handles.Button ( spline.curves[index].p[3], Quaternion.identity, size * .10f, size * .12f, Handles.DotCap ) )
		{
			sCurve = index;
			sPoint = 3;
			Repaint ();

			if ( spline.debug ) Debug.Log ( "Selected p0 - " + spline.curves[index].p[sPoint] );
		}
	}

	private void DrawGizmos ( int index, BezierSpline spline )
	{
		if ( sCurve == index )
		{
			// Curve point
			if ( IsPoint ( sPoint ) == true )
			{
				EditorGUI.BeginChangeCheck ();
				var point = Handles.PositionHandle ( spline.curves[index].p[sPoint], spline.transform.rotation );
				point = spline.transform.InverseTransformPoint ( point );
				if ( EditorGUI.EndChangeCheck () )
				{
					Undo.RecordObject ( spline, "Moved point." );
					// If ALT : move tangent along with point
					if ( Event.current.alt )
					{
						var tanID = GetTangent ( sPoint );
						var distance = spline.curves[index]._p[sPoint] - spline.curves[index]._p[tanID];
						spline.curves[index]._p[tanID] = point - distance;

						// If mirroring, move connected curves' tangents
						if ( spline.curves[sCurve].mirror )
						{
							if ( sCurve != 0 && !( tanID == 2 && sCurve == spline.curves.Length - 1 ) )
							{
								var a = spline.curves[sCurve].p[tanID];
								var b = spline.curves[sCurve].p[sPoint];
								spline.curves[sCurve + ( tanID == 1 ? -1 : +1 )]._p[GetInverseTangent ( tanID )] = spline.transform.InverseTransformPoint ( 2 * b - a );
							}
						}
					}
					spline.curves[index]._p[sPoint] = point;

					// Move curve connected to point
					if ( sPoint == 0 && sCurve != 0 )
					{
						spline.curves[sCurve - 1]._p[3] = point;
					}
					else if ( sPoint == 3 && sCurve != spline.curves.Length - 1 )
					{
						spline.curves[sCurve + 1]._p[0] = point;
					}

					Repaint ();
				}
			}
			// Curve tangent
			else
			{
				EditorGUI.BeginChangeCheck ();
				var tan = Handles.PositionHandle ( spline.curves[index].p[sPoint], spline.transform.rotation );
				tan = spline.transform.InverseTransformPoint ( tan );
				if ( EditorGUI.EndChangeCheck () )
				{
					Undo.RecordObject ( spline, "Moved point tangent." );
					spline.curves[index]._p[sPoint] = tan;

					// If mirroring, move connected curves' tangents
					if ( spline.curves[sCurve].mirror )
					{
						if ( sCurve != 0 && !( sPoint == 2 && sCurve == spline.curves.Length - 1 ) )
						{
							var a = spline.curves[sCurve].p[sPoint];
							var b = spline.curves[sCurve].p[GetTangentPoint ( sPoint )];
							spline.curves[sCurve + ( sPoint == 1 ? -1 : +1 )]._p[GetInverseTangent(sPoint)] = spline.transform.InverseTransformPoint ( 2 * b - a );
							;
						}
					}

					Repaint ();
				}
			}
		}
	}

	private void DrawLines ( int index, BezierSpline spline )
	{
		//if ( index == selectedPoint )	Handles.color = Color.red;
		//else							Handles.color = Color.green;

		Handles.DrawLine ( spline.curves[index].p[0], spline.curves[index].p[1] );
		Handles.DrawLine ( spline.curves[index].p[3], spline.curves[index].p[2] );
		Handles.color = Color.white;
	}

	private void DrawBeizers ( int index, BezierSpline spline )
	{
		Handles.DrawBezier ( spline.curves[index].p[0], spline.curves[index].p[3],
								spline.curves[index].p[1], spline.curves[index].p[2],
								index == sCurve ? Color.blue : Color.cyan,
								null, 3f );
	}

	#endregion
}