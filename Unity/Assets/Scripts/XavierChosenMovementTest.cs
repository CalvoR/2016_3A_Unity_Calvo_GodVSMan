using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class XavierChosenMovementTest : NetworkBehaviour {

    [SerializeField]
    Camera playerCamera;

    [SerializeField]
    float speed;

    [SerializeField]
    GameObject playerCharacter;

    [SerializeField]
    Collider terrainCollider;

    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();
        if (Camera.main && Camera.main.gameObject) {
            Camera.main.gameObject.SetActive(false);
        }
        playerCamera.gameObject.SetActive(true);
    }

    public void FixedUpdate() {
        if (isLocalPlayer) {
            CmdStraf(Input.GetAxis("Sideway"));
            //GoToPositionFromClick(mousePosition);
        }
    }

    [Command]
    public void CmdStraf(float strafSpeed) {
        playerCharacter.transform.position += Vector3.right * speed * strafSpeed;
    }
    /*
    public void GoToPositionFromClick(Vector3 mousePosition) {
        var ray = playerCamera.ScreenPointToRay(mousePosition);

        RaycastHit hit;

        if (terrainCollider.Raycast(ray, out hit, 2000f)) {
            CmdGoToPosition(hit.point);
        }
    }

    [Command]
    public void CmdGoToPosition(Vector3 pos) {
        // For Client Action Replication Prediction
        RpcGoToPosition(pos);

        // If the server is a host do not apply twice the action due to prediction
        if (!Network.isClient) {
            agent.SetDestination(pos);
        }
    }

    // For Client Action Replication Prediction
    [ClientRpc]
    public void RpcGoToPosition(Vector3 pos) {
        agent.SetDestination(pos);
    }*/
}