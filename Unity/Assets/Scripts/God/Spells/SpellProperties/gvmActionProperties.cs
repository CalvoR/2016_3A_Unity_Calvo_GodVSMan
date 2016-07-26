using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class gvmActionProperties : MonoBehaviour {

    public List<Delegate> propertiesActionContainer;
    public enum propertiesName { damage, damageOverTime, healOverTime, heal };
    public gvmActionProperties propertiesCompatibility;
    private static gvmActionProperties _instance;
    
    private gvmActionProperties() {
        propertiesActionContainer = new List<Delegate>();

        propertiesActionContainer.Add(new Func<int, int, int, IEnumerator>(damageOverTime));
        propertiesActionContainer.Add(new Func<int, int, int, IEnumerator>(directDamage));
        // propertiesContainer.Add(new Func<int, int, int, IEnumerator>(heal);

        propertiesCompatibility = new gvmActionProperties();

        // var res = spellPropertiesContainer[1].DynamicInvoke(1, 2);
    }

    public static gvmActionProperties GetInstance() {
        if (_instance == null) {
            _instance = new gvmActionProperties();
        }
        return _instance;
    }

    private IEnumerator directDamage(int HPPool, int damage, int duration) {
        if (duration > 0) {
            yield return new WaitForSeconds(1f);
            duration--;
            HPPool += damage;
            yield return new int[] { HPPool };
        }
    }

    private IEnumerator damageOverTime(int HPPool, int damage, int duration) {
        if (duration > 0) {
            yield return new WaitForSeconds(1f);
            duration--;
            HPPool += damage;
            yield return new int[] { HPPool };
        }
    }
}