using System.Collections;
using UnityEngine;
using Enums;
using DG.Tweening;
using Signals;
using System.Threading.Tasks;
using Managers;


namespace Controllers
{
    public class TurretController : MonoBehaviour
    {


        #region Self Variables
        #region SerializeField Variables

        [SerializeField]
        private Transform taretAreaTranform;
        [SerializeField]
        private float timeIncreaseSpeed;
        [SerializeField]
        private float invokeRepeatTimeForRandomPos;

        #endregion
        #region Private Variables

        private Quaternion _rotation;
        private Vector3 _shotPositon;
        private Vector3 _collectablePos;
        private Vector3 _relativePos;
        private float _randomClampStartPos;
        private float _randomClampEndPos;
        TurretAreaType _turretAreaType;

        #endregion
        #endregion

        public void EnterTurretArea(Transform transformCollectable)///Hit.gameobject.transform.pos.z-gameobjecttransform pos.z>localscale.z ? stop
        {
            _collectablePos = transformCollectable.position;
            _turretAreaType = TurretAreaType.InPlaceTurretArea; 

        }
        public void ExitTurretArea()
        {
            Debug.Log("ExitTurretArea");
            CancelInvoke("TaretMovement");
        }

        private void Start()
        {
           // float TurretMaxHitDistance = (float)Math.Sqrt(Math.Pow(_taretAreaTranform.GetChild(0).transform.localScale.x+1, 2) + Math.Pow(_taretAreaTranform.GetChild(0).transform.localScale.y/2, 2));
          
            // InvokeRepeating("TaretMovement", 0, 0.5f);
            InvokeRepeating("GetRandomPos", 0, invokeRepeatTimeForRandomPos);
        }

        private void GetRandomPos()
        {   

            float TaretAreaStartXPos = taretAreaTranform.position.x - taretAreaTranform.GetChild(0).transform.localScale.x;
            float TaretAreaEndXpos = taretAreaTranform.position.x + taretAreaTranform.GetChild(1).transform.localScale.x;
            float TurretAreaStartZpos = taretAreaTranform.position.z - taretAreaTranform.GetChild(0).transform.localScale.z / 2;
            float TurretAreaEndZPos = taretAreaTranform.position.z + taretAreaTranform.GetChild(1).transform.localScale.z / 2;
            
            _randomClampStartPos =Random.Range(TaretAreaStartXPos, TaretAreaEndXpos);
            _randomClampEndPos = Random.Range(TurretAreaStartZpos, TurretAreaEndZPos);

            
        }
        private void FixedUpdate()
        {
            ChangeTurretMovementWithState(_turretAreaType);
        }

        public  void ChangeTurretMovementWithState(TurretAreaType turretAreaType)
        {
            
            switch (turretAreaType)
            {
                case TurretAreaType.OutPlaceTurretArea:
                    
                    _shotPositon = new Vector3(_randomClampStartPos, 0, _randomClampEndPos);
                    _relativePos = _shotPositon - transform.position;
                    _rotation = Quaternion.LookRotation(_relativePos);

                    transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, Mathf.Lerp(0,1, timeIncreaseSpeed*10));


                    break;

                case TurretAreaType.InPlaceTurretArea:

                    _shotPositon = _collectablePos+new Vector3(0,1,0);
                    _relativePos = _shotPositon - transform.position;
                    _rotation = Quaternion.LookRotation(_relativePos);
                    //Debug.Log(turretAreaType);
                    //lerpTime+= timeİncreaseSpeed;
                    transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, Mathf.Lerp(0, 1, timeIncreaseSpeed));//PlayerıYavaşlat;
                    HitWithRaycast();
                   // await Task.Delay(500);
                    break;
            }
            //transform.DORotateQuaternion(rotation, 0.3f).OnComplete(()=> HitWithRaycast());
        }

        private void HitWithRaycast()
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {   
                Debug.DrawRay(transform.position, transform.forward* 15f, Color.red);

                if (hit.transform.gameObject.CompareTag("Collected"))
                {   
                    hit.transform.parent.GetComponent<CollectableManager>().DecreaseStack();
                }
            }

        }
       
    }
}