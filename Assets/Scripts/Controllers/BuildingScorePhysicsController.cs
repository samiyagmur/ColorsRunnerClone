using System;
using System.Threading.Tasks;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class BuildingScorePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private BuildingManager buildingManager;

        private float _timer = 0.05f;

        #endregion

        #endregion

        private void OnTriggerStay(Collider other)   
        {
            _timer -= Time.fixedDeltaTime ;
            
            if (_timer <= 0)
            {
                _timer = 0.05f;
                
                UpdatePayedAmount();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _timer = 0.05f;
            }
        }

        private  void UpdatePayedAmount()
        {
            buildingManager.PayedAmount++;
        }
    }
}