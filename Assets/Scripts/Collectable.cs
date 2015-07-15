using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour, IVanishable
{
	public bool mHasCollided;

	void Start () 
	{
		mHasCollided = false;
	}

	void Update () 
	{
	
	}

	public void Vanish()
	{
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		BoxCollider2D boxCollider2d = GetComponent<BoxCollider2D>();
		
		if (mHasCollided)
		{
			spriteRenderer.enabled = true;
			boxCollider2d.enabled = true;
		}
		else
		{
			spriteRenderer.enabled = false;
			boxCollider2d.enabled = false;
			mHasCollided = false;
		}
	}
}
