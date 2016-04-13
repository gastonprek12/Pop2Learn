using UnityEngine;

namespace Bigfoot
{
    public static class SmartVector
    {
        public static Vector3 ReplaceX(this Vector3 lhs, float val)
        {
            lhs.x = val;
            return lhs;
        }
        public static Vector3 ReplaceY(this Vector3 lhs, float val)
        {
            lhs.y = val;
            return lhs;
        }
        public static Vector3 ReplaceZ(this Vector3 lhs, float val)
        {
            lhs.z = val;
            return lhs;
        }
        public static Vector2 ReplaceX(this Vector2 lhs, float val)
        {
            lhs.x = val;
            return lhs;
        }
        public static Vector2 ReplaceY(this Vector2 lhs, float val)
        {
            lhs.y = val;
            return lhs;
        }
    }
}