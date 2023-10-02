using UnityEngine;

namespace Entities
{
    public class WaterTexture : MonoBehaviour
    {
        private MeshRenderer rend;
        [Range(0,1f)]
        [SerializeField] private float speed =0.5f;

        void Start()
        {
            rend = GetComponent<MeshRenderer>();
        }

        void Update()
        {
            float scaleX = Mathf.Cos(Time.time) * speed + 1;
            float scaleY = Mathf.Sin(Time.time) * speed + 1;
            rend.material.mainTextureScale = new Vector2(scaleX, scaleY);
        }
    }
}