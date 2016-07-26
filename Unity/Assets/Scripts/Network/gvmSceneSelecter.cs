using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gvmSceneSelecter : MonoBehaviour {

    public void LoadScene(bool godScene) {
        if (godScene) {
            SceneManager.LoadScene("God");
        } else {
            SceneManager.LoadScene("Elu");
        }
    }
}
