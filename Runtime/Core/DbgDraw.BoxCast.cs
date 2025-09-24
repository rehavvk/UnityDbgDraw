// https://github.com/pschraut/UnityDbgDraw
using UnityEngine;
#pragma warning disable IDE0018 // Variable declaration can be inlined
#pragma warning disable IDE0017 // Object initialization can be simplified

namespace Oddworm.Framework
{
    public partial class DbgDraw
    {
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        [System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
        public static void BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float distance, Color color, float duration = 0, bool depthTest = true)
        {
            // Draw start cube
            Cube(center, orientation, halfExtents * 2f, color, duration, depthTest);

            // Draw end cube
            Vector3 endCenter = center + direction.normalized * distance;
            Cube(endCenter, orientation, halfExtents * 2f, color, duration, depthTest);

            // Get corners of start & end cubes to connect them
            Vector3[] startCorners = GetBoxCorners(center, halfExtents, orientation);
            Vector3[] endCorners = GetBoxCorners(endCenter, halfExtents, orientation);

            for (int i = 0; i < 8; i++)
            {
                Line(startCorners[i], endCorners[i], color, duration, depthTest);
            }
        }

        private static Vector3[] GetBoxCorners(Vector3 center, Vector3 halfExtents, Quaternion orientation)
        {
            Vector3[] corners = new Vector3[8];
            int i = 0;
            for (int x = -1; x <= 1; x += 2)
            {
                for (int y = -1; y <= 1; y += 2)
                {
                    for (int z = -1; z <= 1; z += 2)
                    {
                        Vector3 local = new Vector3(x * halfExtents.x, y * halfExtents.y, z * halfExtents.z);
                        corners[i++] = center + orientation * local;
                    }
                }
            }
            return corners;
        }
    }
}
