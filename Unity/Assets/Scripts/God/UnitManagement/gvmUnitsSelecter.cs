using UnityEngine;
using System.Collections;

public class gvmUnitsSelecter : MonoBehaviour {

    public Vector3 position1;
    public Vector3 position2;

    private int floorMask;
    public float camRayLength = 100f;

    public void setSpellPosition() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 spellPosition = floorHit.point;
            spellPosition.y = 0f;
            position1 = spellPosition;
        }
    }

    //activate the spell trigger area : disable click for the spell : call the xml loader to remove spell specific cost
    void OnMouseDown() {
        setSpellPosition();
        Debug.Log(position1);
            //animation
    }
}
