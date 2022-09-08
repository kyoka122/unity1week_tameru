using UnityEngine;

namespace Tameru.Struct
{
    [CreateAssetMenu(fileName = "PlayerParameter", menuName = "ScriptableObjects/PlayerParameter", order = 1)]
    public class PlayerParameter:ScriptableObject
    {
        [SerializeField] private float slowWalkSpeed;
        [SerializeField] private float walkSpeed;
        
        public float SlowWalkSpeed=>slowWalkSpeed;
        public float WalkSpeed=>walkSpeed;
    }
}