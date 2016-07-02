using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class XavierChosenMovementTest : NetworkBehaviour {

    [SerializeField]
    GameObject playerCamera;

    [SerializeField]
    float speed;

    [SerializeField]
    GameObject playerCharacter;

    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();
        if (Camera.main && Camera.main.gameObject) {
            Camera.main.gameObject.SetActive(false);
        }
        playerCamera.SetActive(true);
    }

    public void FixedUpdate() {
        if (isLocalPlayer) {
            CmdStraf(Input.GetAxis("Sideway"));
            CmdMoveForward(Input.GetAxis("Forward"));
            //GoToPositionFromClick(mousePosition);
        }
    }

    [Command]
    public void CmdStraf(float strafSpeed) {
        playerCharacter.transform.Translate(Vector3.right * speed * strafSpeed);
    }

    [Command]
    public void CmdMoveForward(float strafSpeed) {
        playerCharacter.transform.Translate(Vector3.forward * speed * strafSpeed);
    }
}