using UnityEngine;
using System.Collections;

public class gvmUnit : MonoBehaviour {
    
    void OnMouseDown() {
        Debug.Log("click"+this.GetInstanceID());
    }
}
