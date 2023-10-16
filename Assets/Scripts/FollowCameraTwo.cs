using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraTwo : MonoBehaviour
{
    public Transform target; // Reference to the target (scene object) to follow

    // 设置跟随目标
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Update()
    {
        if (target != null)
        {
            // 更新相机位置以跟随目标
            transform.position = new Vector3(4f,1f + target.position.y, transform.position.z);
        }
    }
}
