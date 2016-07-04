using UnityEngine;
using System.Collections;

public class gvmYawMainCamera : MonoBehaviour {
    /*
    [SerializeField]
    Transform cameraTransform;

    [SerializeField]
    float yawSpeed;
    
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
=======
    private float xVariation;
>>>>>>> f5d21a53a2984869bfea72f079d73b851e937169

	
	// Update is called once per frame
	void Update () 
    {
            xVariation = Input.GetAxisRaw("Mouse X");
        
    }


    void FixedUpdate()  
    {
            cameraTransform.Rotate(Vector3.up * xVariation * Time.deltaTime * yawSpeed);
        
    }*/
}
