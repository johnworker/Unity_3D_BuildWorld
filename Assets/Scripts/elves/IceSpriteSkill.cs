using UnityEngine;
using System.Collections;
using System.Linq;

[System.Serializable]
public class IceSpriteSkill : MonoBehaviour
{
    public float freezeDuration; // 冰精灵特有属性：冰冻持续时间
    private bool isFrozen = false; // 是否冻结中
    public IceSpriteSkill iceSprite;

    public DessertManager dessertManager; // 引用 DessertManager 脚本


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

    public void Arrangement()
    {
        // 获取场景中所有的点心克隆
        GameObject[] desserts = GameObject.FindGameObjectsWithTag("點心");

        if (desserts.Length > 0)
        {
            // 按点心的高度升序排序（从低到高）
            desserts = desserts.OrderBy(dessert => dessert.transform.position.y).ToArray();

            // 获取最底部的点心
            Transform bottomDessert = desserts[0].transform;

            // 获取最底部点心的X轴和Z轴位置
            Vector3 bottomPosition = new Vector3(bottomDessert.position.x, bottomDessert.position.y, bottomDessert.position.z);

            // 遍历所有点心并将它们的X轴和Z轴位置设置为与最底部点心一致
            foreach (GameObject dessert in desserts)
            {
                dessert.transform.position = new Vector3(bottomPosition.x, dessert.transform.position.y, bottomPosition.z);
            }
        }
    }

}
