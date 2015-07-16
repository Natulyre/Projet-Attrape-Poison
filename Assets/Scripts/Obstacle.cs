using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacle : MonoBehaviour, IVanishable
{
	public float mSpeed;
	public bool mHasCollided;
	
	// Cam
	public Camera mCam;
	public float mCamHeight;
	public float mCamWidth;
	public Vector3 mCamPos;

	// Needed for Spawn
	public const int NUMBER_OF_SECTIONS = 3;
	public const float OFFSET_FROM_SCREEN_BORDERS = 0.5f;
	private float mSpawnSectionLength;
	private float mRandomX;
	private Vector3 mSpawnPoint;
	private Vector3 mReSpawnPoint;
	private List<int> mSectionList = new List<int>();

	public static System.Random mRnd;
	
	void Start () 
	{
		Init ();
	}
	
	void Update () 
	{
		UpdateCamPos ();

		Move ();

		Spawn ();
	}

	void Init()
	{
		mSpeed = -5.0f;
		mHasCollided = false;
		
		// Cam
		mCam = Camera.main;
		mCamHeight = mCam.orthographicSize;
		mCamWidth = mCamHeight * mCam.aspect;

		mRnd = new System.Random();

		// Get length of each spawn sections
		mSpawnSectionLength = mCamWidth / NUMBER_OF_SECTIONS;

		// Init the list of spawn sections
		InitSectionList();
	
		// Gets the initial cam position when the obstacle is created
		UpdateCamPos();

		// Gets an initial spawn point
		ManagePoints ();

		transform.position = mSpawnPoint;
	}
	
	void InitSectionList()
	{
		for (int i = 0; i < NUMBER_OF_SECTIONS * 2; i++)
		{
			mSectionList.Add (i);
			mSectionList.Add (i+1);
			i++;
		}
	}

	void CheckSectionList()
	{
		// if there is only the same number of elements as the number of section, clear and re-initialise
		if (mSectionList.Count <= NUMBER_OF_SECTIONS)
		{
			mSectionList.Clear();
			InitSectionList();
		}
	}

	public void Spawn()
	{
	 	// If obstacle reaches the limit, it will get a new Spawn point and respawn at the top of the screen
		if (transform.position.y <= mReSpawnPoint.y)
		{
			ManagePoints();

			transform.position = mSpawnPoint;
		}
	}

	public void Vanish()
	{
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		BoxCollider2D boxCollider2d = GetComponent<BoxCollider2D>();

		if (mHasCollided)
		{
			spriteRenderer.enabled = false;
			boxCollider2d.enabled = false;
		}
		else
		{
			spriteRenderer.enabled = true;
			boxCollider2d.enabled = true;
		}
	}

	private void Move()
	{
		transform.Translate(0.0f, mSpeed * Time.deltaTime, 0.0f);
	}

	private void UpdateCamPos()
	{
		mCamPos = mCam.transform.position;
	}

	private void ManagePoints()
	{
		// Get Beginning and End point of camera
		float startCamX = mCamPos.x - mCamWidth + OFFSET_FROM_SCREEN_BORDERS;

		// Randomize a section to be choosen
		int randomIndex = mRnd.Next(mSectionList.Count - 1);

		// Randomize and position in the selected section
		mRandomX = Random.Range (startCamX, (mSectionList[randomIndex] * mSpawnSectionLength) - OFFSET_FROM_SCREEN_BORDERS);

		// Remove Section Chosen from list
		mSectionList.RemoveAt(randomIndex);

		// Check if there is only half the remaining elements in the list
		CheckSectionList();

		// Define the Spawn and Respawn points
		mSpawnPoint = new Vector3(mRandomX, mCamPos.y + mCamHeight, 0.0f);
		mReSpawnPoint = new Vector3(mRandomX, mCamPos.y - (mCamHeight * 2), 0.0f);
	}
}
