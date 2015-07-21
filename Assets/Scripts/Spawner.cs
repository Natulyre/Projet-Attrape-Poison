using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{
	//Pool of fallables to use and the spawning frequency
    public Pool mPool;
    public float mSpawnTimer;

    private float mTimer;

	void Start () 
    {
		Init ();   
	}

	void Init()
	{
		mTimer = 0;
	}

	void Update () 
    {
		Spawn();
	}

    private void Spawn()
    {
		//Update timer continously
		mTimer += Time.deltaTime;

		//Once the timer reaches the specified time
        if(mTimer >= mSpawnTimer)
        {
			//Remove the spawner's frequency from the timer
			//We do not reset it to 0 as it would cause inconsistent spawns
			mTimer -= mSpawnTimer;

			//Then retrieve an instance from the pool and spawn it
        //    Fallable FallableToSpawn = mPool.GetInstance().GetComponent<Fallable>();
         //   FallableToSpawn.Spawn();
        }
    }
}
