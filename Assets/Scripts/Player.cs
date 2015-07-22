using UnityEngine;
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

    // Public variables, designer stuff
    public Image lung;
    public float mSpeed;
    public float mMaxSpeed;
    public float mMinSpeed;
    public float mJumpForce;
    
	// Audio Clips
	public AudioClip mJumpSound;
	public AudioClip mLandSound;
	public AudioClip mCollision;
	public AudioClip mCough;
	public AudioClip mGetCollectable;

	// Audio Source variables
	private AudioSource mSource;
	private float mVol = 1.0f;
	
    // Private variables
    private float mToxicity;
    private float mCurrentSpeed;
    private int mCollectablesCount;
    private bool mInAir;
    private bool mIsdead;
    private Rigidbody2D mBody;
    private Vector2 mRight;
    private Vector2 mLeft;
	private float mOpacity;
	private int mCurrentDirection;
	private GameMusic mGameMusic;

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
		CheckDeath ();
        HandleInput();
    }

	void CheckDeath()
	{
		if (mIsdead)
		{
			Respawn();
		}
	}

   // Debug Draw Lines
   void OnCollisionStay2D(Collision2D col)
   {
       mInAir = false;
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
           
		//Play Sound
		mSource.PlayOneShot(mLandSound, mVol);
       }
   }

   void OnCollisionExit2D(Collision2D col)
   {
       mInAir = true;
   }

   // Handle all the trigegr possible in the game
   void OnTriggerEnter2D(Collider2D col)
   {
       switch (col.tag)
       {
           case COLLECTABLE:
			// Play Sound
			mSource.PlayOneShot(mGetCollectable, mVol);
               
			// Make Collectable dissappear on Collision
			col.GetComponent<Collectable>().Vanish();
			
               mCollectablesCount++;
               Debug.Log("Collectable picked up!");
               break;

           case BADDY:
			// Play Sound
			mSource.PlayOneShot(mCollision, mVol);

               // DisplayToxicity(mToxicity);
               ApplyToxicity();
               Collider2D temp = col.GetComponent<Collider2D>();
               Fallable fal = temp.GetComponent<Fallable>();
               fal.Vanish();

               UpdateLung();
               break;

           case SMOKE:
			// Play Sound
			mSource.PlayOneShot(mCough, mVol);  

			// Play Cough Anim
			
               mIsdead = true;
               break;

           case DOOR:
			col.GetComponent<Door>().LaunchScreen(mCollectablesCount);
               break;
       }
   }

    // Init all the variables that needs to be
    private void Init()
    {
        mBody = GetComponent<Rigidbody2D>();
        mToxicity = 0.3f;
        mOpacity = 1;
        mCurrentSpeed = mSpeed;
        mCollectablesCount = 0;
        mInAir = false;
		mIsdead = false;
		mCurrentDirection = DIRECTION_LEFT;
		mGameMusic = GameObject.Find ("GameMusic").GetComponent<GameMusic>();
		mGameMusic.PlayMusic(GameMusic.Songs.LEVEL);



        mRight = Vector2.right;
        mLeft = -Vector2.right;

		mSource = GetComponent<AudioSource>();
    }

    // Make the character jump
    private void Jump()
    {
        if (!mInAir)
        {
            // Play Anim
			ChangeState(STATE_JUMP);

			// Play Sound
			mSource.PlayOneShot(mJumpSound, mVol);
            
			mInAir = true;
            mBody.AddForce(new Vector2(0, mJumpForce), ForceMode2D.Impulse);
        }
    }

    // Handle all the input related things, jump, movement, etc...
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
    }

    // Handle the respawn of the player
    private void Respawn()
    {
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
        mMaxSpeed -= mToxicity;
        mOpacity -= mToxicity;

        if (mMaxSpeed <= mMinSpeed)
        {
            mMaxSpeed = mMinSpeed;
        }
    }

    private void UpdateLung()
    {
        lung.color = new Color(lung.color.r, lung.color.g, lung.color.b, mOpacity);
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
}
