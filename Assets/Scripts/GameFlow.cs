using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameFlow : MonoBehaviour 
{
	private States mCurrentState;
	private List<States> mLevelList;
	private GameMusic mGameMusic;

	private const string MENU = "SCREEN_START"; 
	private const string LEVEL_1 = "LEVEL_ONE";
	private const string LEVEL_2 = "LEVEL_TWO";
	private const string LEVEL_3 = "LEVEL_THREE";
	private const string LOADING = "SCREEN_LOADING";
    private const string END_LEVEL = "SCREEN_END";

    // Images
    public Sprite WIN_1; 
    public Sprite WIN_2; 
    public Sprite WIN_3; 
    public Sprite LOSE_1;
    public Sprite LOSE_2;
    public Sprite LOSE_3;
    public Sprite PARTIAL_1; 
    public Sprite PARTIAL_2; 
    public Sprite PARTIAL_3; 

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
		END = 4
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

    void OnLevelWasLoaded()
    {
        if(Application.loadedLevelName == END_LEVEL)
        {
            SetEndBG();
        }
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
			ChangeLevel(States.END);
		}
		else 
		{
			mGameMusic.IntroduceMusic(GameMusic.Songs.DEFEAT, GameMusic.Songs.MENU);
			ChangeLevel(States.END);
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
			case (States.END):
                Application.LoadLevel(END_LEVEL);
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
        GameObject temp = GameObject.Find("Background");
        Debug.Log(temp);
        //Image tempImage = temp.GetComponentInChildren<Image>();
        Image tempImage = temp.GetComponent<Image>();

		ShowHideReplayBtn();

        switch(mlevelIndex)
        {
            case 0:
                switch(mCollectablesCount)
                {
                    case 0 :
                        tempImage.sprite = LOSE_1;
                        break;
                    case 1:
                        tempImage.sprite = LOSE_1;
                        break;
                    case 2:
                        tempImage.sprite = PARTIAL_1;
                        break;
                    case 3:
                        tempImage.sprite = WIN_1;
                        break;
                }
                break;
            case 1:
                switch (mCollectablesCount)
                {
                    case 0:
                        tempImage.sprite = LOSE_2;
                        break;
                    case 1:
                        tempImage.sprite = LOSE_2;
                        break;
                    case 2:
                        tempImage.sprite = PARTIAL_2;
                        break;
                    case 3:
                        tempImage.sprite = WIN_2;
                        break;
                }
                break;
            case 2:
                switch (mCollectablesCount)
                {
                    case 0:
                        tempImage.sprite = LOSE_3;
                        break;
                    case 1:
                        tempImage.sprite = LOSE_3;
                        break;
                    case 2:
                        tempImage.sprite = PARTIAL_3;
                        break;
                    case 3:
                        tempImage.sprite = WIN_3;
                        break;
                }
                break;
        }
    }

	// Hides the Replay Button if the Player found all 3 files
	private void ShowHideReplayBtn()
	{
		GameObject tmpRestartBtn = GameObject.Find ("Restart Level");

		if (mCollectablesCount == 3)
		{
			tmpRestartBtn.SetActive(false);
		}
		else
		{
			tmpRestartBtn.SetActive(true);
		}
	}
	
	public bool GetPaused()
	{
		return mIsPaused;
	}
}
