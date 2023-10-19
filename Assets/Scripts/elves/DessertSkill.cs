using UnityEngine;
using UnityEngine.UI;


public class DessertSkill : MonoBehaviour
{
    public Image fillImageTwo; // 引用UI Image组件，用于表示能量条的填充状态
    public float fillAmountIncreasePerLayer = 10f; // 每增加一层点心的填充值
    public float maxFillAmount = 100f; // 最大填充值，达到该值后可使用技能

    private GameObject[] desserts; // 存储点心对象的数组

    private void Start()
    {
        // 自动查找场景中所有带有 "點心" 标签的对象
        desserts = GameObject.FindGameObjectsWithTag("點心");
    }

    public void AddDessertLayer()
    {
        // 增加填充值
        fillImageTwo.fillAmount += fillAmountIncreasePerLayer;

        // 如果填充值达到了最大值，触发技能
        if (fillImageTwo.fillAmount >= maxFillAmount)
        {
            UseArrangementSkill();
        }
    }

    public void UseArrangementSkill()
    {
        // 检查是否有点心对象
        if (desserts.Length > 0)
        {
            // 获取最底层点心的位置
            Vector3 referencePosition = desserts[0].transform.position;

            // 重新排列所有点心的位置，使它们的X轴和Z轴与最底层的点心相同
            foreach (GameObject dessert in desserts)
            {
                Vector3 newPosition = new Vector3(referencePosition.x, dessert.transform.position.y, referencePosition.z);
                dessert.transform.position = newPosition;
            }
        }

        // 重置填充值为0
        fillImageTwo.fillAmount = 0f;
    }
}