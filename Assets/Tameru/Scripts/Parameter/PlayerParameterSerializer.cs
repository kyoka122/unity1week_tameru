using Tameru.Struct;
using UniRx;
using UnityEngine;

namespace Tameru.Parameter
{
    public class PlayerParameterSerializer:MonoBehaviour
    {
        [SerializeField] private ReactiveProperty<PlayerParameter> playerParameter;

        public ReactiveProperty<PlayerParameter> PlayerParameter => playerParameter;

        //[SerializeField] private ReactiveProperty<float> slowWalkSpeed;
        //[SerializeField] private ReactiveProperty<float> walkSpeed;
        //public ReactiveProperty<float> SlowWalkSpeed => slowWalkSpeed;
        //public ReactiveProperty<PlayerParameter> WalkSpeed => walkSpeed;
    }
}