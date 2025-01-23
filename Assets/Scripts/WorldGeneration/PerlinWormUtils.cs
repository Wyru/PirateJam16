using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PerlinWormUtils
{


    public static Vector3 PerlinWormDirection(Vector3 currentPos)
    {
        float noise = SumNoise(currentPos.x, currentPos.z, 0.01f);
        // float degree = mapValue(noise, 0f, 1f, -180f, 180f);
        float degree;
        if (noise < .5f)
        {
            degree = -180f;
        }else{
            degree = 180f;

        }
        return (Quaternion.AngleAxis(degree, Vector3.up) * currentPos).normalized;
    }

    public static float SumNoise(float x, float z, float frequency)
    {
        float amp = 1;
        float freq = frequency;
        float sum = 0;
        float ampSum = 0;

        for (int i = 0; i < 3; i++)
        {
            sum += amp * Mathf.PerlinNoise(x * freq, z * freq);
            ampSum += amp;
            amp *= 0.5f;
            freq *= 2f;
        }
        return sum / ampSum;
    }

    public static Vector3 FindPathToEnd(Vector3 currentPos, Vector3 endPoint)
    {
        Vector3 direction = PerlinWormDirection(currentPos);
        Vector3 generalDirectionToEnd = (endPoint - currentPos).normalized;
        generalDirectionToEnd = (direction * .4f + generalDirectionToEnd * .6f).normalized;

        return currentPos + generalDirectionToEnd;
    }


    public static float mapValue(float mainValue, float inValueMin, float inValueMax, float outValueMin, float outValueMax)
    {
        return (mainValue - inValueMin) * (outValueMax - outValueMin) / (inValueMax - inValueMin) + outValueMin;
    }
}
