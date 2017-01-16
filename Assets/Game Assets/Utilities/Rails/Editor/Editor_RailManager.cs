using UnityEngine;
using UnityEditor;

[CustomEditor ( typeof ( RailManager ) )]
public class Editor_RailManager : Editor
{
	public override void OnInspectorGUI ()
	{
		var rail = target as RailManager;
		Undo.RecordObject ( rail, "Changed Rail Manager" );

		if ( rail.curves.Length == 0 )	InitializeArray ( rail );
		else
		{
			ShowGUI ( rail );
			if ( rail.curves.Length == 0 ) return;   // If array deleted

			DrawLines  ( rail );
			DrawGizmos ( rail );
			DrawHandle ( rail );
		}
	}

	#region FX
	private BezierCurve NewCurve ( RailManager rail )
	{
		// Create curve
		var curve = Instantiate
		(
			new BezierCurve (),
			rail.curves[rail.curves.Length-1].GetWorldPoint ( 3 ),
			rail.transform.rotation
		);

		// Initialize points


		return curve;
	}
	#endregion

	#region HELPERS
	private void DrawLines  ( RailManager rail )
	{
		Handles.color = Color.cyan;
		for ( int c = 0; c != rail.curves.Length; c++ )
		{
			if ( c == selectedCurve ) Handles.color = Color.red;

			var curve = rail.curves[c];
			Handles.DrawBezier
			(
				curve.p[0],
				curve.p[3],
				curve.p[1],
				curve.p[2],
				Color.white, null, 1.5f
			);
		}
	}
	private void DrawGizmos ( RailManager rail )
	{
		for ( int c=0; c!=rail.curves.Length; c++ )
		{
			for ( int p=1; p!=4; p++ )
			{
				var size = HandleUtility.GetHandleSize ( rail.curves[c].p[p] );
				var selected =
					Handles.Button
					(
						rail.curves[c].p[p],
						rail.transform.rotation,
						size * 1.6f, size * 2f,
						p == 3 ? ( Handles.DrawCapFunction )
							Handles.CubeCap
							:
							Handles.SphereCap
					);

				// If clicked on some gizmo
				if ( selected )
				{
					selectedCurve = c;
					selectedPoint = p;
				}
			}
		}
	}

	private int selectedPoint = -1;
	private int selectedCurve = -1;

	private void DrawHandle ( RailManager rail )
	{
		if ( selectedCurve != -1 && selectedPoint != -1 )
		{
			var pos = Handles.PositionHandle ( rail.curves[selectedCurve].p[selectedPoint], rail.transform.rotation );
			rail.curves[selectedCurve].p[selectedPoint] = pos;
		}
	}
	#endregion

	#region GUI HELPERS
	private void InitializeArray ( RailManager rail )
	{
		if ( GUILayout.Button ( "Initialize" ) )
		{
			// Initialize array
			rail.curves = new BezierCurve[1];

			// Create first default curve
			GameObject go;
			go = new GameObject ( "Curve_0" );
			go.AddComponent<BezierCurve> ();
			go.transform.position = rail.transform.position;
			go.transform.rotation = rail.transform.rotation;
			go.transform.parent	  = rail.transform;

			var curve   = go.GetComponent<BezierCurve> ();
			curve.p		= new Vector3[4];
			curve.p[0]	= Vector3.zero;
			curve.p[1]	= Vector3.forward + Vector3.left;
			curve.p[2]	= 3 * Vector3.forward + Vector3.left;
			curve.p[3]	= 4 * Vector3.forward;

			rail.curves[0] = curve;
		}
	}

	private void ShowGUI ( RailManager rail )
	{
		if ( GUILayout.Button ( "Remove all curves" ) )
		{
			// Delete existing curves
			for ( int c=0; c!=rail.curves.Length; c++ )
				DestroyImmediate ( rail.curves[c].gameObject );

			// Create empty array
			rail.curves = new BezierCurve[0];
		}
	}
	#endregion

}