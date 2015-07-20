using UnityEngine;
using System.Collections;
/// <summary>
/// 
/// Camera Sript, really..the only thing its does it fix the x, and make the y follow the player.
/// There is a damp time to mkae "Elasticity"
/// 
/// </summary>
public class CameraController : MonoBehaviour
{
    // Public variables
    public Transform mPlayer;
    public float mDampTime;

    // Private variables
    private Camera mCamera;
    private Vector3 mVelocity = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        mCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // If player non null, Set the camera with a delay set by the damp time to the y of the player 
        if (mPlayer)
        {
            Vector3 point = mCamera.WorldToViewportPoint(mPlayer.position);
            Vector3 delta = mPlayer.position - mCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(0, destination.y, destination.z), ref mVelocity, mDampTime);
        }
    }
}

