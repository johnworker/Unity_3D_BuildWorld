using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // 障礙物的預置物
    public float defaultMoveSpeed = 2.0f; // 預設障礙物的移動速度
    public float currentMoveSpeed; // 當前障礙物的移動速度

    public UnityEvent onSpeedChange; // 定義事件

    
    public float rotationSpeed = 90.0f; // 旋轉速度

    private bool speedChangeScheduled = false;

    private Coroutine speedChangeCoroutine;

    void Start()
    {
        currentMoveSpeed = defaultMoveSpeed;

        // 呼叫CreateObstacle函數，每隔8秒重複一次，一開始等待1秒
        InvokeRepeating("CreateObstacle", 1.0f, 8.0f);

        // 在 120 秒後觸發速度更改
        StartCoroutine(ChangeSpeedAfterDelay(120.0f, 3.0f));

        StartCoroutine(ChangeSpeedAfterDelay(120.0f, 4.0f));

    }

    public void CreateObstacle()
    {
        // 在X軸上生成障礙物
        GameObject obstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);

        obstacle.transform.eulerAngles = new Vector3(-90, 180, 0);

        // 設置障礙物的父物體為ObstacleSpawner，以便跟隨它的移動
        obstacle.transform.parent = transform;

        // 啟動一個協程，使障礙物進行順時針旋轉
        StartCoroutine(RotateObstacle(obstacle));

        // 設定障礙物的移動速度
        Rigidbody obstacleRigidbody = obstacle.GetComponent<Rigidbody>();
        if (obstacleRigidbody != null)
        {
            obstacleRigidbody.velocity = new Vector2(-currentMoveSpeed, 0f);
        }

        // 適當地銷毀障礙物，以避免記憶體洩漏
        Destroy(obstacle, 10.0f); // 這裡的10.0f是一個合適的時間，您可以根據需要調整
    }

    public void ChangeSpeed(float newSpeed)
    {
        currentMoveSpeed = newSpeed;
        // 觸發事件，通知其他腳本速度已更改
        onSpeedChange.Invoke();
    }

    private IEnumerator RotateObstacle(GameObject obstacle)
    {
        while (obstacle != null)
        {
            // 讓障礙物繞Z軸進行順時針旋轉
            obstacle.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // 新的方法，用於銷毀生成的障礙物
    public void DestroyObstacles()
    {
        // 取得場景中所有的障礙物
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("障礙物");

        // 遍歷障礙物清單並銷毀它們
        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
    }

    // 在指定延迟后更改速度
    private IEnumerator ChangeSpeedAfterDelay(float delay, float newSpeed)
    {
        yield return new WaitForSeconds(delay);

        // 更改速度
        ChangeSpeed(newSpeed);
    }

}
