using UnityEngine;

namespace Tameru.View
{
    public class CameraView:MonoBehaviour
    {
        public Camera Camera => camera;

        [SerializeField] private Camera camera;
        
    }
}