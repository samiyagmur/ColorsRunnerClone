using Managers;
using UnityEngine;

namespace Controllers.BuildingControllers
{
    public class SideObjectPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private BuildingManager buildingManager;

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                Destroy(other.gameObject);
            }
        }

    }
}