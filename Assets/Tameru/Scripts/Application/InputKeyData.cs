using UnityEngine;

namespace Tameru.Application
{
    public static class InputKeyData
    {
        public static float VerticalMoveValue=>Input.GetAxis("Vertical");
        public static float HorizontalMoveValue=>Input.GetAxis("Horizontal");
        public static bool IsCharging => Input.GetKey(KeyCode.Mouse0);

        public static bool CanUseMagic => Input.GetKeyUp(KeyCode.Mouse0);
    }
}