using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    [Header("載入畫面")]
    public GameObject loadingPanel;
    [Header("載入進度：文字")]
    public TextMeshProUGUI textLoading;
    [Header("載入進度：圖片")]
    public Image imageLoading;
    [Header("提示文字")]
    public GameObject tip;
    [Header("載入圖片：精靈")]
    public Image imageElf;

    public float minScale = 1.2f;  // 最小縮放值
    public float maxScale = 2.5f;  // 最大縮放值
    public float scaleSpeed = 1.0f;  // 縮放速度

    private bool scalingUp = true;  // 是否正在放大

    /// <summary>
    /// 載入場景
    /// </summary>
    /// <returns></returns>
    private IEnumerator Loading()
    {
        loadingPanel.SetActive(true);

        AsyncOperation ao = SceneManager.LoadSceneAsync("鋪點心");
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            textLoading.text = (ao.progress / 0.9f * 100).ToString("F0") + " %";

            imageLoading.fillAmount = ao.progress / 0.9f;

            yield return null;

            if(ao.progress >= 0.9f)
            {
                tip.SetActive(true);

                if (Input.anyKey) ao.allowSceneActivation = true;
            }
        }
    }

    public void StartLoading()
    {
        StartCoroutine(Loading());
    }

    private void Update()
    {
        // 實現圖片大小忽大忽小的效果
        float scaleFactor = scalingUp ? scaleSpeed * Time.deltaTime : -scaleSpeed * Time.deltaTime;
        imageElf.transform.localScale += new Vector3(scaleFactor, scaleFactor, scaleFactor);

        // 檢查是否需要改變方向
        if (imageElf.transform.localScale.x < minScale || imageElf.transform.localScale.x > maxScale)
        {
            scalingUp = !scalingUp;
        }
    }


}
