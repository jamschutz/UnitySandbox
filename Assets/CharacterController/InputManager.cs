using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jsch.Managers
{
    public class MovementInput
    {
        public KeyCode forward = KeyCode.W;
        public KeyCode backward = KeyCode.S;
        public KeyCode left = KeyCode.A;
        public KeyCode right = KeyCode.D;
    }

    public static class InputManager
    {
        public static MovementInput Movement = new MovementInput();
    }
}