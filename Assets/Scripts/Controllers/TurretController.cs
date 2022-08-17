using System.Collections;
using UnityEngine;
using Enums;
using DG.Tweening;
using Signals;

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
        float randomX;
        float randomY;
        Vector3 relativePos;
        public float dd;
        private float ff;
        public float cc;

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
            //InvokeRepeating("TaretMovement",0, 0.5f);
            InvokeRepeating("GetRandomPos", 0, cc);
        }
        private void GetRandomPos()
        {
             randomX= Random.Range(xTaretPosClamp.x, yTaretPosClamp.x);
             randomY = Random.Range(xTaretPosClamp.y, yTaretPosClamp.y);
        }
        private void FixedUpdate()
        {
            TaretMovement();
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
                    
                    shotPositon = new Vector3(randomX, 0, randomY);
                    relativePos = shotPositon - transform.position;
                    rotation = Quaternion.LookRotation(relativePos);

                    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Mathf.Lerp(0,1,dd));


                    break;
                case TurretAreaType.InPlaceTurretArea:
                    shotPositon = CollectablePos+new Vector3(0,0,0);
                    relativePos = shotPositon - transform.position;
                    rotation = Quaternion.LookRotation(relativePos);
                    //Debug.Log(turretAreaType);
                    ff+=dd;
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, ff);//PlayerıYavaşlat;
                    HitWithRaycast();

                    if (ff >= 1)
                    {   
                        ff = 0;
                    }
                    
                    break;
            }
            //transform.DORotateQuaternion(rotation, 0.3f).OnComplete(()=> HitWithRaycast());
        }



        private void HitWithRaycast()
        {   
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {   
                Debug.DrawRay(transform.position, transform.forward*15, Color.red);

                if (hit.transform.gameObject.CompareTag("Collected"))
                {
                    Debug.Log("Collected");
                    StackSignals.Instance.onDecreaseStack?.Invoke(hit.transform.parent.GetSiblingIndex());
                    hit.transform.parent.SetParent(null);
                    Destroy(hit.transform.parent.gameObject);

                }
            }

        }
        private void RandomPos()//burayı yaz
        {

        }
    }
}