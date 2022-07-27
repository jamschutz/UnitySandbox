using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jsch.Utils
{
    public class HideLockMouse : MonoBehaviour
    {
        public bool hideMouse;
        public bool lockMouse;

        void Start()
        {
            Cursor.visible = !hideMouse;
            Cursor.lockState = lockMouse? CursorLockMode.Locked : CursorLockMode.None;
        }

        public void ShowMouse() { Cursor.visible = true; }
        public void HideMouse() { Cursor.visible = false; }

        public void LockMouse() { Cursor.lockState = CursorLockMode.Locked; }
        public void UnlockMouse() { Cursor.lockState = CursorLockMode.None; }
    }
}
