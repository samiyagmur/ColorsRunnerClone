using System.Collections;
using UnityEngine;
using Enums;
using DG.Tweening;
using Signals;

namespace Controllers
{
    public class TurretController : MonoBehaviour
    {


        #region Self Variables
        #region SerializeField Variables
        [SerializeField]
        private Transform _taretAreaTranform;
        [SerializeField]
        private float timeİncreaseSpeed;
        [SerializeField]
        private float invokeRepeatTime;
        #endregion
        #region Private Variables
        private Quaternion rotation;
        private Vector3 shotPositon;
        private Vector3 collectablePos;
        private Vector3 relativePos;
        private float randomClampStartPos;
        private float randomClampEndPos;
        private float lerpTime;
        TurretAreaType turretAreaType;
        #endregion
        #endregion
       
        
   

        public void EnterTurretArea(Transform transformCollectable)
        {
            collectablePos = transformCollectable.position;
            turretAreaType = TurretAreaType.InPlaceTurretArea; 
        }
        public void ExitTurretArea()
        {
            Debug.Log("ExitTurretArea");
            CancelInvoke("TaretMovement");
        }

        private void Start()
        {
           
           // InvokeRepeating("TaretMovement", 0, 0.5f);
            InvokeRepeating("GetRandomPos", 0, invokeRepeatTime);
        }

        private void GetRandomPos()
        {   
            float TaretAreaStartXPos = _taretAreaTranform.position.x - _taretAreaTranform.GetChild(0).transform.localScale.x;
            float TaretAreaEndXpos = _taretAreaTranform.position.x + _taretAreaTranform.GetChild(1).transform.localScale.x;
            float TurretAreaStartZpos = _taretAreaTranform.position.z - _taretAreaTranform.GetChild(0).transform.localScale.z / 2;
            float TurretAreaEndZPos = _taretAreaTranform.position.z + _taretAreaTranform.GetChild(1).transform.localScale.z / 2;
            
            randomClampStartPos = Random.Range(TaretAreaStartXPos, TaretAreaEndXpos);
            randomClampEndPos = Random.Range(TurretAreaStartZpos, TurretAreaEndZPos);

            Debug.Log(_taretAreaTranform.position.x + "   " + _taretAreaTranform.GetChild(0).transform.localScale);
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
                case TurretAreaType.OutPlaceTurretArea:
                    
                    shotPositon = new Vector3(randomClampStartPos, 0, randomClampEndPos);
                    relativePos = shotPositon - transform.position;
                    rotation = Quaternion.LookRotation(relativePos);

                    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Mathf.Lerp(0,1, timeİncreaseSpeed));


                    break;

                case TurretAreaType.InPlaceTurretArea:
                    shotPositon = collectablePos+new Vector3(0,0,0);
                    relativePos = shotPositon - transform.position;
                    rotation = Quaternion.LookRotation(relativePos);
                    //Debug.Log(turretAreaType);
                    lerpTime+= timeİncreaseSpeed;
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, lerpTime);//PlayerıYavaşlat;
                    HitWithRaycast();

                    if (lerpTime >= 1)
                    {   
                        lerpTime = 0;
                    }
                    
                    break;
            }
            //transform.DORotateQuaternion(rotation, 0.3f).OnComplete(()=> HitWithRaycast());
        }



        private void HitWithRaycast()
        {
            RaycastHit hit;
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
       
    }
}