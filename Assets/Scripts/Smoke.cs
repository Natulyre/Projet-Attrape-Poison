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

	//Magic Number
	private const float HEIGHT_FACTOR = 1.6f;

	void Start () 
	{
		Init ();	
	}

	void Init()
	{
		
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
		Raise ();
	}
	
	private void Raise()
	{
		transform.position = (new Vector3 (mInitCamPos.x,
		                                  mInitCamPos.y - (mCamHeight * HEIGHT_FACTOR) + mDistanceMade,
		                                  0.0f));
		if (mIsRaising)
		{
			mDistanceMade += mSpeed * Time.deltaTime;
		}
	}
}
