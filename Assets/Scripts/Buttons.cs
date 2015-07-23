using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {

	GameFlow mGameFlow;
    GameMusic mGameMusic;
	public AudioClip mButtonClick;

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
		mGameMusic.PlaySound(mButtonClick);
		mGameFlow.LoadLevel (GameFlow.Levels.RESTART);
	}

	public void NextLevel()
	{
		mGameMusic.PlaySound(mButtonClick);
		mGameFlow.LoadLevel (GameFlow.Levels.NEXT);
	}

    public void ToggleSound()
	{
		mGameMusic.PlaySound(mButtonClick);
        mSoundOn = !mSoundOn;

        AudioListener.volume = mSoundOn ? 1 : 0;
    }
}