using UnityEngine;
using System.Collections;

[System.Serializable]
public class IceSpriteSkill : MonoBehaviour
{
    public float freezeDuration; // 冰精灵特有属性：冰冻持续时间
    private bool isFrozen = false; // 是否冻结中

    public void UseIceSkill()
    {
        // 水精灵效果：冰冻所有点心
        if (!isFrozen)
        {
            // 获取场景中所有点心
            GameObject[] desserts = GameObject.FindGameObjectsWithTag("點心");

            // 冻结所有点心
            foreach (GameObject dessert in desserts)
            {
                // 设置点心的刚体为运动学，使其不再受物理影响
                Rigidbody rb = dessert.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
            }

            // 开始计时冰冻持续时间
            StartCoroutine(UnfreezeAfterDuration());
        }
    }

    private IEnumerator UnfreezeAfterDuration()
    {
        // 等待指定的冰冻持续时间后解冻点心
        yield return new WaitForSeconds(freezeDuration);

        // 获取场景中所有点心
        GameObject[] desserts = GameObject.FindGameObjectsWithTag("點心");

        // 解冻所有点心
        foreach (GameObject dessert in desserts)
        {
            // 恢复点心的刚体运动学状态
            Rigidbody rb = dessert.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }

        // 标记为不再冻结状态
        isFrozen = false;
    }
}
