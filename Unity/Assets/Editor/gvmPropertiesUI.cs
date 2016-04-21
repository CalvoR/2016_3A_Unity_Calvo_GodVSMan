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
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Property")) {
            int length = 0;
            if (data.Count > 0) {
                length = data[0].compatibility.Count;
            }
            Debug.Log(length);
            data.Add(new gvmSpellProperty(length));
            for (int j = 0; j < data.Count; j++) {
                data[j].compatibility.Add(0);
            }
        }
        EditorGUILayout.EndHorizontal();
        if (data.Count > 0) {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("name", GUILayout.Width(60));
            for (int i = 0; i < data.Count; i++) {
                EditorGUILayout.BeginVertical(GUILayout.Width(60));
                if (GUILayout.Button("Remove")) {
                    data.RemoveAt(i);
                    for (int j = 0; j < data.Count; j++) {
                        data[j].compatibility.RemoveAt(i);
                    }
                }
                data[i].name = EditorGUILayout.TextField(data[i].name);
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
            for (int i = 0; i < data.Count; i++) {
                EditorGUILayout.BeginHorizontal();
                data[i].name = EditorGUILayout.TextField(data[i].name, GUILayout.Width(60));
                int j;
                for (j = 0; j < data[i].compatibility.Count; j++) {
                    data[i].compatibility[j] = EditorGUILayout.IntField(data[i].compatibility[j], GUILayout.Width(60));
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();

    }


}
