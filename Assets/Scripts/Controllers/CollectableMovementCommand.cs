using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class CollectableMovementCommand : MonoBehaviour
    {
        public void MoveToGround(Transform groundTransform)
        {
            var randomZValue =Random.Range(-(groundTransform.localScale.z/2 - 2),(groundTransform.localScale.z/2 - 2));
            
            Vector3 pos = new Vector3(groundTransform.position.x, groundTransform.position.y,groundTransform.position.z + randomZValue);
            
            transform.DOMove(pos,2f).OnComplete(() =>
            {
                transform.GetComponent<CollectableManager>().ChangeAnimationOnController(CollectableAnimType.CrouchIdle);
            });
        }
    }
}