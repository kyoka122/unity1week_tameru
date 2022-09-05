using Tameru.Entity;
using UnityEngine;

namespace Tameru.Logic
{
    public class PlayerLogic
    {
        private ChargeEntity _chargeEntity;

        public PlayerLogic(ChargeEntity chargeEntity)
        {
            _chargeEntity = chargeEntity;
        }

        private void Move()
        {
            
        }

        public void Charge()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _chargeEntity.AddDefault();
            }
        }
    }
}