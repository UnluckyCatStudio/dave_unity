using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct AudioSettings
{
	public float    master,
					music,
					sfx,
					ambient,
					voices;
}

public class AudioManager : MonoBehaviour
{
	public float	master	= 100,
					music	= 100,
					sfx		= 100,
					ambient	= 100,
					voices	= 100;

	public void LoadValues ()
	{
		Game.ui.master.value	= master;
		Game.ui.music.value		= music;
		Game.ui.sfx.value		= sfx;
		Game.ui.ambient.value	= ambient;
		Game.ui.voices.value	= voices;
	}

	public void ApplySave ( bool justApply = false )
	{
		// Apply
		master	= Game.ui.master.value;
		music	= Game.ui.music.value;
		sfx		= Game.ui.sfx.value;
		ambient	= Game.ui.ambient.value;
		voices	= Game.ui.voices.value;

		Apply ();
		if ( justApply ) return;

		// Save
		PlayerPrefs.SetString ( "Audio", JsonUtility.ToJson ( this ) );
		PlayerPrefs.Save ();
	}

	private void Apply ()
	{
		Game.ui.audioMaster.SetFloat ( "master-vol", CorrectVolume ( master ) );
		Game.ui.audioMaster.SetFloat ( "music-vol", CorrectVolume ( music ) );
		Game.ui.audioMaster.SetFloat ( "sfx-vol", CorrectVolume ( sfx ) );
		Game.ui.audioMaster.SetFloat ( "ambient-vol", CorrectVolume ( ambient ) );
		Game.ui.audioMaster.SetFloat ( "voices-vol", CorrectVolume ( voices ) );
	}

	/// <summary>
	/// Given a % from 0->100 returns
	/// a value from -80->0
	/// </summary>
	private float CorrectVolume ( float value )
	{
		return
			( value / 100 * 80 ) - 80;
	}
}
