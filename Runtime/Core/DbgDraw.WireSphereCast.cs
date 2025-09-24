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
        public static void WireSphereCast(Vector3 center, float radius, Vector3 direction, float distance, Color color, float duration = 0, bool depthTest = true)
        {
            if (!isEnabledAndPlaying)
                return;

            radius = Mathf.Max(0, radius);

            // Start and end sphere
            WireSphere(center, Quaternion.identity, Vector3.one * (radius * 2f), color, duration, depthTest);
            Vector3 endCenter = center + direction.normalized * distance;
            WireSphere(endCenter, Quaternion.identity, Vector3.one * (radius * 2f), color, duration, depthTest);

            // Draw guide lines (connect start and end along main axes)
            Vector3 right = Vector3.right * radius;
            Vector3 up = Vector3.up * radius;
            Vector3 forward = Vector3.forward * radius;

            // Rim points at start
            Vector3[] startRim =
            {
                center + right, center - right,
                center + up, center - up,
                center + forward, center - forward
            };

            // Rim points at end
            Vector3[] endRim = 
            {
                endCenter + right, endCenter - right,
                endCenter + up, endCenter - up,
                endCenter + forward, endCenter - forward
            };

            for (int i = 0; i < startRim.Length; i++)
            {
                Line(startRim[i], endRim[i], color, duration, depthTest);
            }
        }
    }
}