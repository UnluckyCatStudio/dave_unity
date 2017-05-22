using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyru.etc
{
	public static class ExtensionMethods
	{
		#region INT
		public static void Times ( this int max, Action action ) 
		{
			for ( int i = 0;i!=max;i++ )
				action ();
		}
		#endregion

		#region STRING
		public static string Format ( this string s ) 
		{
			return s
				.Replace ( "->", "\n" )
				.Replace ( "[", "<b><color=orange>" )
				.Replace ( "]", "</color></b>" );
		}
		#endregion

		#region MONO
		public static IEnumerator AsyncLerp<T> ( this MonoBehaviour m, string value, float target, float duration, UnityEngine.Object parent = null )
		{
			// Reflection
			var param = typeof ( T ).GetProperty ( value );
			var original = ( float ) param.GetValue ( parent, null );

			var start = Time.time;
			var progress = 0f;

			while ( progress < 1f )
			{
				var newValue = Mathf.Lerp ( original, target, progress );
				param.SetValue ( parent, newValue, null );

				progress = ( Time.time - start ) / duration;
				yield return null;
			}
		}
		public static IEnumerator AsyncLerp<T> ( this MonoBehaviour m, string value, Color target, float duration, UnityEngine.Object parent = null )
		{
			// Reflection
			var param = typeof(T).GetProperty ( value );
			var original = ( Color ) param.GetValue ( parent, null );

			var start = Time.time;
			var progress = 0f;

			while ( progress < 1f )
			{
				var newValue = Color.Lerp ( original, target, progress );
				param.SetValue ( parent, newValue, null );

				progress = ( Time.time - start ) / duration;
				yield return null;
			}
		}
		public static IEnumerator AsyncLerp<T> ( this MonoBehaviour m, string value, Vector3 target, float duration, UnityEngine.Object parent = null )
		{
			// Reflection
			var param = typeof(T).GetField ( value );
			var original = (Vector3) param.GetValue ( parent );

			var start = Time.time;
			var progress = 0f;

			while (progress < 1f)
			{
				var newValue = Vector3.Lerp ( original, target, progress );
				param.SetValue ( parent, newValue );

				progress = (Time.time - start) / duration;
				yield return null;
			}
		}
		public static IEnumerator AsyncLerp<T> ( this MonoBehaviour m, string value, Quaternion target, float duration, UnityEngine.Object parent = null )
		{
			// Reflection
			var param = typeof (T).GetProperty ( value );
			var original = (Quaternion) param.GetValue ( parent, null );

			var start = Time.time;
			var progress = 0f;

			while (progress < 1f)
			{
				var newValue = Quaternion.Lerp ( original, target, progress );
				param.SetValue ( parent, newValue, null );

				progress = (Time.time - start) / duration;
				yield return null;
			}
		}
		#endregion

		#region QUATERNION
		public static Quaternion Inverse ( this Quaternion q ) 
		{
			return Quaternion.Inverse ( q );
		}
		#endregion
	}
}
