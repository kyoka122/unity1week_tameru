using UnityEngine;

namespace Tameru.Application
{
    [CreateAssetMenu(fileName = "PlayerParameter", menuName = "ScriptableObjects", order = 1)]
    public class PlayerParameter:ScriptableObject
    {
        public float SlowWalkSpeed=>slowWalkSpeed;
        public float WalkSpeed=>walkSpeed;
        
        [SerializeField] private float slowWalkSpeed;
        [SerializeField] private float walkSpeed;
    }
}