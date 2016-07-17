using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GvmGameControler : NetworkBehaviour {

    private int NpcCount = 0;
    private int DeadNpcCount = 0;
    private int CorruptedNpcCount = 0;
    private int TransformedNpcCount = 0;
    [SerializeField]
    private int corrupted;
    [SerializeField]
    private gvmGodSceneManager GodManager;
    
    [SerializeField]
    private gvmPlayerControler ChosenManager;
    [SerializeField]
    private Transform RelicContainer;
    private int ChosenRelicCount;

    [SerializeField]
    private gvmZombiesManager NPCManager;
    [SerializeField]
    private gvmServerUI serverUI;
    [SerializeField]
    private gvmGameMenu gameMenu;
    public GameObject NoAuthorityScripts;

    public void addRelicCounter() {
        ChosenRelicCount++;
        serverUI.updateChosenObjectives(RelicContainer.childCount, ChosenRelicCount);
        if (ChosenRelicCount >= RelicContainer.childCount) {
            gameMenu.PauseUnpauseTheGame();
            GodManager.RpcEndTheGame(false);
            ChosenManager.RpcEndTheGame(true);
        }
    }

    public void setNPCCounter(int count) {
        NpcCount = count;
        serverUI.updateGodObjectives(NpcCount, CorruptedNpcCount, TransformedNpcCount, DeadNpcCount);
        serverUI.updateChosenObjectives(RelicContainer.childCount, 0);
    }

    public void addNPCHasCorrupted() {
        CorruptedNpcCount++;
        CheckGODPlayerWin();
        serverUI.updateGodObjectives(NpcCount, CorruptedNpcCount, TransformedNpcCount, DeadNpcCount);
    }

    public void addNPCHasTransformed() {
        TransformedNpcCount++;
        CheckGODPlayerWin();
        serverUI.updateGodObjectives(NpcCount, CorruptedNpcCount, TransformedNpcCount, DeadNpcCount);
    }

    public void addNPCHasDead() {
        DeadNpcCount++;
        CheckGODPlayerWin();
        serverUI.updateGodObjectives(NpcCount, CorruptedNpcCount, TransformedNpcCount, DeadNpcCount);
    }

    private void CheckGODPlayerWin() {
        if (DeadNpcCount >= NpcCount * 0.75 || CorruptedNpcCount == NpcCount) {
            gameMenu.PauseUnpauseTheGame();
            GodManager.RpcEndTheGame(true);
            ChosenManager.RpcEndTheGame(false);
        }
        RpcUpdateGodObjectives();
    }

    private void RpcUpdateGodObjectives() {
    }
}
