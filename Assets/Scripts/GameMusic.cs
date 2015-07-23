using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour 
{
	public float mVol = 1.0f;
	
	public enum Songs
	{
		LEVEL = 0,
		MENU = 1,
		VICTORY = 2,
		DEFEAT = 3,
		NONE = 4
	}
	
	// Audio Source variables
	private AudioSource mSource;
	private Songs mCurrentSong;
	private Songs mNextSong;
	
	private const string MUSIC_PATH = "Audio/Music/";
	private const string MUSIC_LEVEL = "MUSIC_THEME";
	private const string MUSIC_MENU = "MUSIC_MENU";
	
	private const string STINGER_PATH = "Audio/Stingers/";
	private const string STINGER_VICTORY = "STINGER_VICTORY";
	private const string STINGER_DEFEAT = "STINGER_DEFEAT";
	
	private AudioClip[] mClips;
	private bool mStingerish;
	
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
		mStingerish = false;
	}

	void Update()
	{
		if (mStingerish && !mSource.isPlaying) 
		{
			mStingerish = false;
			PlayMusic(mNextSong);
		}
	}
	
	void LoadResources()
	{
		mClips = new AudioClip[]{(AudioClip)Resources.Load (MUSIC_PATH + MUSIC_LEVEL),
								 (AudioClip)Resources.Load (MUSIC_PATH + MUSIC_MENU),
						 		 (AudioClip)Resources.Load (STINGER_PATH + STINGER_VICTORY),
								 (AudioClip)Resources.Load (STINGER_PATH + STINGER_DEFEAT)};
	}
	
	public void PlayMusic(Songs song)
	{
		if (song != mCurrentSong) 
		{
			mStingerish = false;
			mSource.loop = true;
			mCurrentSong = song;
			mSource.clip = mClips[(int)song];
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
	
	public void PlaySound(AudioClip sound)
	{
		mSource.PlayOneShot(sound, mVol);
	}
	
	public void IntroduceMusic(Songs stinger, Songs music)
	{
		mStingerish = true;
		mSource.clip = mClips[(int)stinger];
		mSource.loop = false;
		mSource.Play(0);
		mNextSong = music;
		mCurrentSong = Songs.NONE;
	}
	
}
