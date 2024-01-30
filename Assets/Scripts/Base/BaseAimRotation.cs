using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAimRotation : AimRotation
{
    protected override void RotateArm(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        armRenderer.flipY = Mathf.Abs(rotZ) > 90f;
        charRenderer.flipX = armRenderer.flipY;

        if (-90 <= rotZ && rotZ < 0)
            rotZ = 0;
        else if (-180 < rotZ && rotZ < -90)
            rotZ = 180;

        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
