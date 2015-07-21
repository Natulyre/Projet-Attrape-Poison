using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool : MonoBehaviour 
{
	public int mNbOfFallable;
	public Fallable mFallable;
	
	private List<Fallable> mFallables;

	private void Start() 
	{		
		Init();
	}
	
	// Initialisation of all of the fallables in the pool
	void Init()
	{
		mFallables = new List<Fallable>();
		
		for (int i = 0; i < mNbOfFallable; i++)
		{
			GameObject obj = Instantiate(mFallable.gameObject);
			mFallables.Add(obj.GetComponent<Fallable>());
		}
	}

	// This returns a bullet for each time the fire command is used
	public GameObject GetInstance()
	{
		GameObject fallableToReturn = null;
		if(mNbOfFallable > 0)
		{
			for (int i = 0; i < mNbOfFallable; i++)
			{
				if(!mFallables[i].gameObject.activeInHierarchy)
				{
					fallableToReturn = mFallables[i].gameObject;
				}
			}
		}
		return fallableToReturn;
	}
}