using System;
using System.Collections.Generic;
using Datas.UnityObject;
using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class CollectableMovementCommand : MonoBehaviour
    {
        #region Serialized Variables

        [SerializeField] private CollectableManager collectableManager;

        #endregion

        public void MoveToGround(Transform groundTransform)
        {
            var randomZValue =Random.Range(-(groundTransform.localScale.z/2),(groundTransform.localScale.z/2 - 7));
            
            Vector3 pos = new Vector3(groundTransform.position.x, groundTransform.position.y,groundTransform.position.z + randomZValue);
            
            transform.DOMove(pos,2f).OnComplete(() =>
            {
                collectableManager.ChangeAnimationOnController(CollectableAnimType.CrouchIdle);
                collectableManager.ChangeOutline(true);
            });
        }
        
    }
}