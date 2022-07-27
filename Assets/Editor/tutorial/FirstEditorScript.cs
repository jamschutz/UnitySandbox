using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace jsch.Editor
{
    public class FirstEditorScript : MonoBehaviour
    {
        #if UNITY_EDITOR
        [MenuItem("GameObject/Create HelloWorld")]
        private static void CreateHelloWorldGameObject() 
        {
            if(EditorUtility.DisplayDialog(
                "Hello World",
                "Do you really want to do this?",
                "Create",
                "Cancel"
            )) {
                new GameObject("HelloWorld");
            }
        }
        #endif
    }

}
