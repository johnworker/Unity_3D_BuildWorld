using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SpriteCollectorTwo : MonoBehaviour
{
    public Image fillImage; // 引用UI Image组件，用于表示能量條的填充状态

    private int maxEnergy = 100; // 最大能量值
    private int currentEnergy = 0; // 當前能量值

    public int increaseEnergy = 0; // 增加的能量值

    public SpriteSkill selectedSkill; // 儲存目前選擇的精靈技能
    private float cooldownTimer = 0; // 記錄技能冷卻時間的計時器

    private bool skillUsed = false; // 用於標記技能是否被使用過

    public Button skillButton; // 技能按钮

    // 定義使用技能的事件
    public UnityEvent onUseSkill = new UnityEvent();


    private void Start()
    {
        // 初始化能量條
        currentEnergy = 0;
        UpdateEnergyBar();

        // 添加按钮点击事件监听器
        skillButton.onClick.AddListener(UseSpriteSkill);

    }

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // 检查 FillAmount 是否满了
        if (fillImage.fillAmount >= 1f)
        {
            // 满足条件，可以使用技能，按钮可用
            skillButton.interactable = true;
        }
        else
        {
            // 不满足条件，不能使用技能，按钮禁用
            skillButton.interactable = false;
        }


    }

    private void UseSpriteSkill()
    {
        // 检查 FillAmount 是否满了
        if (fillImage.fillAmount >= 1f)
        {
            // 标记技能已使用
            skillUsed = true;

            // 触发使用技能的事件
            onUseSkill.Invoke();

            // 还可以重置 FillAmount 为 0，表示技能已经使用过了
            fillImage.fillAmount = 0f;
        }
    }


    // 更新UI能量條的填充状态
    private void UpdateEnergyBar()
    {
        // 计算填充百分比
        float fillAmount = (float)currentEnergy / maxEnergy;
        // 更新UI Image组件的fillAmount属性以反映填充状态
        fillImage.fillAmount = fillAmount;
    }

    // 在每次增加一层點心时调用此方法
    public void CollectEnergy()
    {
        currentEnergy += increaseEnergy; // 增加能量

        // 确保当前能量不超过最大值
        currentEnergy = Mathf.Min(currentEnergy, maxEnergy);

        // 更新UI能量條
        UpdateEnergyBar();

        // 在这里检查是否使用了技能，如果使用了技能，将 currentEnergy 重置为 0，并将技能状态标记为未使用
        if (skillUsed)
        {
            currentEnergy = 0;
            skillUsed = false;
            UpdateEnergyBar(); // 更新UI能量條以显示重置后的值
        }
    }
}
