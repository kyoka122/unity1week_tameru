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
        private readonly GraphicFlashView _flashView;
        private readonly VolumeView _volumeView;

        public TopLogic(SceneEntity sceneEntity, SoundEntity soundEntity, GraphicFlashView flashView, VolumeView volumeView)
        {
            _sceneEntity = sceneEntity;
            _soundEntity = soundEntity;
            _volumeView = volumeView;
            _flashView = flashView;

            _soundEntity.SetUpPlayBgm(BgmType.Top);
            _flashView.Init();

            foreach (var button in Object.FindObjectsOfType<BaseButtonView>())
            {
                button.Init(_soundEntity.SetUpPlaySe, _soundEntity.SetUpPlaySe);

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

                // マウスカーソルを離したタイミングで効果音再生
                _volumeView.OnPointerUpBgmSlider()
                    .Subscribe(_soundEntity.SetUpPlaySe)
                    .AddTo(_volumeView);
                _volumeView.OnPointerUpSeSlider()
                    .Subscribe(_soundEntity.SetUpPlaySe)
                    .AddTo(_volumeView);
            }
        }
    }
}