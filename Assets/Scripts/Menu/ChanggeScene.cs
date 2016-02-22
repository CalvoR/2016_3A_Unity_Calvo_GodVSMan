using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChanggeScene : MonoBehaviour
{

    public void Exit()
    {
        Application.Quit();
    }

    public void Change_Scene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
