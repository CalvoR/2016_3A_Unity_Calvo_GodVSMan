using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class gvmSpellEffectGetter : NetworkBehaviour {


    [SerializeField]
    private List<int> effectList;
    private int XMLdata;
    private gvmPropertiesManager properties;
    [SerializeField]
    private gvmNPCData data;
    [SerializeField]
    private gvmGodRessourcesManager resources;

    void Awake() {
        properties = gvmPropertiesManager.GetInstance();
        effectList = new List<int>();
    }

    void update() {
        if (effectList.Count >= 0) {
            List<gvmSpellProperty> Data = gvmPropertiesManager.GetInstance().propertiesContainer; //get spell data when affected by
            for (int i = 0; i < Data.Count; i++) {
                Debug.Log(Data[i]); //display affectedBy (spell) data from xml file 
            }
        }
    }

    public void getNewEffect(gvmUIDataContainer Container) {
        for (int i = 0; i < Container.propertiesId.Count; i++) {
            if (!effectList.Contains(Container.propertiesId[i])) {
                effectList.Add(Container.propertiesId[i]);
            }
            if (isServer) {
                StartCoroutine(dealDamage(Container.propertiesId[i]));
            }
        }
        data.CorruptionState += Container.stateEffect;
    }

    private IEnumerator dealDamage(int effect) {
        var prop = properties.getPropertyById(effect);
        data.HP += prop.damage;
        data.CorruptionState += prop.stateEffect;
        for (int i = 0; i < prop.duration; i++) {
            yield return new WaitForSeconds(1);
            if (!data.UpdateState(data.HP + prop.damage, data.CorruptionState + prop.stateEffect)) {
                i = prop.duration;
                data.changeIntoAZombie();
            }
        }
        //effectList.Remove(effect);
    }
}