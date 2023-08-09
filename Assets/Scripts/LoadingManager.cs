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
}
