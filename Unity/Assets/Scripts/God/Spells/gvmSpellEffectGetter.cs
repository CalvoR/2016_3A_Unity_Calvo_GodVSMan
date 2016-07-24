using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class gvmSpellEffectGetter : NetworkBehaviour {


    [SerializeField]
    private int XMLdata;
    private gvmPropertiesManager properties;
    [SerializeField]
    private gvmNPCData data;
    [SerializeField]
    private gvmGodRessourcesManager resources;
    private int DPS;
    private int CPS;
    private WaitForSeconds waitOneSecond = new WaitForSeconds(1);
    private bool AOEDamage;


    void Awake() {
        properties = gvmPropertiesManager.GetInstance();
        AOEDamage = false;
    }

    public void getNewEffect(gvmUIDataContainer Container) {
        if (isServer) {
            data.CorruptionState -= Container.instantCorruption;
            data.HP -= Container.instantDamage;

            var prop = properties.getPropertyById(Container.propertiesId[0]);
            if (prop.dot > 0 || prop.cot > 0) {
                StartCoroutine(propertyDamages(prop.cot, prop.dot, prop.duration));
            }
            DPS += Container.areaDPS;
            CPS += Container.areaCPS;
            if (!AOEDamage && (CPS > 0 || DPS > 0)) {
                AOEDamage = true;
                StartCoroutine(areaDamages());
            }
        }
    }
    
    private IEnumerator areaDamages() {
        while (data.UpdateState(DPS, CPS)) {
            yield return waitOneSecond;
        }
        AOEDamage = false;
    }

    private IEnumerator propertyDamages(int cot, int dot, int duration) {
        for (int i = 0; i < duration; i++) {
            yield return waitOneSecond;
            if (!data.UpdateState(dot, cot)) {
                i = duration;
                data.changeIntoAZombie();
            }
        }
    }
    
    public void removeAreaEffect(gvmUIDataContainer Container) {
        DPS -= Container.areaDPS;
        CPS -= Container.areaCPS;
    }
}