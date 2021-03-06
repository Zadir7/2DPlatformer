using UnityEngine;

namespace Code.Utils
{
    public static partial class VectorExtensions
    {
        public static Vector3 With(this Vector3 origin, float? x = null, float? y = null, float? z = null) =>
            new Vector3(x ?? origin.x, y ?? origin.y, z ?? origin.z);
        public static Vector2 With(this Vector2 origin, float? x = null, float? y = null) =>
            new Vector2(x ?? origin.x, y ?? origin.y);
    }
}