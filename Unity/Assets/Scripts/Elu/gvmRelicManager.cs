using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gvmRelicManager : MonoBehaviour {

    const uint NB_RELICS = 2;

    static uint nbFoundRelics = 0;

    public static bool hasToUpdate = false;

    [SerializeField]
    int relicID;

    [SerializeField]
    Image relicImage;


    void Awake()
    {
        relicImage.enabled = false;
    }

    void Update()
    {
        if (!hasToUpdate || relicID < nbFoundRelics + 1)
            return;
        
        nbFoundRelics++;
        hasToUpdate = false;

        if (relicID == nbFoundRelics)
        {
            relicImage.enabled = true;
            relicImage.sprite = Resources.Load<Sprite>("Picture/Relic");            
        }

        if (nbFoundRelics >= NB_RELICS)
        {
            ActiveElectVictory();
        }
    }

    void ActiveElectVictory()
    {
        Debug.LogWarning("VICTOOOOORY !!!");
    }
}
