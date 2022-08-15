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
        Vector3 relativePos;
        [SerializeField]
        Vector2 xTaretPosClamp;
        [SerializeField]
        Vector2 yTaretPosClamp;
        TurretAreaType turretAreaType;

        public void EnterTurretArea(Transform transformCollectable)
        {
            CollectablePos = transformCollectable.position;
            turretAreaType = TurretAreaType.InPlaceTurretArea;
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

            Debug.Log(CollectablePos);
            switch (turretAreaType)
            {
                case TurretAreaType.UnPlaceTurretArea:
                    float randomX = Random.Range(xTaretPosClamp.x, yTaretPosClamp.x);
                    float randomY = Random.Range(xTaretPosClamp.y, yTaretPosClamp.y);
                    shotPositon = new Vector3(randomX, 0, randomY);
                    break;
                case TurretAreaType.InPlaceTurretArea:
                    shotPositon = CollectablePos;
                    break;
            }
            
            relativePos = shotPositon - transform.position;
            rotation = Quaternion.LookRotation(relativePos);

            transform.DORotateQuaternion(rotation, 0.3f).OnComplete(() => RayCasting());

        }

        public void RayCasting()
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Debug.DrawRay(transform.position, transform.forward*10, Color.red,0.1f);
                if (hit.transform.gameObject.CompareTag("Collected"))
                {
                    Debug.Log("Collectable");
                    Destroy(hit.transform.parent.gameObject);
                }
            }
        }
    }
}