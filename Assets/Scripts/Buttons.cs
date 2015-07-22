using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {

	GameFlow mGameFlow;
    GameMusic mGameMusic;

    private bool mSoundOn;

	void Start()
	{
		Init();
	}

	void Init()
	{
		mGameFlow = GameObject.Find ("GameFlow").GetComponent<GameFlow>();
        mGameMusic = GameObject.Find("GameMusic").GetComponent<GameMusic>();
        mSoundOn = true;
	}

	public void RestartLevel()
	{
		mGameFlow.RestartLevel ();
	}

	public void NextLevel()
	{
		mGameFlow.NextLevel ();
	}

    public void ToggleSound()
    {
        mSoundOn = !mSoundOn;

        AudioListener.volume = mSoundOn ? 1 : 0;
    }
}