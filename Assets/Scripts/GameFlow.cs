using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameFlow : MonoBehaviour {
	private States mCurrentState;
	private List<States> mLevelList;
	private GameMusic mGameMusic;
	
	private const string Menu = "SCREEN_START"; 
	private const string Level_1 = "LEVEL_ONE";
	private const string Level_2 = "LEVEL_TWO";
	private const string Level_3 = "LEVEL_TWO";
	private const string Loading = "SCREEN_LOADING";
	private const string Victory = "SCREEN_VICTORY";
	private const string Defeat = "SCREEN_DEFEAT";

	private System.Random mRnd;
	private int mlevelIndex;
	private int mCollectablesCount;
	private bool mIsPaused;

	// Getter
	public States GetState() { return mCurrentState; }

	public enum States
	{
		MENU = 0,
		GAME_LEVEL_1 = 1,
		GAME_LEVEL_2 = 2,
		GAME_LEVEL_3 = 3,
		VICTORY = 4,
		DEFEAT = 5,
	}

	public enum Levels
	{
		RESTART,
		NEXT
	}

	void Start () 
	{
		Init();
		DontDestroyOnLoad(this.gameObject);
	}

	void Init()
	{
		mGameMusic = GameObject.Find ("GameMusic").GetComponent<GameMusic>();
		mLevelList = new List<States> ();
		mRnd = new System.Random ();
		mLevelList.Add(States.GAME_LEVEL_1);
		mLevelList.Add(States.GAME_LEVEL_2);
		mLevelList.Add(States.GAME_LEVEL_3);
		mlevelIndex = mRnd.Next(-1, 2);
		mIsPaused = false;
	}

	private void NextLevel()
	{
		if (++mlevelIndex >= 3) 
		{
			mlevelIndex = 0;
		}
		ChangeLevel(mLevelList [mlevelIndex]);
	}

	public void EndLevel(int nbCollectables)
	{
		mCollectablesCount = nbCollectables;
		if (nbCollectables >= 2) {
			mGameMusic.IntroduceMusic(GameMusic.Songs.VICTORY, GameMusic.Songs.MENU);
			ChangeLevel(States.VICTORY);
		}
		else 
		{
			mGameMusic.IntroduceMusic(GameMusic.Songs.DEFEAT, GameMusic.Songs.MENU);
			ChangeLevel(States.DEFEAT);
		}
	}

	private void RestartLevel()
	{
		ChangeLevel(mLevelList [mlevelIndex]);
		UnPause();
	}

	public void LoadLevel(GameFlow.Levels option)
	{
		switch (option) 
		{
		case Levels.NEXT:
			Application.LoadLevel(Loading);
			Invoke ("NextLevel", 3);
			break;
		case Levels.RESTART:
			Application.LoadLevel(Loading);
			Invoke ("RestartLevel", 3);
			break;
		}
	}

	public void ChangeLevel(States state)
	{
		if (mCurrentState != state) 
		{
			mCurrentState = state;

			switch (state) 
			{
			case (States.MENU):
				Application.LoadLevel(Menu);
				break;
			case (States.GAME_LEVEL_1):
				Application.LoadLevel(Level_1);
				break;
			case (States.GAME_LEVEL_2):
				Application.LoadLevel(Level_2);
				break;
			case (States.GAME_LEVEL_3):
				Application.LoadLevel(Level_3);
				break;
			case (States.VICTORY):
				Application.LoadLevel(Victory);
				break;
			case (States.DEFEAT):
				Application.LoadLevel(Defeat);
				break;
			default:
				break;
			}
		}
	}

	public int GetCollectableCount()
	{
		return mCollectablesCount;
	}

	public void Pause()
	{
		if (mIsPaused) {
			mIsPaused = false;
			Time.timeScale = 0;
		}
		else
		{
			mIsPaused = true;
			Time.timeScale = 1;
		}
	}

	public void UnPause()
	{
		mIsPaused = false;
		Time.timeScale = 1;
	}

	public void RestartGame()
	{
		ChangeLevel(States.MENU);
		UnPause();
	}
}
