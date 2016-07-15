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
        xVariation = HeadTransform.rotation.eulerAngles.y;
    }
    
    void FixedUpdate() {
        if (isLocalPlayer) {
            yVariation += Input.GetAxisRaw("Mouse Y") * Time.deltaTime * pitchSpeed;
            yVariation = Mathf.Clamp(yVariation, -CLAMP_ANGLE, CLAMP_ANGLE);

            xVariation += Input.GetAxisRaw("Mouse X") * Time.deltaTime * yawSpeed;
            CmdRotateCamera(xVariation, yVariation);

            CharacterTransform.rotation = Quaternion.Euler(0.0f, xVariation, 0.0f);
            HeadTransform.rotation = Quaternion.Euler(-yVariation, xVariation, 0.0f);
        }
    }

    [Command]
    void CmdRotateCamera(float x, float y) {
        CharacterTransform.rotation = Quaternion.Euler(0.0f, x, 0.0f);
        HeadTransform.rotation = Quaternion.Euler(-y, x, 0.0f);
    }

    #endregion

}
