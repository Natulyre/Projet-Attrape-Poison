using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndingScreen : MonoBehaviour {

	private Text mText;
	private GameFlow mGameFlow;

	// Use this for initialization
	void Start () 
	{
		mGameFlow = GameObject.Find ("GameFlow").GetComponent<GameFlow>();
		mText = GetComponent<Text>();
		mText.text = "Collectables: " + mGameFlow.GetCollectableCount().ToString() + "/3";
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
}
