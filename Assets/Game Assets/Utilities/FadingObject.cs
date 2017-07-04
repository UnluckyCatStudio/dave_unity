using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingObject : MonoBehaviour
{
	public void FadeOut( float duration )
	{
		// Coroutines are called using this method:
		StartCoroutine (Fade (duration, 0));
		// There a bunch of overloads
	}
	public void FadeIn( float duration )
	{
		StartCoroutine (Fade (duration, 1));
	}

	// -------------------------------------------------------------------

	// Coroutines must be IEnumerators
	// You could implement this using 2 different method, for fading in and out
	// if you want or need to.
	private IEnumerator Fade( float duration, float targetTransparency )
	{
		var original = r.material.color;
		// If you use 'material' instead of 'sharedMaterial'
		// Unity will create an instance of the material.
		// Having too much instances can slow down the game!
		// But also notice that modifying 'sharedMaterial' won't
		// reset the changes when pausing the editor — 'material' will. 
		var newColor = original;
		newColor.a = targetTransparency;

		var step = 1f / duration;
		var progress = 0f;
		while (r.sharedMaterial.color.a != targetTransparency)
		{
			// Interpolate between two colors
			r.sharedMaterial.color = Color.Lerp (original, newColor, progress);
			// Progress towards target color
			progress += step * Time.deltaTime;

			// Coroutines must have a 'yield' statement,
			// which pauses the its execution.
			// You can check the docs about what you can yield.
			yield return new WaitForEndOfFrame ();
		}

		// REMEMBER that fading objects need
		// a shader that support transparency!
		// Standard Shader does if set to either 'Transparent' or 'Fade'
	}

	// Get the renderer
	Renderer r;
	void Awake() { r = GetComponent<Renderer> (); }
}
