using UnityEngine;
using UnityEditor;
using System.Collections;

class BossBattleDesigner : EditorWindow {
    [MenuItem("Window/Boss Battle Designer")]

    public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(BossBattleDesigner));
    }

    void OnGUI() {
        // The actual window code goes here
    }
}