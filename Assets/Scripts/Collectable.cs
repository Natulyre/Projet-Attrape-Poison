using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour, IVanishable
{
	void Start () 
	{

	}

	void Update () 
	{
	
	}

	public void Vanish()
	{
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		BoxCollider2D boxCollider2d = GetComponent<BoxCollider2D>();

		spriteRenderer.enabled = true;
		boxCollider2d.enabled = true;
	}
}
