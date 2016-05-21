using UnityEngine;
using System.Collections;

public class gvmUnitsSelecter : MonoBehaviour {

    public Vector3[] position;

    private int floorMask;
    public float camRayLength = 300f;

    void Awake() {
        position = new Vector3[2];
    }

    public void setSpellPosition(int posIndex) {

        floorMask = LayerMask.GetMask("Floor");
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 spellPosition = floorHit.point;
            spellPosition.y = 0f;
            position[posIndex] = spellPosition;
        }
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            setSpellPosition(0);
            Debug.Log("fear: " + position[0]);
        }
        if (Input.GetMouseButtonUp(0)) {
            setSpellPosition(1);
            Debug.Log("fear: " + position[1]);
        }
        if (Input.GetMouseButtonDown(1)) {
            setSpellPosition(0);
            Debug.Log("faith: " + position[1]);
        }
        if (Input.GetMouseButtonUp(1)) {
            Debug.Log("faith: " + position[1]);
            setSpellPosition(1);
        }
    }
}
