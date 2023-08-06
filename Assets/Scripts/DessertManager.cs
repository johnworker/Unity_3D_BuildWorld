using UnityEngine;

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

    /// <summary>
    /// 用來儲存生成的點心物件
    /// </summary>
    private GameObject tempDessert;

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
}
