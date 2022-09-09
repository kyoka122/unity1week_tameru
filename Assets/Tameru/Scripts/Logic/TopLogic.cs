using Tameru.Entity;
using Tameru.View;
using UniRx;
using UnityEngine;

namespace Tameru.Logic
{
    public sealed class TopLogic
    {
        private readonly SceneEntity _sceneEntity;
        private readonly SoundEntity _soundEntity;
        private readonly VolumeView _volumeView;

        public TopLogic(SceneEntity sceneEntity, SoundEntity soundEntity, VolumeView volumeView)
        {
            _sceneEntity = sceneEntity;
            _soundEntity = soundEntity;
            _volumeView = volumeView;

            // TODO: 例外が吐かれてしまうため、BGMが設定されたらコメント解除
            // _soundEntity.SetUpPlayBgm(BgmType.Top);

            foreach (var button in Object.FindObjectsOfType<BaseButtonView>())
            {
                button.Init(_soundEntity.SetUpPlaySe);

                switch (button)
                {
                    case TopButtonView topButton:
                        topButton.Init();
                        break;
                    case LoadButtonView loadButton:
                        loadButton.Init(_sceneEntity.SetUpLoad);
                        break;
                }
            }

            // 音量調整
            {
                _volumeView.Init(_soundEntity.bgmVolume, _soundEntity.seVolume);

                // Slider値と音量を紐付け
                _volumeView.UpdateBgmVolume()
                    .Subscribe(_soundEntity.SetBgmVolume)
                    .AddTo(_volumeView);
                _volumeView.UpdateSeVolume()
                    .Subscribe(_soundEntity.SetSeVolume)
                    .AddTo(_volumeView);

                // TODO: 例外が吐かれてしまうため、SEが設定されたらコメント解除
                // マウスカーソルを離したタイミングで効果音再生
                // _volumeView.OnPointerUpBgmSlider()
                //     .Subscribe(_soundEntity.SetUpPlaySe)
                //     .AddTo(_volumeView);
                // _volumeView.OnPointerUpSeSlider()
                //     .Subscribe(_soundEntity.SetUpPlaySe)
                //     .AddTo(_volumeView);
            }
        }
    }
}