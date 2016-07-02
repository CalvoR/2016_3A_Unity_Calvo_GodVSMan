using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gvmYawMainCamera : NetworkBehaviour {

    [SerializeField]
    Transform cameraTransform;

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
    GameObject playerCharacter;
    
    // Update is called once per frame
    void FixedUpdate() {
        if (isLocalPlayer) {
            xVariation = Input.GetAxisRaw("Mouse X") * Time.deltaTime * yawSpeed;

            yVariation += Input.GetAxisRaw("Mouse Y") * Time.deltaTime * pitchSpeed;
            yVariation = Mathf.Clamp(yVariation, -clampAngle, clampAngle);

            CmdMoveHead(xVariation, yVariation);

            CmdStraf(Input.GetAxis("Sideway"));
            CmdMoveForward(Input.GetAxis("Forward"));
        }
    }
    
    [Command]
    public void CmdMoveHead(float x, float y) {
        playerCharacter.transform.Rotate(Vector3.up * x);
        cameraTransform.rotation = Quaternion.Euler(-y, cameraTransform.rotation.eulerAngles.y, 0.0f);
    }

    [Command]
    public void CmdStraf(float strafSpeed) {
        playerCharacter.transform.position += Vector3.right * speed * strafSpeed;
    }

    [Command]
    public void CmdMoveForward(float strafSpeed) {
        playerCharacter.transform.position += Vector3.forward * speed * strafSpeed;
    }



}
