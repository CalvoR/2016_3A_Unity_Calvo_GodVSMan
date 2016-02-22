using UnityEngine;
using System.Collections;

public class gvmYawMainCamera : MonoBehaviour {

    [SerializeField]
    Transform cameraTransform;

    [SerializeField]
    float yawSpeed;

    private float xVariation;

	
	// Update is called once per frame
	void Update () {

        xVariation = Input.GetAxisRaw("Horizontal");
    }


    void FixedUpdate()  {
        cameraTransform.Rotate(Vector3.up * xVariation * Time.deltaTime * yawSpeed);
    }
}
