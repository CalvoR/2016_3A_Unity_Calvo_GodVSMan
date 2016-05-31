using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gvmYawMainCamera : NetworkBehaviour {

    [SerializeField]
    Transform cameraTransform;

    [SerializeField]
    float yawSpeed;

    private float xVariation;

	
	// Update is called once per frame
	void Update () 
    {
        if (isLocalPlayer) {
            xVariation = Input.GetAxisRaw("Mouse X");
        }
    }


    void FixedUpdate()  
    {
        if (isLocalPlayer) {
            cameraTransform.Rotate(Vector3.up * xVariation * Time.deltaTime * yawSpeed);
        }
    }
}
