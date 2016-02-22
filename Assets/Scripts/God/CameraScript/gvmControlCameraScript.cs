using UnityEngine;
using System.Collections;

public class gvmControlCameraScript : MonoBehaviour {

    private GameObject gameCamera;
    private Vector3 mapPosition;
    private Vector3 mouseClickDownPosition;
    private Vector3 vectorZoom;
    private float xMinLimit = 50f;
    private float xMaxLimit = -50f;
    private float yMinLimit = 15f;
    private float yMaxLimit = 150f;
    private float zMinLimit = 100f;
    private float zMaxLimit = -100f;

    [Range(-20f, -120f)]
    [SerializeField]
    private float cameraAngle = -65f;
    [Range(100, 1)]
    [SerializeField]
    private int cameraSpeed = 1;
    [Range(10, 100)]
    [SerializeField]
    private int cameraZoomSpeed = 50;
    [Range(0.0f, 4f)]
    [SerializeField]
    private float cameraKeyboardSpeed = 1.0f;


    public Vector3 vector3InvertYAndZAxes;

    void Start() {
        gameCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update() {
        //set new camera position controled with mouse
        if (Input.mousePosition.x >= Screen.width - 5) {
            gameCamera.transform.position = checkCameraLocationOnTheMap(gameCamera.transform.position + Vector3.right / cameraSpeed);
        }
        if (Input.mousePosition.x <= 5) {
            gameCamera.transform.position = checkCameraLocationOnTheMap(gameCamera.transform.position - Vector3.right / cameraSpeed);
        }
        if (Input.mousePosition.y >= Screen.height - 5) {
            gameCamera.transform.position = checkCameraLocationOnTheMap(gameCamera.transform.position + Vector3.forward / cameraSpeed);
        }
        if (Input.mousePosition.y <= 5) {
            gameCamera.transform.position = checkCameraLocationOnTheMap(gameCamera.transform.position - Vector3.forward / cameraSpeed);
        }

        gameCamera.transform.rotation = Quaternion.AngleAxis(cameraAngle, Vector3.left);
        //set new camera position controled with keyboard (zqsd)
        gameCamera.transform.position = checkCameraLocationOnTheMap(gameCamera.transform.position + Vector3.right * Input.GetAxis("HorizontalCameraControl") * cameraKeyboardSpeed);
        gameCamera.transform.position = checkCameraLocationOnTheMap(gameCamera.transform.position + Vector3.forward * Input.GetAxis("VerticalCameraControl") * cameraKeyboardSpeed);

        if (gameCamera.transform.position.y > yMinLimit && gameCamera.transform.position.y < yMaxLimit || gameCamera.transform.position.y < yMinLimit && Input.GetAxis("Mouse ScrollWheel") < 0 || gameCamera.transform.position.y > yMaxLimit && Input.GetAxis("Mouse ScrollWheel") > 0) {
            gameCamera.transform.position += Vector3.down * Input.GetAxis("Mouse ScrollWheel") * cameraZoomSpeed * Mathf.Cos(90+cameraAngle);
            gameCamera.transform.position += Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * cameraZoomSpeed * Mathf.Sin(90+cameraAngle);
        }
    }

    //check limits for the camera position
    Vector3 checkCameraLocationOnTheMap(Vector3  vectorTmp) {
        if ( vectorTmp.z >= zMinLimit) {
             vectorTmp.z = zMinLimit;
        }
        if ( vectorTmp.z <= zMaxLimit) {
             vectorTmp.z = zMaxLimit;
        }
        if ( vectorTmp.x >= xMinLimit) {
             vectorTmp.x = xMinLimit;
        }
        if ( vectorTmp.x <= xMaxLimit) {
             vectorTmp.x = xMaxLimit;
        }
        return vectorTmp;
    }
}

