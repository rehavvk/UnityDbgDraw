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
        public static void WireCapsuleCast(Vector3 center, float radius, float height, Vector3 direction, Quaternion orientation, float distance, Color color, float duration = 0, bool depthTest = true)
        {
            if (!isEnabledAndPlaying)
                return;

            // Clamp values to valid capsule
            radius = Mathf.Max(0, radius);
            float cappedHeight = Mathf.Max(0, height - radius * 2);

            // Draw start capsule
            WireCapsule(center, orientation, radius, height, color, duration, depthTest);

            // Draw end capsule
            Vector3 endCenter = center + direction.normalized * distance;
            WireCapsule(endCenter, orientation, radius, height, color, duration, depthTest);

            // Connect start/end by drawing "edge lines"
            Vector3[] startRim = GetWireCapsuleRim(center, radius, cappedHeight, orientation);
            Vector3[] endRim = GetWireCapsuleRim(endCenter, radius, cappedHeight, orientation);

            for (int i = 0; i < startRim.Length; i++)
            {
                Line(startRim[i], endRim[i], color, duration, depthTest);
            }
        }

        private static Vector3[] GetWireCapsuleRim(Vector3 center, float radius, float height, Quaternion orientation)
        {
            // Approximate with 8 rim points (N,E,S,W around top and bottom)
            Vector3[] rim = new Vector3[8];
            Vector3 up = orientation * Vector3.up * (height * 0.5f);
            Vector3 right = orientation * Vector3.right * radius;
            Vector3 forward = orientation * Vector3.forward * radius;

            // top circle
            rim[0] = center + up + right;
            rim[1] = center + up - right;
            rim[2] = center + up + forward;
            rim[3] = center + up - forward;

            // bottom circle
            rim[4] = center - up + right;
            rim[5] = center - up - right;
            rim[6] = center - up + forward;
            rim[7] = center - up - forward;

            return rim;
        }
    }
}