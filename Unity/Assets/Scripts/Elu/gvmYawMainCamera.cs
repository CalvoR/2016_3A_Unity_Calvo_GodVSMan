using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gvmYawMainCamera : NetworkBehaviour {


    [SerializeField]
    float yawSpeed;
    [SerializeField]
    float pitchSpeed;
    
    private float xVariation;
    private float yVariation;
    private float clampAngle = 75.0f;
    
    [SerializeField]
    float speed;

    [SerializeField]
    GameObject cameraTransform;
    [SerializeField]
    GameObject playerCharacter;
    
    // Update is called once per frame
    void FixedUpdate() {
        if (isLocalPlayer) {
            xVariation = Input.GetAxisRaw("Mouse X") * Time.deltaTime * yawSpeed;

            yVariation += Input.GetAxisRaw("Mouse Y") * Time.deltaTime * pitchSpeed;
            yVariation = Mathf.Clamp(yVariation, -clampAngle, clampAngle);

            CmdMoveHead(yVariation);
            CmdMoveBody(xVariation);

            CmdStraf(Input.GetAxis("Sideway"));
            CmdMoveForward(Input.GetAxis("Forward"));

            playerCharacter.transform.Translate(Vector3.right * speed * Input.GetAxis("Forward"));

            playerCharacter.transform.Translate(Vector3.forward * speed * Input.GetAxis("Sideway"));
            playerCharacter.transform.position =
                Vector3.right * playerCharacter.transform.position.x +
                Vector3.up +
                Vector3.forward * playerCharacter.transform.position.z;
        }
    }
    
    [Command]
    public void CmdMoveHead( float y) {
        cameraTransform.transform.Rotate(Vector3.right, y);
        cameraTransform.transform.rotation = Quaternion.Euler(-y, cameraTransform.transform.rotation.eulerAngles.y, 0.0f);
    }

    [Command]
    public void CmdMoveBody(float x) {
        playerCharacter.transform.Rotate(Vector3.up * x);
    }

    [Command]
    public void CmdStraf(float strafSpeed) {
        playerCharacter.transform.Translate(Vector3.right * speed * strafSpeed);
    }

    [Command]
    public void CmdMoveForward(float strafSpeed) {
        playerCharacter.transform.Translate(Vector3.forward * speed * strafSpeed);
        playerCharacter.transform.position =
            Vector3.right * playerCharacter.transform.position.x +
            Vector3.up +
            Vector3.forward * playerCharacter.transform.position.z;
    }
}
