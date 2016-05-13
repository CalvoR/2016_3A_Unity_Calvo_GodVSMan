using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gvmSpellEffectGetter : MonoBehaviour {


    [SerializeField]
    private List<int> effectList;
    private int XMLdata;
    private gvmPropertiesManager properties;
    [SerializeField]
    private gvmNPCData data;

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

    public void getNewEffect(List<int> newEffects) {
        for(int i = 0; i < newEffects.Count; i++) {
            effectList.Add(newEffects[i]);
            StartCoroutine(dealDamage(newEffects[i], i));
        }
    }

    private IEnumerator dealDamage(int effect, int effectIndex) {
        var prop = properties.getPropertyById(effect);
        data.HP += prop.damage;
        for (int i = 0; i < prop.duration; i++) {
            yield return new WaitForSeconds(1);
            data.HP += prop.damage;
        }
        effectList.Remove(effect);
    }
}