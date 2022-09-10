using UnityEngine;

namespace Tameru.Application
{
    [CreateAssetMenu(fileName = "PlayerParameter", menuName = "ScriptableObjects/PlayerParameter", order = 1)]
    public class PlayerParameter:ScriptableObject
    {
        public float FreezeSpeed { get; } = 0;
        public float SlowWalkSpeed=>slowWalkSpeed;
        public float WalkSpeed=>walkSpeed;
        public float Territory => territory;
        
        [SerializeField] private float slowWalkSpeed;
        [SerializeField] private float walkSpeed;
        [SerializeField,Tooltip("敵がそれ以上近づけない距離")] private float territory;
        
    }
}