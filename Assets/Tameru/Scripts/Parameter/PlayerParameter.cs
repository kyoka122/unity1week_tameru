using System;
using UniRx;
using UnityEngine;

namespace Tameru.Struct
{
    [CreateAssetMenu(fileName = "PlayerParameter", menuName = "ScriptableObjects", order = 1)]
    public class PlayerParameter:ScriptableObject
    {
        public float slowWalkSpeed;
        public float walkSpeed;
        
        public float SlowWalkSpeed=>slowWalkSpeed;
        public float WalkSpeed=>walkSpeed;
    }
}