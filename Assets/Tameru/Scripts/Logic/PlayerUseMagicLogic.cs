using Tameru.Application;
using Tameru.Entity;
using Tameru.Parameter;
using Tameru.Utility;
using Tameru.View;
using UnityEngine;

namespace Tameru.Logic
{
    public class PlayerUseMagicLogic
    {
        private readonly PlayerChargeEntity _playerChargeEntity;
        private readonly PlayerMoveEntity _playerMoveEntity;
        private readonly ISoundEntity _soundEntity;
        private readonly PlayerMagicView _playerMagicView;
        private readonly PlayerMagicParameter _playerMagicParameter;

        public PlayerUseMagicLogic(PlayerChargeEntity playerChargeEntity, PlayerMoveEntity playerMoveEntity,
            ISoundEntity soundEntity, PlayerMagicView playerMagicView, PlayerMagicParameter playerMagicParameter)
        {
            _playerChargeEntity = playerChargeEntity;
            _playerMoveEntity = playerMoveEntity;
            _soundEntity = soundEntity;
            _playerMagicView = playerMagicView;
            _playerMagicParameter = playerMagicParameter;
        }

        public void UpdateUseMagic()
        {
            if (InputKeyData.CanUseMagic)
            {
                TryUseMagic();
            }
        }

        private void TryUseMagic()
        {
            var type = _playerChargeEntity.currentMagic.Value;
            _playerChargeEntity.ConsumeAll();
            if (type == MagicType.None)
            {
                return;
            }

            var magic = _playerMagicParameter.Find(type);
            var direction = _playerMoveEntity.direction;
            var theta = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, theta));
            var instance = Object.Instantiate(magic.prefab, _playerMoveEntity.pos, rotation);
            instance.Shot(direction);

            _soundEntity.SetUpPlaySe(type.ConvertSeType());
        }
    }
}