using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordShot : MonoBehaviour
{
	private void Update ()
	{
		transform.Translate ( 0, 0, -20f * Time.deltaTime, Space.Self );
	}
}
