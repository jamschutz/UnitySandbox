using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace jsch.Editor.LevelCreator
{
    public static class MenuItems
    {
        [MenuItem("Tools/Level Creator/New Level Scene")]
        private static void NewLevel()
        {
            EditorUtils.NewLevel();
        }
        
    }
}