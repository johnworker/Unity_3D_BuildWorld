using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class FireSpriteSkill : MonoBehaviour
{
    public FireSpriteSkill fireSpriteSkill; // 引用火精灵技能

    public DessertManager dessertManager; // 引用 DessertManager 脚本

    public ObstacleSpawner obstacleSpawner;

    /// <summary>
    /// 音效管理器
    /// </summary>
    private SoundManager soundManager;

    [Header("粉碎特效")]
    public GameObject crushEffect;

    [Header("爆炸音效")]
    public AudioClip soundExplode;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void DestroyEffect()
    {
        GameObject effectIns = Instantiate(crushEffect, transform.position, Quaternion.identity);
        Destroy(effectIns, 5f);
    }

    // 在某个事件或条件下调用此方法来触发销毁点心克隆的操作
    public void UseFireSkill()
    {
        // 呼叫火精靈技能的方法來銷毀隨機的點心克隆，傳遞要銷毀的數量（這裡是1）
        // fireSpriteSkill.BurnRandomDesserts(1);

        // 呼叫 ObstacleSpawner 的銷毀障礙物方法
        obstacleSpawner.DestroyObstacles();
        DestroyEffect();
        soundManager.PlaySound(soundExplode);
    }

    public void BurnRandomDesserts(int numberOfDessertsToBurn)
    {
        // 获取场景中所有的点心克隆
        GameObject[] desserts = GameObject.FindGameObjectsWithTag("點心");

        // 创建一个列表来存储要销毁的点心克隆
        List<GameObject> dessertsToBurn = new List<GameObject>();

        // 按点心的高度降序排序（从高到低）
        desserts = desserts.OrderByDescending(dessert => dessert.transform.position.y).ToArray();

        // 获取下方往上数第2个点心的索引
        int index = Mathf.Min(1, desserts.Length - 1);

        // 遍历点心列表并添加到销毁列表中
        for (int i = index; i < desserts.Length; i++)
        {
            // 判断点心是否是“晃动位置”，如果是则跳过
            if (desserts[i].name == "晃动位置")
            {
                continue;
            }

            dessertsToBurn.Add(desserts[i]);

            // 如果已经收集到足够的点心要销毁，跳出循环
            if (dessertsToBurn.Count >= numberOfDessertsToBurn)
            {
                break;
            }
        }

        // 销毁选定的点心克隆
        foreach (GameObject dessert in dessertsToBurn)
        {
            Destroy(dessert);
        }

    }
}
