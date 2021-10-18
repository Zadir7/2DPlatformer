using UnityEngine;
using Updates;

namespace Code.Controllers.Player
{
    public class ContactPool : IUpdatable
    {
        private ContactPoint2D[] _contacts = new ContactPoint2D[10];
        private int _contactCount;
        private Collider2D _collider;

        private const float CollideThreshold = 0.6f;

        public bool IsGrounded { get; private set; }
        public bool HasLeftContact { get; private set; }
        public bool HasRightContact { get; private set; }

        public ContactPool(Collider2D collider)
        {
            _collider = collider;
        }

        public void Update(float deltaTime)
        {
            IsGrounded = false;
            HasLeftContact = false;
            HasRightContact = false;

            _contactCount = _collider.GetContacts(_contacts);

            for (var i = 0; i < _contactCount; i++)
            {
                if (_contacts[i].normal.y > CollideThreshold) IsGrounded = true;
                if (_contacts[i].normal.x > CollideThreshold) HasLeftContact = true;
                if (_contacts[i].normal.x > -CollideThreshold) HasRightContact = true;
            }
        }
    }
}