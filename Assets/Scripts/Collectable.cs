using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour, IVanishable
{
	Animator mAnimatorTest;
	SpriteRenderer mSpriteRenderer;
	BoxCollider2D mBoxCollider2d;

	void Start () 
	{
		 mAnimatorTest = GetComponent<Animator>();
	}

	void Update () 
	{
		Disable();
	}

	public void Vanish()
	{
		mAnimatorTest.SetTrigger("Collected");

		mSpriteRenderer = GetComponent<SpriteRenderer>();
		mBoxCollider2d = GetComponent<BoxCollider2D>();
	}

	public void Disable()
	{
		if(mAnimatorTest.GetCurrentAnimatorStateInfo(0).IsName("IDLE_VANISH"))
		{	
			mSpriteRenderer.enabled = false;
			mBoxCollider2d.enabled = false;
		}
	}
}
