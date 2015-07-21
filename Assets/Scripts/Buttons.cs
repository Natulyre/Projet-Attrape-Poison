using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {

	GameFlow mGameFlow;

	void Start()
	{
		Init();
	}

	void Init()
	{
		mGameFlow = GameObject.Find ("GameFlow").GetComponent<GameFlow>();
	}

	public void RestartLevel()
	{
		mGameFlow.RestartLevel ();
	}

	public void NextLevel()
	{
		mGameFlow.NextLevel ();
	}
}