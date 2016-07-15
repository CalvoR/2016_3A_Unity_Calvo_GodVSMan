using UnityEngine;
using System.Collections;

public class gvmAnimations : MonoBehaviour
{
    public Animation animation1;

    public bool Equiped = false;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "npc")
        {
            collision.gameObject.GetComponent<gvmNPCData>().takeDamage(10);
        }
    }
}
