using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameFlow : MonoBehaviour {
	private States mCurrentState;
	private List<States> mLevelList;
	private GameMusic mGameMusic;
	
	private const string MENU = "SCREEN_START"; 
	private const string LEVEL_1 = "LEVEL_ONE";
	private const string LEVEL_2 = "LEVEL_TWO";
	private const string LEVEL_3 = "LEVEL_THREE";
	private const string LOADING = "SCREEN_LOADING";
	private const string END = "SCREEN_END";

    // Images
    public Image WIN_1; 
    public Image WIN_2; 
    public Image WIN_3; 
    public Image LOSE_1;
    public Image LOSE_2;
    public Image LOSE_3;
    public Image PARTIAL_1; 
    public Image PARTIAL_2; 
    public Image PARTIAL_3; 

    // Local Images variables
    private Image winImage1;
    private Image winImage2;
    private Image winImage3;
    private Image loseImage1;
    private Image loseImage2;
    private Image loseImage3;
    private Image insImage1;
    private Image insImage2;
    private Image isnImage3;

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
			Application.LoadLevel(LOADING);
			Invoke ("NextLevel", 3);
			break;
		case Levels.RESTART:
			Application.LoadLevel(LOADING);
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
				Application.LoadLevel(MENU);
				break;
			case (States.GAME_LEVEL_1):
				Application.LoadLevel(LEVEL_1);
				break;
			case (States.GAME_LEVEL_2):
				Application.LoadLevel(LEVEL_2);
				break;
			case (States.GAME_LEVEL_3):
				Application.LoadLevel(LEVEL_3);
				break;
			case (States.VICTORY):
                SetEndBG();
				Application.LoadLevel(END);
				break;
			case (States.DEFEAT):
                SetEndBG();
				Application.LoadLevel(END);
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
		if (!mIsPaused) {
			mIsPaused = true;
			Time.timeScale = 0;
		}
		else
		{
			mIsPaused = false;
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

    private void SetEndBG()
    {
        GameObject temp = GameObject.Find("BackGround");
        Image tempImage = temp.GetComponent<Image>();
        tempImage = WIN_1;
    }
	
	public bool GetPaused()
	{
		return mIsPaused;
	}
}
