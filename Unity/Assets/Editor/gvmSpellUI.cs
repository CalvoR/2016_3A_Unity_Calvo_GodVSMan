using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Linq;

public class gvmSpellUI : EditorWindow {

    private gvmSpellContainer spellContainer;
    private string[] propertiesNameList;
    private List<gvmSpellData> data;
    private int[] index;
    private List<int>[] propList;
    private Vector2 scroll;
    private string[] fileList;
    private string fileToLoad = "SpellData";

    [MenuItem("GvM/Data Editor/Spell Editor")]
    public static void ShowWindow() {
        GetWindow<gvmSpellUI>(false, "Spell Editor", true);
    }

    void OnEnable() {

        scroll = new Vector2(0, 0);
        spellContainer = gvmSpellContainer.Load(fileToLoad);
        data = spellContainer.spells;
        index = new int[data.Count];
        var behaviourDir = Resources.LoadAll("Prefabs/God/Spells", typeof(GameObject));

        fileList = toArrayOfString(behaviourDir);
        for (int i = 0; i < index.Length; i++) {
            index[i] = getndexOf(data[i].prefab, fileList);
        }

        var properties = gvmPropertiesManager.GetInstance().propertiesContainer;
        propertiesNameList = new string[properties.Count];
        for(int i = 0; i < properties.Count; i++) {
            propertiesNameList[i] = properties[i].name;
        }

        propList = new List<int>[data.Count];
        for (int i = 0; i < propList.Length; i++) {
            propList[i] = data[i].propertiesId;
        }
    }

    void OnGUI() {

        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save")) {
            var path = EditorUtility.SaveFilePanel("Save Spell",
                                                   "Asset/Ressources",
                                                   fileToLoad,
                                                   "xml");
            if (path.Length != 0) {
                spellContainer.Save(data);
            }
        }
        if (GUILayout.Button("Load")) {
            var path = EditorUtility.OpenFilePanel("Load Spell",
                                                   "Asset/Ressources",
                                                   "xml");
            if (path.Length != 0)
            {
                path = path.Split('/').Last().Split('.')[0];
                Debug.Log(path);
                spellContainer = gvmSpellContainer.Load(path);
                data = spellContainer.spells;
                index = new int[data.Count];
                var behaviourDir = Resources.LoadAll("Prefabs/God/Spells", typeof(GameObject));

                fileList = toArrayOfString(behaviourDir);
                for (int i = 0; i < index.Length; i++) {
                    index[i] = getndexOf(data[i].prefab, fileList);
                }

                var properties = gvmPropertiesManager.GetInstance().propertiesContainer;
                propertiesNameList = new string[properties.Count];
                for (int i = 0; i < properties.Count; i++) {
                    propertiesNameList[i] = properties[i].name;
                }

                propList = new List<int>[data.Count];
                for (int i = 0; i < propList.Length; i++) {
                    propList[i] = data[i].propertiesId;
                }
            }
        }
        if (GUILayout.Button("Create New Spell")) {
            data = new List<gvmSpellData>();
            index = new int[data.Count];
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        scroll = EditorGUILayout.BeginScrollView(scroll);
        if (GUILayout.Button("Add Spell")) {
            data.Add(new gvmSpellData());
            index = new int[data.Count];
        }
        EditorGUILayout.BeginVertical();

        if (data != null)
        {
            for (int j = 0; j < data.Count; j++)
            {
                var spell = data[j];
                EditorGUILayout.BeginHorizontal();
                spell.name = EditorGUILayout.TextField(spell.name);
                if (GUILayout.Button("Remove Spell"))
                {
                    data.Remove(spell);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginHorizontal(GUILayout.Width(400));
                EditorGUILayout.LabelField("Cost");
                spell.cost = EditorGUILayout.IntField(spell.cost);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal(GUILayout.Width(400));
                EditorGUILayout.LabelField("Instant Corruption");
                spell.instantCorruption = EditorGUILayout.IntField(spell.instantCorruption);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal(GUILayout.Width(400));
                EditorGUILayout.LabelField("Instant Damage");
                spell.instantDamage = EditorGUILayout.IntField(spell.instantDamage);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal(GUILayout.Width(400));
                EditorGUILayout.LabelField("Area CPS (Corruption)");
                spell.areaCPS = EditorGUILayout.IntField(spell.areaCPS);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal(GUILayout.Width(400));
                EditorGUILayout.LabelField("Area DPS (Damage)");
                spell.areaDPS = EditorGUILayout.IntField(spell.areaDPS);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal(GUILayout.Width(400));
                EditorGUILayout.LabelField("Area Duration");
                spell.areaDuration = EditorGUILayout.IntField(spell.areaDuration);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal(GUILayout.Width(400));
                EditorGUILayout.LabelField("Cast Time");
                spell.castTime = EditorGUILayout.IntField(spell.castTime);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal(GUILayout.Width(400));
                EditorGUILayout.LabelField("Cooldown");
                spell.cooldown = EditorGUILayout.IntField(spell.cooldown);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Properties", GUILayout.Width(100));
                if (GUILayout.Button("+", GUILayout.Width(30)))
                {
                    propList[j].Add(0);
                }
                if (GUILayout.Button("-", GUILayout.Width(30)))
                {
                    propList[j].RemoveAt(0);
                }
                for (int i = 0; i < spell.propertiesId.Count; i++)
                {
                    propList[j][i] = EditorGUILayout.Popup(propList[j][i], propertiesNameList);
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal(GUILayout.Width(400));
                EditorGUILayout.LabelField("prefab");
                index[j] = EditorGUILayout.Popup(index[j], fileList);
                spell.prefab = fileList[index[j]];
                EditorGUILayout.EndHorizontal();

                EditorGUI.indentLevel--;
            }
        } else {
            EditorGUILayout.LabelField("Erreur De Chargement Des Données");
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndHorizontal();
    }


    public string[] toArrayOfString(Object[] info) {
        string[] data = new string[info.Length];

        for(int i = 0; i < info.Length; i++) {
            data[i] = info[i].name;
        }
        return data;
    }

    public int getndexOf(string strToFind, string[] strArray) {
        for (int i = 0; i < strArray.Length; i++) {
            if (strArray[i].Equals(strToFind)) {
                return i;
            }
        }
        return 0;
    }

}
