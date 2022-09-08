using UnityEngine;

namespace Tameru.Application
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