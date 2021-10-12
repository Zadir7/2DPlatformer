using UnityEngine;

namespace Code.Views
{
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class BaseView : MonoBehaviour
    {
        public Transform ViewTransform;
        public SpriteRenderer Renderer;
        public Rigidbody2D Rigidbody;

        public void Awake()
        {
            ViewTransform = GetComponent<Transform>();
            Renderer = GetComponent<SpriteRenderer>();
            Rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}