using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

public class gvmWaveBehaviour : NetworkBehaviour {
    
    private int floorMask;
    private bool spellCasted = false;
    private float camRayLength = 100f;
    private Camera GodCamera;
    
    [SerializeField]
    private GameObject firstArea;
    [SerializeField]
    private GameObject secondArea;
    [SerializeField]
    private GameObject spellCollider;
    private gvmGodRessourcesManager resourcesUI;

    private Vector3 firstClickPosition = Vector3.up * -1000;
    private Vector3 secondClickPosition = Vector3.up * -1000;
    private bool clickedOnce;
    private bool clickedTwice;
    private float areaSize = 1f;
    private int tsunamiWaveSpeed = 30;
    
    void Start() {
        if (hasAuthority) {
            GodCamera = GameObject.FindGameObjectWithTag("GodCamera").GetComponent<Camera>();
            floorMask = LayerMask.GetMask("Floor");
        }
        if (isServer) {
            resourcesUI = GameObject.FindGameObjectWithTag("AvatarPoolManager").GetComponent<gvmGodRessourcesManager>();
        }
        spellCollider.GetComponent<gvmSpellCollider>().Init(gameObject.GetComponent<gvmUIDataContainer>());
        gameObject.SetActive(false);
    }

    void OnEnable() {
        clickedOnce = false;
        clickedTwice = false;
        firstArea.SetActive(true);
    }

    void Update() {
        if (hasAuthority && !clickedTwice)
        {
            if (Input.GetMouseButtonDown(1))
            {
                disableSpell();
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (clickedOnce && !clickedTwice)
                {
                    //second click
                    secondClickPosition = setClickPosition();
                    clickedTwice = true;
                    CmdCastSpellTsunami(firstClickPosition, secondClickPosition);
                }
                if (!clickedOnce && !clickedTwice)
                {
                    //first click
                    firstClickPosition = setClickPosition();
                    clickedOnce = true;
                    secondArea.SetActive(true);
                }
            }
            if (clickedOnce && !clickedTwice)
            {
                // waiting for second click
                Ray camRay = GodCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit floorHit;
                if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
                {
                    displayCastedSpellPosition(floorHit.point);
                }
            }
            if (!clickedOnce && !clickedTwice)
            {
                // waiting for first click
                setSpellPosition();
            }
        }
    }

    public void updateSpellPosition() {
        Ray camRay = GodCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 spellPosition = floorHit.point;
            spellPosition.y = 0f;
            transform.position = spellPosition;
        }
    }

    public void setSpellPosition() {
        Ray camRay = GodCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 spellPosition = floorHit.point;
            spellPosition.y = 0f;
            transform.position = spellPosition;
        }
    }
    
    Vector3 setClickPosition() {
        Ray camRay = GodCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            return Vector3.right * floorHit.point.x + Vector3.forward * floorHit.point.z;
        }
        return Vector3.zero;
    }

    //display wave area after first click depending on first click position
    void displayCastedSpellPosition(Vector3 position) {
            var xAxis = (gameObject.transform.position.x - position.x) / -200;
            var yAxis = (gameObject.transform.position.z - position.z) / -200;
            var angle = Math.Acos(xAxis / (float)Math.Sqrt((xAxis * xAxis) + (yAxis * yAxis))) * 180 / Math.PI;
            angle = yAxis > 0 ? -angle : angle;
            if (double.IsNaN(angle) == false) {
            gameObject.transform.localRotation = Quaternion.Euler(0, (float)angle, 0);
            }
        
    }

    [Command]
    void CmdCastSpellTsunami(Vector3 spellAreaBeginning, Vector3 spellAreaEnd) {
        resourcesUI.useRessourcesForCastedSpell(gameObject.name);
        gameObject.transform.position = spellAreaBeginning;
        gameObject.SetActive(true);
        secondArea.SetActive(true);
        spellCollider.SetActive(true);
        displayCastedSpellPosition(spellAreaEnd);
        RpcCastSpellTsunami(spellAreaBeginning, spellAreaEnd);
        StartCoroutine(spellAnimation());
    }

    //display final spell position and area end start trigger animation
    [ClientRpc]
    void RpcCastSpellTsunami(Vector3 spellAreaBeginning, Vector3 spellAreaEnd) {
        displayCastedSpellPosition(spellAreaEnd);
        gameObject.transform.position = spellAreaBeginning;
        gameObject.SetActive(true);
        secondArea.SetActive(true);
        spellCollider.SetActive(true);
        displayCastedSpellPosition(spellAreaEnd);
        StartCoroutine(spellAnimation());
    }

    //translate trigger block along aera effect
    IEnumerator spellAnimation() {
        spellCollider.SetActive(true);
        while (spellCollider.transform.localPosition.x < areaSize) {
            spellCollider.transform.Translate(Vector3.right * tsunamiWaveSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }
        disableSpell();
    }
    
    public void disableSpell() {
        spellCollider.SetActive(false);
        secondArea.SetActive(false);
        gameObject.SetActive(false);
        gameObject.transform.position = Vector3.up * -1000;
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        spellCollider.transform.localPosition = Vector3.up * 2.5f;
    }
}
