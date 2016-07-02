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

    [SerializeField]
    private MeshRenderer mesh;

    [SerializeField]
    private gvmGodRessourcesManager resources;

    void Awake() {
        properties = gvmPropertiesManager.GetInstance();
        effectList = new List<int>();
        resources.NPCCounter++;
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
        for(int i = 0; i < Container.propertiesId.Count; i++) {
            effectList.Add(Container.propertiesId[i]);
            int v1 = 0;
            int v2 = 0;
            if (data.state >= 0) {
                if (data.state + Container.stateEffect >= 0)
                {
                    v2 = Container.stateEffect; 
                } else {
                    v2 = -data.state; 
                    v1 = -(data.state + Container.stateEffect); 
                }
            }
            if (data.state <= 0) {
                if (data.state + Container.stateEffect <= 0) {
                    v1 = -Container.stateEffect;
                } else {
                    v1 = data.state;
                    v2 = data.state + Container.stateEffect;
                }
            }

            resources.setResourcesPerSeconds(v1, v2);
            
            StartCoroutine(dealDamage(Container.propertiesId[i], i));
        }
        if (data.state == 0) {
            if (data.state + Container.stateEffect > 0) {
                resources.FaithfulNPCCounter++;
            }
            if (data.state + Container.stateEffect < 0) {
                resources.FearfulNPCCounter++;
            }
        }
        if (data.state < 0) {
            if (data.state + Container.stateEffect > 0) {
                resources.FaithfulNPCCounter++;
                resources.FearfulNPCCounter--;
            }
            if (data.state + Container.stateEffect == 0) {
                resources.FearfulNPCCounter--;
            }
        }
        if (data.state > 0) {
            if (data.state + Container.stateEffect < 0) {
                resources.FaithfulNPCCounter--;
                resources.FearfulNPCCounter++;
            }
            if (data.state + Container.stateEffect == 0) {
                resources.FaithfulNPCCounter--;
            }
        }

        data.state += Container.stateEffect;
        mesh.material.color = data.state == 0 ? Color.yellow : data.state < 0 ? Color.red : Color.green;
    }

    private IEnumerator dealDamage(int effect, int effectIndex) {
        var prop = properties.getPropertyById(effect);
        data.HP += prop.damage;
        for (int i = 0; i < prop.duration; i++) {
            yield return new WaitForSeconds(1);
            data.HP += prop.damage;
            if (data.HP < 0) {
                gameObject.SetActive(false);
            }
        }
        effectList.Remove(effect);
    }
}