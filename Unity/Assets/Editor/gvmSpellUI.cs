using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class gvmSpellUI : EditorWindow {

    private gvmSpellContainer spellContainer;
    private List<gvmSpellData> data;
    private Vector2 scroll;

    [MenuItem("GvM/Data Editor/Spell Editor")]
    public static void ShowWindow() {
        GetWindow<gvmSpellUI>(false, "Spell Editor", true);
    }

    void OnEnable() {
        scroll = new Vector2(0, 0);
        spellContainer = gvmSpellContainer.GetInstance();

        data = spellContainer.spells;
    }

    void OnGUI() {

        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save")) {
            var path = EditorUtility.SaveFilePanel("Save Spell",
                                                   "Asset/Ressources",
                                                   "SpellData",
                                                   "xml");
            if (path.Length != 0) {
                spellContainer.Save(data);
            }
        }
        if (GUILayout.Button("Load")) {
            var path = EditorUtility.OpenFilePanel("Load Spell",
                                                   "Asset/Ressources",
                                                   "xml");
            if (path.Length != 0) {
                spellContainer.Load();
                spellContainer = gvmSpellContainer.GetInstance();
                data = spellContainer.spells;
            }
        }
        if (GUILayout.Button("Create New Spell")) {
            data = new List<gvmSpellData>();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        scroll = EditorGUILayout.BeginScrollView(scroll);
        EditorGUILayout.BeginVertical();
        foreach(gvmSpellData spell in data) {
            spell.name = EditorGUILayout.TextField(spell.name);
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginHorizontal(GUILayout.Width(300));
            EditorGUILayout.LabelField("Fear Cost");
            spell.fearCost = EditorGUILayout.IntField(spell.fearCost);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal(GUILayout.Width(300));
            EditorGUILayout.LabelField("Faith Cost");
            spell.faithCost = EditorGUILayout.IntField(spell.faithCost);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal(GUILayout.Width(300));
            EditorGUILayout.LabelField("State Effect");
            spell.stateEffect = EditorGUILayout.IntField(spell.stateEffect);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Properties Id", GUILayout.Width(100));
            if (GUILayout.Button("+", GUILayout.Width(30))) {
                spell.propertiesId.Add(0);
            }
            if (GUILayout.Button("-", GUILayout.Width(30))) {
                spell.propertiesId.RemoveAt(0);
            }
            for (int i = 0; i < spell.propertiesId.Count; i++) {
                spell.propertiesId[i] = EditorGUILayout.IntField(spell.propertiesId[i], GUILayout.Width(30));
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal(GUILayout.Width(300));
            EditorGUILayout.LabelField("Behaviour Name");
            spell.behaviour = EditorGUILayout.TextField(spell.behaviour);
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndHorizontal();
    }


}
