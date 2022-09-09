using System.Collections.Generic;
using Tameru.Entity;
using Tameru.Utility;
using UniRx;
using UnityEngine;

namespace Tameru.Parameter
{
    [CreateAssetMenu(fileName = "PlayerMagicParameter", menuName = "ScriptableObjects/PlayerMagicParameter", order = 2)]
    public class PlayerMagicParameter:ScriptableObject
    {
        public int[] MagicChargeValue=>magicChargeValue;

        public string[] MagicName => magicName;
        //[SerializeField] private int weakMagicNeedChargeValue;
        //[SerializeField] private int strongMagicNeedChargeValue;
        
        [SerializeField, EnumIndex(typeof(MagicMode))] private int[] magicChargeValue;
        [SerializeField, EnumIndex(typeof(MagicMode))] private string[] magicName;
        
        public Dictionary<MagicMode, int> GetChargeParameter()
        {
            return new()
            {
                {MagicMode.None, magicChargeValue[0]},
                {MagicMode.Weak, magicChargeValue[1]},
                {MagicMode.Strong, magicChargeValue[2]}
            };
        }
    }
}