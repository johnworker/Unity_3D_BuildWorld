using UnityEngine;
using UnityEngine.UI;       // 引用介面API
using TMPro;
using System.Collections;

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
    [Header("檢查遊戲失敗牆壁")]
    public Transform checkWall;
    [Header("結算畫面")]
    public GameObject final;
    [Header("蓋點心數量文字介面")]
    public TextMeshProUGUI textDessertCount;
    [Header("最佳數量文字介面")]
    public TextMeshProUGUI textBest;
    [Header("本次數量文字介面")]
    public TextMeshProUGUI textCurrent;
    [Header("生成點心音效")]
    public AudioClip soundCreateDessert;
    [Header("結束音效")]
    public AudioClip soundEnd;
    [Header("蓋點心音樂")]
    public AudioClip soundBGMStart;
    [Header("遊戲結束音樂")]
    public AudioClip soundBGMGameOver;

    [Header("動畫控制器")]
    //public Animator ani;

    [Header("張開參數")]
    //public string parOpen = "張開";

    /// <summary>
    /// 用來儲存生成的點心物件
    /// </summary>
    public GameObject tempDessert;
    /// <summary>
    /// 開始蓋點心
    /// </summary>
    private bool startDessert;
    /// <summary>
    /// 房子總高度
    /// </summary>
    private float height;
    /// <summary>
    /// 第一個點心
    /// </summary>
    private Transform firstDessert;
    /// <summary>
    /// 點心總數
    /// </summary>
    public int count;

    /// <summary>
    /// 音效管理器
    /// </summary>
    private SoundManager soundManager;

    /// <summary>
    /// 遊戲結束
    /// </summary>
    private bool gameOver;

    // 用於引用 SpriteCollector 腳本的變量
    public SpriteCollector spriteCollector;

    public ObstacleSpawner obstacleSpawner;

    public IceSpriteSkill iceSprite;


    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        soundManager.PlayBGM(soundBGMStart, true);
        // 呼叫生成點心函式
        CreateDessert();
        // 重複調用函式("函式名稱"，調用時間，重複頻率);
        InvokeRepeating("Shake", 0, 3);

        // 獲取 SpriteCollector 腳本組件
        spriteCollector = GetComponent<SpriteCollector>();


    }

    private void Update()
    {
        Track();

    }


    /// <summary>
    /// 建立點心
    /// </summary>
    private void CreateDessert()
    {
        // 如果超過 16 層，隨機選擇 desserts[] 中的一個預製物
        if (count >= 16)
        {
            int randomIndex = Random.Range(0, desserts.Length);
            tempDessert = Instantiate(desserts[randomIndex], pointShake);

            Renderer dessertRenderer = tempDessert.GetComponent<Renderer>();
            if (dessertRenderer != null)
            {
                float randomHue = Random.Range(0f, 1f); // 隨機色相
                Color newColor = Color.HSVToRGB(randomHue, 1f, 1f);
                dessertRenderer.material.color = newColor;
            }

        }
        else
        {
            // 儲存生成出來的點心 = 實例化(點心預置物陣列[第一個]， 晃動位置);
            int prefabIndex = 0;
            if (count < 5)
            { 
                prefabIndex = 0;
                //tempDessert = Instantiate(desserts[0], pointShake); 
            }
            else if (count < 8)
            { 
                prefabIndex = 1;
                //tempDessert = Instantiate(desserts[1], pointShake);
            }
            else if (count < 12)
            {
                prefabIndex = 2;
                //tempDessert = Instantiate(desserts[2], pointShake);
            }
            else
            {
                prefabIndex = 3;
                //tempDessert = Instantiate(desserts[3], pointShake);
            }
            tempDessert = Instantiate(desserts[prefabIndex], pointShake);

            Renderer dessertRenderer = tempDessert.GetComponent<Renderer>();
            if (dessertRenderer != null)
            {
                float randomHue = Random.Range(0f, 1f);
                Color newColor = Color.HSVToRGB(randomHue, 1f, 1f);
                dessertRenderer.material.color = newColor;
            }

            soundManager.PlaySound(soundCreateDessert);

        }
    }


    /// <summary>
    /// 晃動效果
    /// </summary>
    private void Shake()
    {
        // 如果已經達到了第 10 層，將 shakePower 設置為 3，否則保持原值
        if (count >= 20)
        {
            shakePower = 3f;
        }

        // 晃動位置剛體.速度 = 晃動位置.右邊 * 力道
        pointShakeRig.velocity = pointShake.right * shakePower;
    }

    /// <summary>
    /// 蓋點心
    /// </summary>
    public void DessertDown()
    {
        // 如果 遊戲結束 或者 目前沒有點心 跳出
        if (gameOver || !tempDessert) return;

        // 暫存點心.變形.設定父物件(無);{功能：脫離晃動位置}
        tempDessert.transform.SetParent(null);
        // 暫存點心.取得元件<剛體>().運動學 = false;{功能：取消運動學，避免卡在空中}
        tempDessert.GetComponent<Rigidbody>().isKinematic = false;

        // 播放动画
        //ani.SetBool(parOpen, true);

        // 暫存點心.取得元件<點心>().是否降落中 = true;{功能：確定房子降落中}
        tempDessert.GetComponent<Dessert>().down = true;
        // 延遲調用函式("函式名稱"，延遲時間);
        Invoke("CreateDessert", 1);
        // 開始蓋點心
        startDessert = true;

        // 點心總高度 遞增指定 暫存點心.取得元件<盒形碰撞器>().尺寸.y * 點心尺寸.y
        // 有些點心有縮放，例如大點心縮小到 0.7 所以實際尺寸為碰撞器 * 尺寸
        height += tempDessert.GetComponent<BoxCollider>().size.y * tempDessert.transform.localScale.y;

        // 如果還沒有第一個點心
        if (!firstDessert)
        {
            // 第一個點心 = 暫存點心.變形
            firstDessert = tempDessert.transform;
            // 延遲調用函式("建立遊戲檢查失敗牆壁"，1.2秒)
            Invoke("CreateCheckWall", 1.2f);
            // 刪除(第一個點心.取得元件<點心>())
            Destroy(firstDessert.GetComponent<Dessert>());
        }


        // 點心總數遞增
        count++;


        // 蓋點心數量文字介面.文字 = "點心數量：" + 點心總數;
        textDessertCount.text = "點心數量：" + count;

        // 調用 SpriteCollector 腳本中的 CollectEnergy 方法，增加能量
        spriteCollector.CollectEnergy();


        // 目前沒有點心
        tempDessert = null;

    }



    /// <summary>
    /// 懸吊點心物件往上位移
    /// 攝影機追蹤目前高度
    /// </summary>
    private void Track()
    {
        // 如果 (開始蓋點心)
        if (startDessert)
        {
            // 攝影機新座標 = (0，點心總高度，-10);
            Vector3 posCam = new Vector3(0, height + 1f, -10);
            // 攝影機.座標 = 三維向量.插植(攝影機.座標，攝影機新座標，0.3 * 速度 * 一個影格時間);
            myCamera.position = Vector3.Lerp(myCamera.position, posCam, 0.3f * 10 * Time.deltaTime);

            // 懸掛點心物件新座標 = (0，點心總高度 +6，0);
            Vector3 posSus = new Vector3(0, height + 6, 0);
            // 攝影機.座標 = 三維向量.插植(懸掛點心物件.座標，懸掛點心物件新座標，0.3 * 速度 * 一個影格時間);
            pointSuspention.position = Vector3.Lerp(pointSuspention.position, posSus, 0.3f * 10 * Time.deltaTime);

        }

    }

    /// <summary>
    /// 建立檢查遊戲失敗牆壁
    /// </summary>
    private void CreateCheckWall()
    {
        // 生成(檢查遊戲失敗牆壁，第一個點心.座標，零角度)
        Instantiate(checkWall, firstDessert.position, Quaternion.identity);
    }

    /// <summary>
    /// 遊戲結束：顯示結算畫面
    /// </summary>
    public void GameOver()
    {

        // 如果 遊戲結束 跳出
        if (gameOver) return;

        soundManager.PlaySound(soundEnd);

        // 遊戲結束
        gameOver = true;

        // 結算畫面.啟動設定(顯示)
        final.SetActive(true);

        // 本次數量文字介面.文字 = "本次數量：" + 點心總數
        textCurrent.text = "本次數量：" + count;

        // 如果 點心總數 > 玩家參考.取得整數("蓋點心最佳數量")
        if (count > PlayerPrefs.GetInt("最佳數量"))
        {
            // 玩家參考.設定整數("蓋點心最佳數量"，點心總數)
            PlayerPrefs.SetInt("最佳數量", count);
        }
        // 最佳數量文字介面.文字 = "最佳數量：" + 玩家參考.取得整數("蓋點心最佳數量")
        textBest.text = "最佳數量：" + PlayerPrefs.GetInt("最佳數量");
        soundManager.PlayBGM(soundBGMGameOver, false);

    }
}
