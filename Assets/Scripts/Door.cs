using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Door : MonoBehaviour 
{

	//private List<int> mScreenList = new List<int>();

	// Audio Clips
	public AudioClip mLosing;
	public AudioClip mVictory;

	// Audio Source variables
	private AudioSource mSource;
	private float mVol = 1.0f;

	private GameMusic mGameMusic;
	private GameFlow mGameFlow;

	// Use this for initialization
	void Start () 
	{
		Init ();
	}
	
	void Init()
	{
		mSource = GetComponent<AudioSource>();
		mGameMusic = GameObject.Find("GameMusic").GetComponent<GameMusic>();
		mGameFlow = GameObject.Find ("GameFlow").GetComponent<GameFlow>();
	}

	// Lauches the Victory or Losing Screen depening on the number of collectables
	public void LaunchScreen(int p_nbCollectables)
	{
		if (p_nbCollectables <= 1)
		{
			// Launch Losing Screen
			StopGameMusic ();
			mSource.PlayOneShot(mLosing, mVol);
			mGameMusic.PlayMusic(GameMusic.Songs.MENU);
			mGameFlow.ChangeLevel(GameFlow.States.DEFEAT);
		}
		else if (p_nbCollectables >= 2)
		{
			// Launch Victory Screen
			StopGameMusic ();
			mSource.PlayOneShot(mVictory, mVol);
			mGameMusic.PlayMusic(GameMusic.Songs.MENU);
			mGameFlow.ChangeLevel(GameFlow.States.VICTORY);
		}
	}

	private void StopGameMusic()
	{
		mGameMusic.StopMusic();
	}
}
