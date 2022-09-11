using System;
using System.Collections.Generic;
using System.Linq;
using Tameru.Application;
using Tameru.Entity;
using UnityEngine;

namespace Tameru.Parameter
{
    [CreateAssetMenu(fileName = "PlayerMagicParameter", menuName = "ScriptableObjects/PlayerMagicParameter", order = 2)]
    public class PlayerMagicParameter:ScriptableObject
    {
        [SerializeField] private List<MagicData> data;
        
        public Dictionary<MagicType, int> GetAllChargeValue()
        {
            return data.ToDictionary(param => param.type, param => param.chargeValue);
        }

        public MagicData Find(MagicType type)
        {
            var magic = data.Find(x => x.type == type);
            if (magic == null)
            {
                throw new Exception($"magic data is nothing: {type}");
            }

            return magic;
        }

        public int FindDamage(MagicType findType)
        {
            int? result = data.FirstOrDefault(magic => magic.type == findType)?.damage;
            if (result==null)
            {
                Debug.LogError("Not Found Data");
                return -1;
            }
            return result.Value;
        }

        public string FindName(MagicType findType)
        {
            string result = data.FirstOrDefault(magic => magic.type == findType)?.name;
            if (result==null)
            {
                Debug.LogError("Not Found Data");
            }
            return result;
        }

        public int FindChargeValue(MagicType findType)
        {
            int? result= data.FirstOrDefault(magic => magic.type == findType)?.chargeValue;
            if (result==null)
            {
                Debug.LogError($"Not Found Data");
                return -1;
            }
            return result.Value;
        }
    }
}