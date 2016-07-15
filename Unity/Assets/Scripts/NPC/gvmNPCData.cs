using UnityEngine;
using System.Collections;

public class gvmNPCData : MonoBehaviour {
    // currently useless
    public float HP = 100;
    public int state = 0;

    public void takeDamage(int Damage)
    {
        HP -= Damage;
    }
}
