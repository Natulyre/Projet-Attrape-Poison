using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Door : MonoBehaviour 
{

	//private List<int> mScreenList = new List<int>();

	// Audio Clips
	public AudioClip mLosing;
	public AudioClip mVictory;

	// Audio Source variables
	private AudioSource mSource;
	private float mVol = 1.0f;

	// Use this for initialization
	void Start () 
	{
		Init ();
	}
	
	void Init()
	{
		mSource = GetComponent<AudioSource>();

		//Init mScreenList
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	// Lauches the Victory or Losing Screen depening on the number of collectables
	public void LaunchScreen(int p_nbCollectables)
	{
		if (p_nbCollectables <= 1)
		{
			Debug.Log ("Losing Screen");
			mSource.PlayOneShot(mLosing, mVol);
		}
		else if (p_nbCollectables >= 2)
		{
			Debug.Log ("Victory Screen");
			mSource.PlayOneShot(mVictory, mVol);
		}
	}
}
