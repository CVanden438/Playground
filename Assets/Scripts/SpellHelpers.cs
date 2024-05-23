using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpellHelpers
{
    public static Vector3 ClampMaxRange(Vector3 startPos, Vector3 targetPos, float range)
    {
        Vector3 direction = targetPos - startPos;

        // Check if the distance to the target position exceeds the maximum range
        if (direction.magnitude > range)
        {
            // Normalize the direction and multiply it by the maximum range
            direction = direction.normalized * range;

            // Calculate the new target position at the maximum range
            targetPos = startPos + direction;
        }
        return targetPos;
    }
}
