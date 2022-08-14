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

        public void EnterTurretArea(Transform transformCollectable)
        {
            CollectablePos = transformCollectable.position;
            ChangeTurrentMovementWithState(TurretAreaType.InPlaceTurretArea);
        }
        public void ExitTurretArea()
        {
            Debug.Log("ExitTurretArea");
            CancelInvoke("TaretMovement");
        }

        private void Start()
        {   
            InvokeRepeating("TaretMovement", 0.01f, 1f);
        }

        private void TaretMovement()
        {
            ChangeTurrentMovementWithState(turretAreaType);
        }
        public void ChangeTurrentMovementWithState(TurretAreaType turretAreaType)
        {
            Debug.Log(turretAreaType);
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

            Vector3 relativePos = shotPositon - transform.position;
            rotation = Quaternion.LookRotation(relativePos);

            transform.DORotateQuaternion(rotation, 0.9f).OnComplete(() => Debug.DrawRay(transform.position, relativePos, Color.red));

            RaycastHit hit;

            if (Physics.Raycast(transform.position, rotation.eulerAngles, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Collectable"))
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}