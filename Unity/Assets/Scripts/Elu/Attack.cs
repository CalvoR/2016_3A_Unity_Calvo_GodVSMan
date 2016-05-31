using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    [SerializeField]
    Animation attack;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(1))
        {
            attack.Play();
        }
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "npc")
        {
            Destroy(collision.gameObject);
        }
    }
}
