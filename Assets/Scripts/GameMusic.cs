using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour 
{
	// Instance
	private static GameMusic instance = null;
	private static GameMusic Instance{ get { return instance; } }

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
	
	private void PlayMusic()
	{
		mSource.Play(0);
	}
}
