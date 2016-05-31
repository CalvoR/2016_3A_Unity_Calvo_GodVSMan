using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gvmPitchMainCamera : NetworkBehaviour {


    [SerializeField]
    Transform cameraTransform;

    [SerializeField]
    float pitchSpeed;
    
    private float yVariation = 0.0f;
    private float clampAngle = 75.0f;

    void Start()
    {
        yVariation = cameraTransform.rotation.eulerAngles.x;  
    }
    

	void Update ()
    {
        if (isLocalPlayer) {
            yVariation += Input.GetAxisRaw("Mouse Y") * Time.deltaTime * pitchSpeed;
            yVariation = Mathf.Clamp(yVariation, -clampAngle, clampAngle);
        }
    }

    void FixedUpdate()  
    {
        if (isLocalPlayer) {
            cameraTransform.rotation = Quaternion.Euler(-yVariation, cameraTransform.rotation.eulerAngles.y, 0.0f);
        }
    }
}
