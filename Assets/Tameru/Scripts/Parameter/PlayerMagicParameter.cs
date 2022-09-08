using System.Collections.Generic;
using Tameru.Entity;
using Tameru.Utility;
using UnityEngine;

namespace Tameru.Parameter
{
    [CreateAssetMenu(fileName = "PlayerMagicParameter", menuName = "ScriptableObjects/PlayerMagicParameter", order = 2)]
    public class PlayerMagicParameter:ScriptableObject
    {
        public int WeakMagicNeedChargeValue=>magicNeedChargeValue[1];
        public int StrongMagicNeedChargeValue=>magicNeedChargeValue[2];

        public string[] MagicName => magicName;
        //[SerializeField] private int weakMagicNeedChargeValue;
        //[SerializeField] private int strongMagicNeedChargeValue;
        
        [SerializeField, EnumIndex(typeof(MagicMode))] private int[] magicNeedChargeValue;
        [SerializeField, EnumIndex(typeof(MagicMode))] private string[] magicName;
        
        public Dictionary<MagicMode, int> GetNeedChargeParameter()
        {
            return new()
            {
                {MagicMode.None, 0},
                {MagicMode.Weak, WeakMagicNeedChargeValue},
                {MagicMode.Strong, StrongMagicNeedChargeValue}
            };
        }
    }
}