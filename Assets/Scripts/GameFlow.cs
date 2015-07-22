using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameFlow : MonoBehaviour {
	private States mCurrentState;
	private List<States> mLevelList;
	
	private const string Menu = "START_SCREEN"; 
	private const string Level_1 = "LEVEL_TEST_ONE";
	private const string Level_2 = "LEVEL_TEST_TWO";
	private const string Level_3 = "LEVEL_TEST_THREE";
	private const string Victory = "VICTORY";
	private const string Defeat = "DEFEAT";

	private System.Random mRnd;
	private int levelIndex;

	public enum States
	{
		MENU = 0,
		GAME_LEVEL_1 = 1,
		GAME_LEVEL_2 = 2,
		GAME_LEVEL_3 = 3,
		VICTORY = 4,
		DEFEAT = 5,
	}

	void Start () 
	{
		Init();
		DontDestroyOnLoad(this.gameObject);
	}

	void Init()
	{
		mLevelList = new List<States> ();
		mRnd = new System.Random ();
		mLevelList.Add(States.GAME_LEVEL_1);
		mLevelList.Add(States.GAME_LEVEL_2);
		mLevelList.Add(States.GAME_LEVEL_3);
		levelIndex = mRnd.Next(0, 2);
	}

	public void NextLevel()
	{
		if (++levelIndex >= 3) 
		{
			levelIndex = 0;
		}
		ChangeLevel(mLevelList [levelIndex]);
	}

	public void RestartLevel()
	{
		ChangeLevel(mLevelList [levelIndex]);
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
				Application.LoadLevel(Level_1);
				break;
			case (States.GAME_LEVEL_3):
				Application.LoadLevel(Level_1);
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

}
