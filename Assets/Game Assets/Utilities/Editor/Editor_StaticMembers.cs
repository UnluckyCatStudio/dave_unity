using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(MonoBehaviour),true)]
public class Editor_StaticPlus : Editor
{
	bool foldout;
	public override void OnInspectorGUI ()
	{
		var type = target.GetType ();
		var fields = type.GetFields
			( BindingFlags.Static | BindingFlags.Public );  // You can use whatever flags you need

		if (fields != null && fields.Length != 0)
		{
			foldout = EditorGUILayout.Foldout ( foldout, "Static fields" );
			if (foldout)
			{
				foreach (var f in fields)
				{
					#region FIELD TYPES
					// Field type check
					// Currently only supported types:
					// - bool
					// - string
					// - color
					// - float
					// - enums ( without flags )
					// You can easily add more if you wish
					if		(f.FieldType.Equals ( typeof (bool) ))		ShowBool ( f );
					else if (f.FieldType.Equals ( typeof (string) ))	ShowString ( f );
					else if (f.FieldType.Equals ( typeof (Color) ))		ShowColor ( f ); 
					else if (f.FieldType.Equals ( typeof (float) ))		ShowFloat ( f );
					else if (f.FieldType.Equals ( typeof (Enum) ))		ShowEnum ( f ); 
					#endregion
				}
			}
		}

		// Add some space to separate
		EditorGUILayout.Space ();
		/// Normal Unity inspector
		base.OnInspectorGUI ();
	}

	#region FUNCTIONS
	void ShowBool ( FieldInfo f ) 
	{
		var value = EditorGUILayout.Toggle ( f.Name, (bool) f.GetValue ( null ) );
		if (value != (bool) f.GetValue ( null ))
			f.SetValue ( null, value );
	}
	void ShowString ( FieldInfo f ) 
	{
		var value = EditorGUILayout.TextField ( f.Name, (string) f.GetValue ( null ) );
		if (value != (string) f.GetValue ( null ))
			f.SetValue ( null, value );
	}
	void ShowColor ( FieldInfo f ) 
	{
		var value = EditorGUILayout.ColorField ( f.Name, (Color) f.GetValue ( null ) );
		if (value != (Color) f.GetValue ( null ))
			f.SetValue ( null, value );
	}
	void ShowFloat ( FieldInfo f ) 
	{
		var value = EditorGUILayout.FloatField ( f.Name, (float) f.GetValue ( null ) );
		if (value != (float) f.GetValue ( null ))
			f.SetValue ( null, value );
	}
	void ShowEnum ( FieldInfo f )
	{
		var value = EditorGUILayout.EnumPopup ( f.Name, (Enum) f.GetValue ( null ) );
		if (value != (Enum) f.GetValue ( null ))
			f.SetValue ( null, value );
	}
	#endregion
}
