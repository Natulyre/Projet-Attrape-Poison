using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{

    public Pool mPool;
    public float mSpawnTimer;

    private float mtimer;

	// Use this for initialization
	void Start () 
    {
        mtimer = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        float dt = Time.deltaTime;
        Spawn(dt);
	}

    private void Spawn(float dt)
    {
        mtimer += dt;

        if(mtimer >= mSpawnTimer)
        {
            mtimer = 0;
            Fallable FallableToSpawn = mPool.GetInstance().GetComponent<Fallable>();
            FallableToSpawn.Spawn();
        }
    }
}
