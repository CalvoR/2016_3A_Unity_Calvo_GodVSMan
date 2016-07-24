using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEditor;

public class gvmPropertiesUI : EditorWindow {

    private gvmPropertiesManager properties;
    private List<gvmSpellProperty> data;

    [MenuItem("GvM/Data Editor/Properties Editor")]
    public static void ShowWindow() {
        GetWindow<gvmPropertiesUI>(false, "Properties Editor", true);
    }
    
    void OnEnable() {
        properties = gvmPropertiesManager.GetInstance();        
        data = properties.propertiesContainer;
    }

    void OnGUI() {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save")) {
            var path = EditorUtility.SaveFilePanel("Save Properties",
                                                   "Asset/Ressources",
                                                   "PropertiesCompatibility",
                                                   "xml");
            if (path.Length != 0) {
                Debug.Log(data[0].ToString());
                properties.Save(data);
            }
        }
        if (GUILayout.Button("Load")) {
            var path = EditorUtility.OpenFilePanel("Load Properties",
                                                   "Asset/Ressources",
                                                   "xml");
            if (path.Length != 0) {
                properties.Load();
                properties = gvmPropertiesManager.GetInstance();
                data = properties.propertiesContainer;
            }
        }
        if (GUILayout.Button("Create New Properties")) {
            data = new List<gvmSpellProperty>();
        }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Add Property")) {
            int length = 0;
            if (data.Count > 0) {
                length = data[0].compatibilities.Count;
            }
            data.Add(new gvmSpellProperty(length));
            for (int j = 0; j < data.Count; j++) {
                data[j].compatibilities.Add(0);
            }
        }
        EditorGUILayout.BeginHorizontal();
        if (data != null)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Name", GUILayout.Width(60));
            EditorGUILayout.LabelField("Duration", GUILayout.Width(60));
            EditorGUILayout.LabelField("Corruption/seconds", GUILayout.Width(60));
            EditorGUILayout.LabelField("Total Corruption", GUILayout.Width(60));
            EditorGUILayout.LabelField("Damage/seconds", GUILayout.Width(65));
            EditorGUILayout.LabelField("Total Damage", GUILayout.Width(60));
            /*
        EditorGUILayout.LabelField("Name", GUILayout.Width(60));
        EditorGUILayout.LabelField("Duration", GUILayout.Width(60));
        EditorGUILayout.LabelField("Cast Time", GUILayout.Width(60));
        EditorGUILayout.LabelField("Cooldown", GUILayout.Width(60));
        EditorGUILayout.LabelField("Instant Damage", GUILayout.Width(60));
        EditorGUILayout.LabelField("Instant Corruption", GUILayout.Width(60));
        EditorGUILayout.LabelField("Spell Area Damage Per Seconds", GUILayout.Width(60));
        EditorGUILayout.LabelField("Spell Area Corruption Per Seconds", GUILayout.Width(60));
        EditorGUILayout.LabelField("Properties Effect Multiplicator", GUILayout.Width(60));*/
            EditorGUILayout.EndHorizontal();
            for (int i = 0; i < data.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                data[i].name = EditorGUILayout.TextField(data[i].name, GUILayout.Width(60));
                data[i].duration = EditorGUILayout.IntField(data[i].duration, GUILayout.Width(60));
                data[i].cot = EditorGUILayout.IntField(data[i].cot, GUILayout.Width(60));
                EditorGUILayout.LabelField((data[i].cot * data[i].duration).ToString(), GUILayout.Width(60));
                data[i].dot = EditorGUILayout.IntField(data[i].dot, GUILayout.Width(65));
                EditorGUILayout.LabelField((data[i].dot * data[i].duration).ToString(), GUILayout.Width(60));
                if (GUILayout.Button("Remove"))
                {
                    data.RemoveAt(i);
                    for (int j = 0; j < data.Count; j++)
                    {
                        data[j].compatibilities.RemoveAt(i);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            if (data.Count > 0)
            {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("name", GUILayout.Width(60));
                for (int i = 0; i < data.Count; i++)
                {
                    EditorGUILayout.BeginVertical(GUILayout.Width(60));
                    EditorGUILayout.LabelField(data[i].name, GUILayout.Width(60));
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
                for (int i = 0; i < data.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(data[i].name, GUILayout.Width(60));
                    int j;
                    for (j = 0; j < data[i].compatibilities.Count; j++)
                    {
                        data[i].compatibilities[j] = EditorGUILayout.IntField(data[i].compatibilities[j],
                            GUILayout.Width(60));
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
            }
        }
        EditorGUILayout.EndVertical();

    }


}
