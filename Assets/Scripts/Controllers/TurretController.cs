using System.Collections;
using UnityEngine;
using Enums;
using DG.Tweening;

namespace Controllers
{
    public class TurretController : MonoBehaviour
    {
        Quaternion rotation;
        Vector3 shotPositon;
        Vector3 CollectablePos;
        [SerializeField]
        Vector2 xTaretPosClamp;
        [SerializeField]
        Vector2 yTaretPosClamp;
        TurretAreaType turretAreaType;
        RaycastHit hit;

        public void EnterTurretArea(Transform transformCollectable)
        {
            CollectablePos = transformCollectable.position;
            turretAreaType = TurretAreaType.InPlaceTurretArea;
            Debug.Log("EnterTurretArea");
            
        }
        public void ExitTurretArea()
        {
            Debug.Log("ExitTurretArea");
            CancelInvoke("TaretMovement");
        }

        private void Start()
        {   
            InvokeRepeating("TaretMovement", 0.01f, 0.5f);
        }

        private void TaretMovement()
        {
            ChangeTurrentMovementWithState(turretAreaType);
        }
        public void ChangeTurrentMovementWithState(TurretAreaType turretAreaType)
        {
            
            switch (turretAreaType)
            {
                case TurretAreaType.UnPlaceTurretArea:
                    float randomX = Random.Range(xTaretPosClamp.x, yTaretPosClamp.x);
                    float randomY = Random.Range(xTaretPosClamp.y, yTaretPosClamp.y);
                    shotPositon = new Vector3(randomX, 0, randomY);
                    break;
                case TurretAreaType.InPlaceTurretArea:
                    shotPositon = CollectablePos+new Vector3(0,0,0);
                    break;
            }

            Vector3 relativePos = shotPositon - transform.position;
            rotation = Quaternion.LookRotation(relativePos);
            
            transform.DORotateQuaternion(rotation, 0.3f).OnComplete(()=> HitWithRaycast());
        }



        private void HitWithRaycast()
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Debug.DrawRay(transform.position, transform.forward*15, Color.red);

                if (hit.transform.gameObject.CompareTag("Collectable"))
                {
                    Debug.Log("Collectable");
                    Destroy(hit.transform.parent.gameObject);
                }
            }

        }
    }
}