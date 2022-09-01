using Controllers;
using Signals;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    public class ParticalManager : MonoBehaviour
    {
        [SerializeField] private List<ParticleEmitController> emitController = new List<ParticleEmitController>();

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ParticalSignals.Instance.onParticleBurst += OnParticleBurst;
            ParticalSignals.Instance.onCollectableDead += OnCollectableDead;
            ParticalSignals.Instance.onParticleStop += OnParticleStop;
        }

        private void UnsubscribeEvents()
        {
            ParticalSignals.Instance.onParticleBurst -= OnParticleBurst;
            ParticalSignals.Instance.onCollectableDead -= OnCollectableDead;
            ParticalSignals.Instance.onParticleStop -= OnParticleStop;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion Event Subscription
        private void OnDecreaseStack(int arg0)
        {
           
        }

        private void OnParticleStop()
        {
            for (int i = 0; i < emitController.Count; i++)
            {
                emitController[i].EmitStop();
            }
        }

        private void OnCollectableDead(Vector3 arg0)
        {

            CollectableDead(arg0);
        }

        private async void OnParticleBurst(Vector3 arg0)
        {
            Vector3 newTransform = new Vector3(Random.Range(arg0.x - 1.5f, arg0.x + 1.5f),arg0.y,arg0.z);
            emitController[0].EmitParticle(newTransform);

            await Task.Delay(100);

            emitController[1].EmitParticle(newTransform + new Vector3(0, 4, 3));





        }

        private void CollectableDead(Vector3 transforPosition)
        {
            emitController[1].EmitParticle(transforPosition);
        }



    }
}