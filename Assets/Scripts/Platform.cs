using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour 
{
	public float mRange;
	public float mSpeed;
	private float mOffset;

	private Vector3 mMaxPosUp;
	private Vector3 mMaxPosDown;
	private Vector3 mNewPos;

	void Start () 
	{
		Init ();
	}

	void Init()
	{
		// Values
		mRange = 6.0f;
		mSpeed = 2.0f;
		mOffset = 0.2f;
		
		// Start and Max Pos
		mMaxPosDown = transform.position;
		mMaxPosUp = transform.position;
		mMaxPosUp.y += mRange;
		
		mNewPos = mMaxPosUp;
	}

	void Update () 
	{
		Movement();	
	}

	void Movement()
	{
		// Reaches Up, goes back down
		if (transform.position.y >= mMaxPosUp.y - mOffset)
		{
			mNewPos = mMaxPosDown;
		}

		// Reaches Max down, goes back up
		if (transform.position.y <= mMaxPosDown.y + mOffset)
		{
			mNewPos = mMaxPosUp;
		}

		transform.position = Vector3.Lerp(transform.position, mNewPos, mSpeed * Time.deltaTime);
	}
}
