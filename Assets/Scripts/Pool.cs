using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// Pool class acting as a container for our Fallables
/// 
/// </summary>

public class Pool : MonoBehaviour 
{
    // Adjustable variables
	public int mNbOfFallable;
	public Fallable mFallable;

    // Private list
	private List<Fallable> mFallables;
	
	// Initialisation of all of the fallables in the pool
	private void Awake () 
	{		
        mFallables = new List<Fallable>();

        for (int i = 0; i < mNbOfFallable; i++)
		{
			GameObject obj = Instantiate(mFallable.gameObject);
            Fallable tempFallable = obj.GetComponent<Fallable>();
			mFallables.Add(obj.GetComponent<Fallable>());
		}
	}
	// This returns a bullet for each time the fire command is used
	public GameObject GetInstance()
	{
		GameObject fallbleToReturn = null;
		if(mNbOfFallable > 0)
		{
			for (int i = 0; i < mNbOfFallable; i++)
			{
				if(!mFallables[i].gameObject.activeInHierarchy)
				{
					fallbleToReturn = mFallables[i].gameObject;
				}
			}
		}
		return fallbleToReturn;
	}

	//// Activates each bullet
	//public void ActivateBullet(Fallable fallableToActivate)
	//{
	//	fallableToActivate.Spawn();
	//}
	//// Resets each bullet to be reused after
	//public void Recycle(Fallable toRecycle)
	//{
	//	toRecycle.Vanish();
	//}
}



