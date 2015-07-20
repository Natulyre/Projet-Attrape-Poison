using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    // All DEM consts!!
    private const string COLLECTABLE = "Collectable";
    private const string BADY = "Bady";
    private const string SMOKE = "Smoke";
    private const string DOOR = "Door";
    private const string FLOOR = "Floor";

    // Public variables, designer stuff
    public float mSpeed;
    public float mMaxSpeed;
    public float mMinSpeed;
    public float mJumpForce;
    // public float mToxicityMultiplier;
    
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

	private Transform currentTarget;

    // Use this for initialization
    void Start()
    {
        Init();

		currentTarget = null;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;


        //Debug.Log(mInAir);
        //Debug.Log("Collectables: " + mCollectablesCount);

        if (mIsdead)
        {
            Respawn();
        }

        HandleInput(dt);
    }

   // Debug Draw Lines
   void OnCollisionStay2D(Collision2D col)
   {
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
            Debug.Log("Anims and shits here");
            
			//Play Sound
			mSource.PlayOneShot(mLandSound, mVol);
        }
    }

    // Handle all the trigegr possible in the game
    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case COLLECTABLE:
				// Play Sound
				mSource.PlayOneShot(mGetCollectable, mVol);

                // DisplayCollectible(mCollectablesCount);
                
				// Make Collectable dissappear on Collision
				col.GetComponent<Collectable>().Vanish();
				
                mCollectablesCount++;
                Debug.Log("Collectable picked up!");
                break;

            case BADY:
				// Play Sound
				mSource.PlayOneShot(mCollision, mVol);

                // DisplayToxicity(mToxicity);
                // col.gameObject.transform.parent.Vanish();
                ApplyToxicity();
                Collider2D temp = col.GetComponent<Collider2D>();
                Fallable fal = temp.GetComponent<Fallable>();
                fal.Vanish();
                break;

            case SMOKE:
				// Play Sound
				mSource.PlayOneShot(mCough, mVol);  

				// Play Cough Anim
				
                mIsdead = true;
                break;

            case DOOR:
                // LaunchScreen(mCollectablesCount);
                break;
        }
    }

    // Init all the variables that needs to be
    private void Init()
    {
        mBody = GetComponent<Rigidbody2D>();
        mToxicity = 0.3f;
        mCurrentSpeed = mSpeed;
        mCollectablesCount = 0;
        mInAir = false;
        mIsdead = false;

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

			// Play Sound
			mSource.PlayOneShot(mJumpSound, mVol);
            
			mInAir = true;
            mBody.AddForce(new Vector2(0, mJumpForce), ForceMode2D.Impulse);
        }
    }

    // Handle all the input related things, jump, movement, etc...
    private void HandleInput(float dt)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft(dt);
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveRight(dt);
        }

    }

    // Handle the respawn of the player
    private void Respawn()
    {
        Application.LoadLevel("Prototype");
    }

    // Make the player go right
    private void MoveRight(float dt)
    {
        // RunLeft anim here
        mBody.velocity += mRight * mCurrentSpeed * dt;

        if (mBody.velocity.x >= mMaxSpeed)
        {
            mBody.velocity -= mRight * mCurrentSpeed * dt;
        }
    }

    // Make the player go left
    private void MoveLeft(float dt)
    {
        // RunRight anim here
        mBody.velocity += mLeft * mCurrentSpeed * dt;

        if (mBody.velocity.x <= -mMaxSpeed)
        {
            mBody.velocity -= mLeft * mCurrentSpeed * dt;
        }
    }

    // Reduce the player speed according to the toxicity
    private void ApplyToxicity()
    {
        // mToxicity += mToxicityMultiplier;
        // mCurrentSpeed -= mToxicity
        mMaxSpeed -= mToxicity;

        if (mMaxSpeed <= mMinSpeed)
        {
            mMaxSpeed = mMinSpeed;
            // mCurrentSpeed = mMinSpeed;
        }
    }
}
