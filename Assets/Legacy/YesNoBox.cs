using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Spawns an UI box that asks for the user
/// to confirm or deny something.
/// </summary>
public class YesNoBox : MonoBehaviour
{
	public Text title;
	public Text countdown;
	public bool confirmed;
	public bool declined;

	public YesNoBox ( string title )
	{
		this.title.text = title;
		StartCoroutine ( "Countdown" );
	}

    IEnumerator Countdown ()
    {
        var t = 15;
        while (true)
        {
            countdown.text = "Reverting changes in " + t.ToString() + " seconds.";
            if (t == 0)
            {
                declined = true;
                Destroy(gameObject);
                break;
            }

            yield return new WaitForSeconds(1);
            t--;
        }
    }
}
