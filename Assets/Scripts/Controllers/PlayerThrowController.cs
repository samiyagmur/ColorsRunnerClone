using System.Collections;
using UnityEngine;
using DG.Tweening;
namespace Controllers
{
    public class PlayerThrowController : MonoBehaviour
    {
        #region Self Variables
        #region Private Variables

        public GameObject _throwObject;
        public Rigidbody _throwRigidbody;

        #endregion
        #endregion

        private void Awake()
        {
            _throwObject = GetGameObject();
            
        }

        private GameObject GetGameObject() => Resources.Load<GameObject>("PlayerPrefabs/Ragdol");


        public void ThrowGameObject()
        {
            var obj = Instantiate(_throwObject, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f), Quaternion.identity);
            obj.transform.DOJump(new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y + Random.Range(1, 4), transform.position.z + 20f), 5f, 1, 1.5f);
            obj.transform.rotation = new Quaternion(Random.Range(-180, 180), Random.Range(180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
        }
    }
}