using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
	public float mFPS;
	public UnityEngine.UI.Text mUIText;
	
	private void Update()
	{
		mFPS = 1.0f / Time.deltaTime;
		//mUIText.text = mFPS.ToString("#.00") + " FPS";
	}
}