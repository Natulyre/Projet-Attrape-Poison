using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour, IVanishable
{
	public float mSpeed;

	private Vector3 mSpawnPoint;
	private Vector3 mReSpawnPoint;
	private Vector3 mMovingVector;
	
	void Start () 
	{
		Camera cam = Camera.main;
		float height = cam.orthographicSize;
		//float width = height * cam.aspect;
		Vector3 camPos = cam.transform.position;


		mSpeed = -10.0f;

		
		mSpawnPoint = new Vector3(0.0f, camPos.y + height, 0.0f);
		mReSpawnPoint = new Vector3(0.0f, camPos.y - height, 0.0f);

		//mSpawnPoint = new Vector3(0.0f, 7.0f, 0.0f);
		//mReSpawnPoint = new Vector3(0.0f, -7.0f, 0.0f);
		
		transform.position = mSpawnPoint;

		//Spawn();
	}

	void Update () 
	{
		Move ();

		Spawn ();

		Debug.Log (transform.position.y);
	}

	void Spawn()
	{
	 	if (transform.position.y <= mReSpawnPoint.y)
		{
			transform.position = mSpawnPoint;
		}
	}

	public void Vanish()
	{

	}

	private void Move()
	{
		transform.Translate(0.0f, mSpeed * Time.deltaTime, 0.0f);
	}
}
