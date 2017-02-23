using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
	public GameObject edge;

	private IEnumerator Fade ( bool fadeIn )
	{
		yield return new WaitForSeconds ( .2f );
		edge.SetActive ( true );
	}
}
