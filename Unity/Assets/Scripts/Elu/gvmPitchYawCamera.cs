using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gvmPitchYawCamera : NetworkBehaviour {

    #region Attributs

    [SerializeField]
    Transform CharacterTransform;

    [SerializeField]
    Transform HeadTransform;

    [SerializeField]
    float pitchSpeed;

    [SerializeField]
    float yawSpeed;

    private float xVariation;
    private float yVariation;
    private const float CLAMP_ANGLE = 75.0f;

    #endregion

    #region Méthodes

    void Start()
    {
        yVariation = HeadTransform.rotation.eulerAngles.x;
        xVariation = 0.0f;
    }
    

	void Update ()
    {
        yVariation += Input.GetAxisRaw("Mouse Y") * Time.deltaTime * pitchSpeed;
        yVariation = Mathf.Clamp(yVariation, -CLAMP_ANGLE, CLAMP_ANGLE);

        xVariation = Input.GetAxisRaw("Mouse X");
    }


    void FixedUpdate()  
    {
        CmdRotateCamera();   
    }

    [Command]
    void CmdRotateCamera()
    {
        HeadTransform.rotation = Quaternion.Euler(-yVariation, HeadTransform.rotation.eulerAngles.y, 0.0f);
        CharacterTransform.Rotate(Vector3.up * xVariation * Time.deltaTime * yawSpeed);
    }

    #endregion

}
