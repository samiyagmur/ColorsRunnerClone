using System.Collections;
using UnityEngine;

namespace Controllers
{

    public class ParticleEmitController:MonoBehaviour
    {
        [SerializeField] private Vector3 emitPositionAdd;
        [SerializeField] private int particalStartSize;
        [SerializeField] private int bursCount;


        private ParticleSystem _particleSystem;


        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _particleSystem.Stop();
            transform.Rotate(0, 0, 0);
        }


        public void EmitParticle(Vector3 burstPostion)
        {
            gameObject.SetActive(true);

            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams
            {

                position = burstPostion + emitPositionAdd,
                startSize = particalStartSize,

            };
            _particleSystem.Emit(emitParams, bursCount);
            _particleSystem.Play();

        }

        public void EmitStop()
        {
            _particleSystem.Stop();
            gameObject.SetActive(false);
        }
    }
    
}