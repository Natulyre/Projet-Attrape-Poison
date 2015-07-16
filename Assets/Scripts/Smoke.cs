using UnityEngine;
using System.Collections;

public class Smoke : MonoBehaviour 
{
	// Variables for Smoke
	public float mSpeed;
	public float mDistanceMade;
	public bool mIsRaising;

	// Camera
	public Camera mCam;
	public float mCamHeight;
	public Vector3 mCamPos;

	void Start () 
	{
		Init ();	
	}

	void Init()
	{
		// Values
		mSpeed = 0.05f;
		mDistanceMade = 0.0f;
		mIsRaising = true;

		// Cam
		mCam = Camera.main;
		mCamHeight = mCam.orthographicSize;

		// Get the camera's inital position
		UpdateCamPos();

		// Place Smoke as the bottom of the camera)
		transform.position = new Vector3(mCamPos.x, mCamPos.y - (mCamHeight * 2), 0.0f);
	}

	void Update () 
	{
		UpdateCamPos();
		Raise ();
	}

	private void UpdateCamPos()
	{
		mCamPos = mCam.transform.position;  
	}
	
	private void Raise()
	{
		transform.position = new Vector3(mCamPos.x, mCamPos.y - (mCamHeight * 1.6f) + mDistanceMade, 0.0f);

		if (mIsRaising)
		{
			mDistanceMade += mSpeed * Time.deltaTime;
		}
	}
}
