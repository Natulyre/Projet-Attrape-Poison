using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {

	GameFlow mGameFlow;
    GameMusic mGameMusic;
	public AudioClip mButtonClick;
	public GameObject mPauseMenu;

    private bool mSoundOn;

	void Start()
	{
		Init();
	}

	void Update()
	{
		KeyBoardInput();
	}

	void Init()
	{
		mGameFlow = GameObject.Find ("GameFlow").GetComponent<GameFlow>();
        mGameMusic = GameObject.Find("GameMusic").GetComponent<GameMusic>();
        mSoundOn = true;
	}

	public void RestartLevel()
	{
		mGameFlow.UnPause ();
		mGameMusic.PlaySound(mButtonClick);
		mGameFlow.LoadLevel (GameFlow.Levels.RESTART);
	}

	public void RestartLevelQuickly()
	{
		mGameMusic.PlaySound(mButtonClick);
		mGameFlow.RestartGame();
	}

	public void NextLevel()
	{
		mGameMusic.PlaySound(mButtonClick);
		mGameFlow.LoadLevel (GameFlow.Levels.NEXT);
	}

	public void Home()
	{
		mGameMusic.PlaySound(mButtonClick);
		mGameFlow.RestartGame ();
		GameObject mDog = GameObject.Find ("GameFlow");
		GameObject mCat = GameObject.Find ("GameMusic");
		Destroy(mDog);
		Destroy(mCat);
	}

    public void ToggleSound()
	{
		mGameMusic.PlaySound(mButtonClick);
        mSoundOn = !mSoundOn;

        AudioListener.volume = mSoundOn ? 1 : 0;
    }

	// Handles the Input by Keyboard
	public void KeyBoardInput()
	{
		// Press Enter for Next Level
		if (Input.GetKey(KeyCode.Return))
		{
			NextLevel ();
		}

		// Press Backspace
		if (Input.GetKey(KeyCode.Backspace))
		{
			if (mGameFlow.GetState() != GameFlow.States.MENU)
			{
				RestartLevel ();
			}
		}
	}

	public void Pause()
	{
		mGameMusic.PlaySound(mButtonClick);
		mGameFlow.Pause ();
	}

	public void ShowMenu(bool toggle)
	{
		mPauseMenu.SetActive (toggle);
	}
}