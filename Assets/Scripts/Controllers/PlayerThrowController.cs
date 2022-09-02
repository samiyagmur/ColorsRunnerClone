using System.Collections;
using UnityEngine;
using DG.Tweening;
using Signals;

namespace Controllers
{
    public class PlayerThrowController : MonoBehaviour
    {
        public void ThrowGameObject(Transform playerTransform)
        {
          // ParticalSignals.Instance.onParticleBurst?.Invoke(playerTransform.position);
        }
    }
}