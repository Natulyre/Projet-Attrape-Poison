using UnityEngine;
using System.Collections;


//Camera Script, really..the only thing its does it fix the x, and make the y follow the player.
//There is a damp time to mkae "Elasticity"

public class CameraController : MonoBehaviour
{
    // Public variables
    public Transform mPlayer;
    public float mDampTime;

    // Private variables
    private Camera mCamera;
    private Vector3 mVelocity = Vector3.zero;

    void Start()
    {
        mCamera = Camera.main;
    }

    void Update()
    {
        // If player isn't null, Set the camera with a delay set by the damp time to the y of the player 
        if (mPlayer)
        {
            Vector3 point = mCamera.WorldToViewportPoint(mPlayer.position);
            Vector3 delta = mPlayer.position - mCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(0, destination.y, destination.z), ref mVelocity, mDampTime);
        }
    }
}

