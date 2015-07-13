using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    // All DEM consts!!
    private const string COLLECTABLE = "Collectable";
    private const string BADY = "Bady";
    private const string SMOKE = "Smoke";

    // Public variables, designer stuff
    public float mSpeed;
    public float mMaxSpeed;
    public float mMinSpeed;
    public float mJumpForce;
    public float mToxicityMultiplier;

    // Private variables
    private float mToxicity;
    private float mCurrentSpeed;
    private int mCollectablesCount;
    private bool mInAir;
    private bool mIsdead;
    private Rigidbody2D mBody;
    private Vector2 mRight;
    private Vector2 mLeft;

    // Use this for initialization
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        Debug.Log("Collectables: " + mCollectablesCount);

        if (mIsdead)
        {
            Respawn();
        }

        HandleInput(dt);
    }

    // Handle all the trigegr possible in the game
    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case COLLECTABLE:
                mCollectablesCount++;
                break;

            case BADY:
                mToxicity += mToxicityMultiplier;
                ApplyToxicity();
                break;

            case SMOKE:
                mIsdead = true;
                break;
        }
    }

    // Init all the variables that needs to be
    private void Init()
    {
        mBody = GetComponent<Rigidbody2D>();
        mToxicity = 0.0f;
        mCurrentSpeed = mSpeed;
        mCollectablesCount = 0;
        mInAir = false;
        mIsdead = false;

        mRight = Vector2.right;
        mLeft = -Vector2.right;
    }

    // Make the character jump
    private void Jump()
    {
        if (mBody.velocity.y == 0)
        {
            mInAir = false;
        }

        if (!mInAir)
        {
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
        mBody.velocity += mRight * mCurrentSpeed * dt;

        if (mBody.velocity.x >= mMaxSpeed)
        {
            mBody.velocity -= mRight * mCurrentSpeed * dt;
        }
    }

    // Make the player go left
    private void MoveLeft(float dt)
    {
        mBody.velocity += mLeft * mCurrentSpeed * dt;

        if (mBody.velocity.x <= -mMaxSpeed)
        {
            mBody.velocity -= mLeft * mCurrentSpeed * dt;
        }
    }

    // Reduce the player speed according to the toxicity
    private void ApplyToxicity()
    {
        mCurrentSpeed -= mToxicity;

        if (mCurrentSpeed <= mMinSpeed)
        {
            mCurrentSpeed = mMinSpeed;
        }
    }
}
