using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour 
{
	public float mVol = 1.0f;

	// Audio Source variables
	private AudioSource mSource;
	private bool mSoundOn;
	private Songs mCurrentSong;

	private const string PATH = "Audio/Music/";
	private const string MUSIC_LEVEL = "LevelMusicPlaceHolder";
	private const string MUSIC_MENU = "MenuMusicPlaceHolder";

	public enum Songs
	{
		LEVEL = 0,
		MENU = 1
	}

	private AudioClip[] clips;

	void Awake()
	{	
		LoadResources();
		DontDestroyOnLoad(this.gameObject);
	}

	void Start()
	{
		Init();
		PlayMusic(Songs.MENU);
	}

	void Init()
	{
		mSource = GetComponent<AudioSource>();
        mSoundOn = true;
	}

	void LoadResources()
	{
		clips = new AudioClip[]{(AudioClip)Resources.Load (PATH + MUSIC_LEVEL),
							    (AudioClip)Resources.Load (PATH + MUSIC_MENU)};
	}
	
	public void PlayMusic(Songs song)
	{
		if (song != mCurrentSong) 
		{
			mCurrentSong = song;
			mSource.clip = clips[(int)song];
			StopMusic();
		}
		if (!mSource.isPlaying) 
		{
			mSource.Play(0);
		}
	}
	public void StopMusic()
	{
		mSource.Stop();
	}

    public void ToggleSound()
    {
        Debug.Log("CRISS !!!!");
        mSoundOn = !mSoundOn;

        AudioListener.volume = mSoundOn ? 1 : 0;
    }

}
