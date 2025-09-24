// DbgDraw for Unity. Copyright (c) 2019-2024 Peter Schraut (www.console-dev.de). See LICENSE.md
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
        public static void SphereCast(Vector3 center, float radius, Vector3 direction, float distance, Color color, float duration = 0, bool depthTest = true)
        {
            if (!isEnabledAndPlaying)
                return;

            radius = Mathf.Max(0, radius);

            // Start and end sphere
            Sphere(center, Quaternion.identity, Vector3.one * (radius * 2f), color, duration, depthTest);
            Vector3 endCenter = center + direction.normalized * distance;
            Sphere(endCenter, Quaternion.identity, Vector3.one * (radius * 2f), color, duration, depthTest);

            // Draw connecting "guide cylinders" as lines (like a swept sphere volume)
            Vector3 right = Vector3.right * radius;
            Vector3 up = Vector3.up * radius;
            Vector3 forward = Vector3.forward * radius;

            Vector3[] startRim = 
            {
                center + right, center - right,
                center + up,    center - up,
                center + forward, center - forward
            };

            Vector3[] endRim = 
            {
                endCenter + right, endCenter - right,
                endCenter + up,    endCenter - up,
                endCenter + forward, endCenter - forward
            };

            for (int i = 0; i < startRim.Length; i++)
            {
                Line(startRim[i], endRim[i], color, duration, depthTest);
            }
        }
    }
}