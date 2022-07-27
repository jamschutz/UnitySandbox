using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace jsch.Editor.LevelCreator
{
    public static class EditorUtils
    {
        // Creates a new scene
        public static void NewScene()
        {
            EditorApplication.SaveCurrentSceneIfUserWantsTo();
            EditorApplication.NewScene();
        }

        // remove all the elements of the scene
        public static void CleanScene()
        {
            GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();
            foreach(var go in allObjects) {
                GameObject.DestroyImmediate(go);
            }
        }

        // Creates a new scene capable to be used as a level
        public static void NewLevel()
        {
            NewScene();
            CleanScene();
            GameObject levelGO = new GameObject("Level");
            levelGO.transform.position = Vector3.zero;
            
        }
    }
}

