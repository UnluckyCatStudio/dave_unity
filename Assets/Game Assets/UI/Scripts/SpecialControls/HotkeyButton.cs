using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kyru.UI
{
	public class HotkeyButton : MonoBehaviour
	{
		private Button button;

		public Key  key;
		public Text info;
		public Text esc;

		void Awake ()
		{
			button = GetComponent<Button> ();
		}
	}
}
