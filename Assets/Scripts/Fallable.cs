﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fallable : MonoBehaviour {
	
	public float mSpeed;

	//Spawning
	public const int NUMBER_OF_SECTIONS = 3;
	public const float OFFSET_FROM_SCREEN_BORDERS = 0.5f;
	public const float OFFSET_FROM_SCREEN_HEIGHT = 4f;
	private float mSpawnY;
	
	// Handle actual spawning
	private static System.Random mRnd;
	private float mSpawnSectionLength;
	private float mRandomX;
	private Vector3 mSpawnPoint;
	private Vector3 mReSpawnPoint;
	private List<int> mSectionList = new List<int>();

    // Camera
    private Camera mCam;
    private float mCamHeight;
    private float mCamWidth;
    private Vector3 mCamPos;
		
    public SpriteRenderer spriteRenderer; 
    public BoxCollider2D boxCollider2d;


    void Start()
    {
        Init();
    }

    void Update()
    {
        UpdateCamPos();
        Move();
        Respawn();
    }

    void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2d = GetComponent<BoxCollider2D>();
		
        mSpeed = -5.0f;
		mSpawnY = mCamPos.y + mCamHeight + OFFSET_FROM_SCREEN_HEIGHT;

        // Camera
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
        ManagePoints();

        transform.position = mSpawnPoint;

        Vanish();
    }

    void InitSectionList()
    {
        for (int i = 0; i < NUMBER_OF_SECTIONS * 2; i++)
        {
            mSectionList.Add(i);
            mSectionList.Add(i + 1);
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

    // Spawn the object & activate it
    public void Spawn()
    {
        gameObject.SetActive(true);
        spriteRenderer.enabled = true;
        boxCollider2d.enabled = true;

        ManagePoints();
        
        transform.position = mSpawnPoint;
    }

    // Vanish method from the IVanishable interface, desactivate the object
    public void Vanish()
    {
        spriteRenderer.enabled = false;
        boxCollider2d.enabled = false;
        gameObject.SetActive(false);
    }

    private void Move()
    {
        transform.Translate(0.0f, mSpeed * Time.deltaTime, 0.0f);
    }

    private void UpdateCamPos()
    {
		if (mSpawnY < mCamPos.y + mCamHeight + OFFSET_FROM_SCREEN_HEIGHT) 
		{
			mSpawnY = mCamPos.y + mCamHeight + OFFSET_FROM_SCREEN_HEIGHT;
		}
        mCamPos = mCam.ScreenToWorldPoint(mCam.transform.position);
        mCamPos = mCam.transform.position;
    }
    
    private void ManagePoints()
    {
        // Get Beginning and End point of camera
        float startCamX = mCamPos.x - mCamWidth + OFFSET_FROM_SCREEN_BORDERS;

        // Randomize a section to be choosen
        int randomIndex = mRnd.Next(mSectionList.Count - 1);

        // Randomize and position in the selected section
        mRandomX = Random.Range(startCamX + randomIndex * mSpawnSectionLength, mSectionList[randomIndex] * mSpawnSectionLength - OFFSET_FROM_SCREEN_BORDERS);

        // Remove Section Chosen from list
        mSectionList.RemoveAt(randomIndex);

        // Check if there is only half the remaining elements in the list
        CheckSectionList();

        // Define the Spawn and Respawn points

        mSpawnPoint = new Vector3(mRandomX, mSpawnY + OFFSET_FROM_SCREEN_HEIGHT, 0.0f);
        mReSpawnPoint = new Vector3(mRandomX, mCamPos.y - (mCamHeight * 2), 0.0f);
    }

    // Desactivate when out of screen
    private void Respawn()
    {
        if(transform.position.y <= mReSpawnPoint.y)
        {
            Vanish();
        }
    }
}
