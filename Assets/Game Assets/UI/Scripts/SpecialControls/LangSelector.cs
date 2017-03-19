using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kyru.UI
{
	public class LangSelector : MonoBehaviour
	{
		private void Start () 
		{
<<<<<<< HEAD
			//GetComponent<Dropdown> ().value = Localization.lang;
=======
			GetComponent<Dropdown> ().value = (int) Localization.lang;
>>>>>>> refs/remotes/origin/menus
		}

		public void ChangeLang ( int lang ) 
		{
			Localization.lang = lang;
			Localization.UpdateAllTexts ();
		}
	} 
}
