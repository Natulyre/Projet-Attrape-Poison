using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour 
{
	// Instance
	private static GameMusic instance = null;
	private static GameMusic Instance{ get { return instance; } }

    private bool mSoundOn;

	// Audio Source variables
	private AudioSource mSource;
	public float mVol = 1.0f;

	// Use this for initialization
	void Awake () 
	{
		Singleton();
		Init();
		PlayMusic();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void Init()
	{
		mSource = GetComponent<AudioSource>();
        mSoundOn = true;
	}


	private void Singleton()
	{
		if (instance !=null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
	
	public void PlayMusic()
	{
		mSource.Play(0);
	}

	public void StopMusic()
	{
		mSource.Stop ();
	}

    public void ToggleSound()
    {
        Debug.Log("CRISS !!!!");
        mSoundOn = !mSoundOn;

        AudioListener.volume = mSoundOn ? 1 : 0;
    }

}
