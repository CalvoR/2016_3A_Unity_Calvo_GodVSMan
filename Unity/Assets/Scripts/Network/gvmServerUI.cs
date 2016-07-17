using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class gvmServerUI : NetworkBehaviour
{
    [SerializeField]
    private Text corrupted;

    [SerializeField]
    private Text transformed;

    [SerializeField]
    private Text dead;
    
    [SerializeField]
    private Text relic;

    public void updateGodObjectives(int npcCount, int corruptedNpcCount, int transformedNpcCount, int deadNpcCount) {
        corrupted.text = "PNJ corrompus: "+corruptedNpcCount + "/" + npcCount;
        transformed.text = "PNJ transformés: "+transformedNpcCount + "/" + npcCount;
        dead.text = "PNJ mort: "+deadNpcCount + "/" + npcCount;
    }

    public void updateChosenObjectives(int childCount, int chosenRelicCount) {
        relic.text = "Reliques Trouvées: " + chosenRelicCount + "/" + childCount;
    }
}
