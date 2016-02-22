using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gvmDisplay : MonoBehaviour {

    [SerializeField]
    public Text heroStatsDisplay;

    void Start () {
        UpdateStatsDisplay();

    }

    public void UpdateStatsDisplay()
    {
        if (heroStatsDisplay != null)
            heroStatsDisplay.text = "Player\n Attaque:" + HeroStats.Attack;
    }
}
