using UnityEngine;

namespace Code.Views
{
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseView : MonoBehaviour
    {
        public Transform ViewTransform;
        public SpriteRenderer Renderer;
        public Rigidbody2D Rigidbody;
        public Collider2D ViewCollider;

        public void Awake()
        {
            ViewTransform = GetComponent<Transform>();
            Renderer = GetComponent<SpriteRenderer>();
            Rigidbody = GetComponent<Rigidbody2D>();
            ViewCollider = GetComponent<Collider2D>();
        }
    }
}