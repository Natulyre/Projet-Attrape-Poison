using UnityEngine;
using System.Collections;

public class Smoke : MonoBehaviour 
{
	// Variables for Smoke
	public float mSpeed;
	private bool mIsRaising;
	private float mDistanceMade;

	// Camera
	private Camera mCam;
	private float mCamHeight;
	private Vector3 mInitCamPos;
	private GameFlow mGameFlow;

	//Magic Number
	private const float HEIGHT_FACTOR = 1.6f;

	void Start () 
	{
		Init ();	
	}

	void Init()
	{
		mGameFlow = GameObject.Find ("GameFlow").GetComponent<GameFlow>();
		
		//Default values
		mIsRaising = true;
		mDistanceMade = 0;

		// Cam
		mCam = Camera.main;
		mCamHeight = mCam.orthographicSize;

		// Get the camera's inital position
		mInitCamPos = mCam.transform.position;

		// Place Smoke at the bottom of the camera)
		transform.position = new Vector3(mInitCamPos.x, mInitCamPos.y - (mCamHeight * HEIGHT_FACTOR), 0.0f);
	}

	void Update () 
	{
		if (!mGameFlow.GetSmokePaused()) 
		{
			
			Raise ();
		}
	}
	
	private void Raise()
	{
		if (!mGameFlow.GetSmokePaused ()) {
			transform.position = (new Vector3 (mInitCamPos.x,
		                                  mInitCamPos.y - (mCamHeight * HEIGHT_FACTOR) + mDistanceMade,
		                                  0.0f));
			if (mIsRaising) {
				mDistanceMade += mSpeed * Time.deltaTime;
			}
		}
	}
}
