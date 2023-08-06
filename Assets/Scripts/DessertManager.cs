﻿using UnityEngine;

public class DessertManager : MonoBehaviour
{
    // Transform 可以儲存物件 Transform 元件，可以取得座標、角度、尺寸的資訊
    // Rigidbody 可以儲存物件 Rigidbody 元件，可以取得物理資訊
    // GameObject 可以儲存預置物或場景上的物件
    [Header("懸吊甜點物件")]
    public Transform pointSuspention;
    [Header("晃動位置")]
    public Transform pointShake;
    [Header("晃動位置鋼剛體")]
    public Rigidbody pointShakeRig;
    [Header("甜點預置物陣列")]
    public GameObject[] desserts;
    [Header("晃動力道"), Range(0.5f, 10)]
    public float shakePower = 2f;
    [Header("攝影機")]
    public Transform myCamera;

    /// <summary>
    /// 用來儲存生成的點心物件
    /// </summary>
    private GameObject tempDessert;
    /// <summary>
    /// 開始蓋點心
    /// </summary>
    private bool startDessert;
    /// <summary>
    /// 房子總高度
    /// </summary>
    private float height;

    private void Start()
    {
        // 呼叫生成點心函式
        CreateDessert();
        // 重複調用函式("函式名稱"，調用時間，重複頻率);
        InvokeRepeating("Shake", 0, 3);
    }

    /// <summary>
    /// 建立點心
    /// </summary>
    private void CreateDessert()
    {
        // 儲存生成出來的房子 = 實例化(點心預置物陣列[第一個]， 晃動位置);
        tempDessert = Instantiate(desserts[0], pointShake);
    }

    /// <summary>
    /// 晃動效果
    /// </summary>
    private void Shake()
    {
        // 晃動位置剛體.速度 = 晃動位置.右邊 * 力道
        pointShakeRig.velocity = pointShake.right * shakePower;
    }

    /// <summary>
    /// 蓋點心
    /// </summary>
    public void HouseDown()
    {
        // 暫存點心.變形.設定父物件(無);{功能：脫離晃動位置}
        tempDessert.transform.SetParent(null);
        // 暫存點心.取得元件<剛體>().運動學 = false;{功能：取消運動學，避免卡在空中}
        tempDessert.GetComponent<Rigidbody>().isKinematic = false;
        // 延遲調用函式("函式名稱"，延遲時間);
        Invoke("CreateDessert", 1);
        // 開始蓋點心
        startDessert = true;

        // 點心總高度 遞增指定 暫存點心.取得元件<盒形碰撞器>().尺寸.y * 點心尺寸.y
        // 有些點心有縮放，例如大點心縮小到 0.7 所以實際尺寸為碰撞器 * 尺寸
        height += tempDessert.GetComponent<BoxCollider>().size.y * tempDessert.transform.localScale.y;
    }

    private void Update()
    {
        Track(); 
    }

    /// <summary>
    /// 攝影機追蹤
    /// 懸吊點心物件位移
    /// </summary>
    private void Track()
    {
        // 如果 (開始蓋點心)
        if (startDessert)
        {
            // 攝影機新座標 = (0，點心總高度，-10);
            Vector3 posCam = new Vector3(0, height, -10);
            // 攝影機.座標 = 三維向量.插植(攝影機.座標，攝影機新座標，0.3 * 速度 * 一個影格時間);
            myCamera.position = Vector3.Lerp(myCamera.position, posCam, 0.3f * 10 * Time.deltaTime);

            // 懸掛點心物件新座標 = (0，點心總高度 +6，0);
            Vector3 posSus = new Vector3(0, height + 6, 0);
            // 攝影機.座標 = 三維向量.插植(懸掛點心物件.座標，懸掛點心物件新座標，0.3 * 速度 * 一個影格時間);
            pointSuspention.position = Vector3.Lerp(pointSuspention.position, posSus, 0.3f * 10 * Time.deltaTime);

        }
    }
}
