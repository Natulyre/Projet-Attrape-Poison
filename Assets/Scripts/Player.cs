﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    // All DEM consts!!
    private const string COLLECTABLE = "Collectable";
    private const string BADDY = "Baddy";
    private const string SMOKE = "Smoke";
    private const string DOOR = "Door";
    private const string FLOOR = "Floor";
	private const float MAX_TIMER = 0.6f;
	private const float MAX_TOXIC_TIMER = 3.0f;
    private Color BASE_COLOR = Color.black;


    // Public variables, designer stuff
    public Color ONE_COLOR;
    public Color TWO_COLOR;
    public Color THREE_COLOR; 

    public Image mLung;
    public Image mFolder1;
    public Image mFolder2;
    public Image mFolder3;
    public float mSpeed;
    public float mMaxSpeed;
	public float mTmpMaxSpeed;
    public float mMinSpeed;
    public float mJumpForce;
    
	// Audio Clips
	public AudioClip mJumpSound;
	public AudioClip mLandSound;
	public AudioClip mCollision;
	public AudioClip mCough;
	public AudioClip mGetCollectable;

	
    // Private variables
    private float mToxicity;
	private float mPrevToxicity;
	private bool mTouchedBady;
    private float mCurrentSpeed;
    private int mCollectablesCount;
    private bool mInAir;
    private bool mIsdead;
	private bool mPressingAction;
	private bool mIsOnDoor;
    private SpriteRenderer mRenderer;
    private Rigidbody2D mBody;
    private Vector2 mRight;
    private Vector2 mLeft;
	private float mOpacity;
	private float mTmpOpacity;
	private int mCurrentDirection;
	private GameMusic mGameMusic;
	private GameFlow mGameFlow;
	private Animator mAnimator;
	private bool mTimerOn;
	private float mTimer;
	private bool mToxicTimerOn;
	private float mToxicTimer;
    private Color mplayerColor;

	private const int STATE_IDLE = 0;
	private const int STATE_RUN = 1;
	private const int STATE_JUMP = 2;
	
	private const int DIRECTION_LEFT = 0;
	private const int DIRECTION_RIGHT = 1;



    // Use this for initialization
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
	{
		if (!mGameFlow.GetPaused () && !mGameFlow.GetPaused()) 
		{
			HandleInput ();
			CheckDeath ();
			CheckExit ();
			mAnimator.SetBool ("isJumping", mInAir);
			Timer ();
			ToxicTimer();
			VeriftyToxicity();
		}
		HandlePause();
    }

	void CheckDeath()
	{
		if (mIsdead)
		{
			mGameFlow.PauseSmoke (true);
			mInAir = false;
			mBody.velocity = Vector2.zero;

			// Fix smoke that is still going up

			if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName ("CHARACTER_DESTROY_IDLE"))
			{
				Respawn();
			}
		}
	}

   // Debug Draw Lines
   void OnCollisionStay2D(Collision2D col)
   {
       if (!(col.contacts[0].normal == Vector2.up))
       {
           mInAir = true;
       }
       else
       {
           mInAir = false;
		}

       foreach (ContactPoint2D contact in col.contacts)
       {
           Debug.DrawRay(contact.point, contact.normal, Color.white);
       }
   }

   void OnCollisionEnter2D(Collision2D col)
   {
       if (col.collider.CompareTag(FLOOR) && col.contacts[0].normal == Vector2.up)
       {
           	mInAir = false;
           	//Debug.Log("Anims and shits here"); 
           
			//Play Sound if timer has reached his maximum
			if (mTimer >= MAX_TIMER)
			{
				mGameMusic.PlaySound(mLandSound);
				mTimerOn = false;
			}
       }
   }

   void OnCollisionExit2D(Collision2D col)
   {
		mInAir = true;

		// Leaving Floor, starts/reset timer
		if (col.collider.tag == FLOOR)
		{	
			mTimerOn = true;
			mTimer = 0.0f;
		}
   }

   // Handle all the trigegr possible in the game
   void OnTriggerEnter2D(Collider2D col)
   {
       switch (col.tag)
       {
           case COLLECTABLE:
			// Play Sound
			mGameMusic.PlaySound(mGetCollectable);
               
			// Make Collectable dissappear on Collision
				col.GetComponent<Collectable>().Vanish();
			
               mCollectablesCount++;
               UpdateColor();
               UpdateFolders();
               break;

           case BADDY:
				// Play Sound
				mGameMusic.PlaySound(mCollision);

               // DisplayToxicity(mToxicity);
               ApplyToxicity();
               Collider2D temp = col.GetComponent<Collider2D>();
               Fallable fal = temp.GetComponent<Fallable>();
               fal.Vanish();

               UpdateLung();
               break;

           case SMOKE:
				// Play Sound
				if (!mIsdead)
				{
					// Play Cough Anim
					mGameMusic.PlaySound(mCough);
					mAnimator.SetTrigger ("Death");
					mIsdead = true;
				}
               	break;

           case DOOR:
				mIsOnDoor = true;
               break;
       }
   }

    // Init all the variables that needs to be
    private void Init()
	{
		mGameFlow = GameObject.Find ("GameFlow").GetComponent<GameFlow>();
		mGameFlow.PauseSmoke (false);
        mRenderer = GetComponent<SpriteRenderer>();
    	mRenderer.color = BASE_COLOR;
        mBody = GetComponent<Rigidbody2D>();
        mToxicity = 0.3f;
		mPrevToxicity = 0.0f;
        mOpacity = 1;
		mTmpOpacity = mOpacity;
        mCurrentSpeed = mSpeed;
		mTmpMaxSpeed = mMaxSpeed;
        mCollectablesCount = 0;
        mInAir = false;
		mIsdead = false;
		mCurrentDirection = DIRECTION_LEFT;
		mGameMusic = GameObject.Find ("GameMusic").GetComponent<GameMusic>();
		mAnimator = GetComponent<Animator>();
		mGameMusic.PlayMusic(GameMusic.Songs.LEVEL);
		mTimerOn = true;
		mTimer = MAX_TIMER;
		mToxicTimerOn = false;
		mToxicTimer = 0.0f;

        mFolder1.enabled = true;
        mFolder2.enabled = true;
        mFolder3.enabled = true;

        mRight = Vector2.right;
        mLeft = -Vector2.right;
    }

    // Make the character jump
    private void Jump()
    {
        if (!mInAir)
        {
            // Play Anim
			ChangeState(STATE_JUMP);

			// Play Sound
			mGameMusic.PlaySound(mJumpSound);
            
			mInAir = true;
            mBody.AddForce(new Vector2(0, mJumpForce), ForceMode2D.Impulse);
        }
    }

    // Handle all the input related things, jump, movement, etc...
    private void HandleInput()
    {
		float inputX = Input.GetAxisRaw ("Horizontal");
		bool moving = (Mathf.Abs (inputX)) > 0;

		mAnimator.SetBool ("isMoving", moving);


        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
			MoveLeft();
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
		}
		mPressingAction = Input.GetKey (KeyCode.Space);


    }

	private void HandlePause()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			mGameFlow.Pause();
		}
	}

	private void CheckExit()
	{
		if (mPressingAction && mIsOnDoor) 
		{
			mGameFlow.EndLevel(mCollectablesCount);
		}
	}

    // Handle the respawn of the player
    private void Respawn()
	{
		mGameFlow.PauseSmoke (false);
		Application.LoadLevel(Application.loadedLevelName);
    }

    // Make the player go right
    private void MoveRight()
    {
		//Update the player's direction
		ChangeDirection(DIRECTION_LEFT);

		Vector2 movement = mRight * mCurrentSpeed * Time.deltaTime;

		mBody.velocity += movement;

        if (mBody.velocity.x >= mMaxSpeed)
        {
			mBody.velocity -= movement;
        }
    }

    // Make the player go left
    private void MoveLeft()
	{
		//Update the player's direction
		ChangeDirection(DIRECTION_RIGHT);

		Vector2 movement = mLeft * mCurrentSpeed * Time.deltaTime;

		mBody.velocity += movement;

        if (mBody.velocity.x <= -mMaxSpeed)
        {
			mBody.velocity -= movement;
        }
    }

    // Reduce the player's max speed according to the toxicity
    private void ApplyToxicity()
    {
		if (!mTouchedBady)
		{
			mTouchedBady = true;
			mToxicTimerOn = true;
			mMaxSpeed -= (mToxicity + mPrevToxicity);
			mOpacity -= (mToxicity + mPrevToxicity);
			mPrevToxicity += mToxicity;
			
			if (mMaxSpeed <= mMinSpeed)
			{
				mMaxSpeed = mMinSpeed;
			}
		}
    }

	// Verifies if Toxiciy has to be applied or not
	private void VeriftyToxicity()
	{
		if (mToxicTimerOn && mToxicTimer >= MAX_TOXIC_TIMER)
		{	
			mMaxSpeed = mTmpMaxSpeed;
			mOpacity = mTmpOpacity;
			mToxicTimerOn = false;
			mTouchedBady = false;
		}
	}

    private void UpdateLung()
    {
        mLung.color = new Color(mLung.color.r, mLung.color.g, mLung.color.b, mOpacity);
    }

	//Changes animation state
	private void ChangeState(int state)
	{
		switch (state) 
		{
		case STATE_IDLE:
			break;
		case STATE_RUN:
			break;
		case STATE_JUMP:
			break;
		default:
			break;
		}
	}

	//Changes sprite's direction
	private void ChangeDirection(int direction)
	{

		if (mCurrentDirection != direction) 
		{
			mCurrentDirection = direction;
			switch (direction) 
			{
			case DIRECTION_LEFT:
				transform.Rotate (0, -180, 0);
				break;
			case DIRECTION_RIGHT:
				transform.Rotate (0, -180, 0);
				break;
			default:
				break;
			}
		}
	}

    // Very timer
	private void Timer()
	{
		if (mTimerOn)
		{
			mTimer += Time.deltaTime;
		}
	}

	// Very toxic timer
	private void ToxicTimer()
	{
		if (mToxicTimerOn)
		{
			mToxicTimer += Time.deltaTime;
		}
		else
		{
			mToxicTimer = 0.0f;
		}
	}

    // Update the character color when collectable picked
    private void UpdateColor()
    {
        switch(mCollectablesCount)
        {
            case 1:
                mRenderer.color = ONE_COLOR;
                break;
            case 2:
                mRenderer.color = TWO_COLOR;
                break;
            case 3:
                mRenderer.color = THREE_COLOR;
                break;
        }
    }

    // Update the folders in the UI whenpicked
    private void UpdateFolders()
    {
        switch (mCollectablesCount)
        {
            case 1:
                mFolder1.color = new Color(mFolder1.color.r, mFolder1.color.g, mFolder1.color.b, 0);
                break;
            case 2:
                mFolder2.color = new Color(mFolder2.color.r, mFolder2.color.g, mFolder2.color.b, 0);
                break;
            case 3:
                mFolder3.color = new Color(mFolder3.color.r, mFolder3.color.g, mFolder3.color.b, 0);
                break;
        }
    }
}
