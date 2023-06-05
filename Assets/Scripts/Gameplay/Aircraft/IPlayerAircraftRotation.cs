using UnityEngine;

namespace Gameplay
{
    public abstract class PlayerAircraftRotationBase : MonoBehaviour
    {
        public abstract void SetInput(float inputHorizontal, float inputVertical);
    }
}