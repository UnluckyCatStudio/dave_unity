using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

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
	#region UI
	public Slider	master,
					music,
					sfx	,
					ambient,
					voices;
	#endregion

	public new AudioMixer audio;

	public void LoadValues ()
	{
		master.value	= Game.audio.master;
		music.value		= Game.audio.music;
		sfx.value		= Game.audio.sfx;
		ambient.value	= Game.audio.ambient;
		voices.value	= Game.audio.voices;
	}

	public void ApplySave ( bool justApply = false )
	{
		// Apply
		Game.audio.master	= master.value;
		Game.audio.music	= music.value;
		Game.audio.sfx		= sfx.value;
		Game.audio.ambient	= ambient.value;
		Game.audio.voices	= voices.value;

		Apply ();
		if ( justApply ) return;

		// Save
		PlayerPrefs.SetString ( "Audio", JsonUtility.ToJson ( Game.audio ) );
		PlayerPrefs.Save ();
	}

	private void Apply ()
	{
		audio.SetFloat ( "master-vol", CorrectVolume ( master.value ) );
		audio.SetFloat ( "music-vol", CorrectVolume ( music.value ) );
		audio.SetFloat ( "sfx-vol", CorrectVolume ( sfx.value ) );
		audio.SetFloat ( "ambient-vol", CorrectVolume ( ambient.value ) );
		audio.SetFloat ( "voices-vol", CorrectVolume ( voices.value ) );
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
