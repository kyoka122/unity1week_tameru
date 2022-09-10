using System;
using UnityEngine;

namespace Tameru.View
{
    public sealed class LoadButtonView : BaseButtonView
    {
        [SerializeField] private SceneName sceneName = default;

        public void Init(Action<SceneName> action)
        {
            push += () => action?.Invoke(sceneName);
        }
    }
}