using System;
using UnityEngine;

namespace Code.Views
{
    public class LevelEndView : MonoBehaviour
    {
        public event Action<Collider2D> OnLevelEndEnter = _ => { };

        public void OnTriggerEnter2D(Collider2D other)
        {
            OnLevelEndEnter.Invoke(other);
        }
    }
}
