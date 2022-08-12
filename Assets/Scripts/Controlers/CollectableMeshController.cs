using Enums;
using System.Collections;
using UnityEngine;


namespace Controlers
{
    public class CollectableMeshController : MonoBehaviour
    {
        [Header("Data")]
        public Material Data;
        private void Awake()
        {

            Data = GetCollectableMaterial();


        }
        private void Start()
        {
            GetComponent<Renderer>().material = Data;
        }

        public Material GetCollectableMaterial(ColorType type) => Resources.Load<Material>($"Materials/{type.ToString()}Mat");

        public void changeOutLine()///BakılcakDron
        {

        }
        public void reChangeOutLine()
        {

        }
    }
}