using UnityEngine;
using System.Collections;

public class gvmPitchMainCamera : MonoBehaviour {


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
        yVariation += Input.GetAxisRaw("Mouse Y") * Time.deltaTime * pitchSpeed;
        yVariation = Mathf.Clamp(yVariation, -clampAngle, clampAngle);
    }

    void FixedUpdate()  
    {
        cameraTransform.rotation = Quaternion.Euler(-yVariation, cameraTransform.rotation.eulerAngles.y, 0.0f);
    }
}
