using UnityEngine;
using UnityEngine.UI;

public class ShowAdButton : MonoBehaviour
{
    public AdManager adManager;
    public Button showAdButton;

    private void Start()
    {
        showAdButton.onClick.AddListener(ShowAd);
    }

    private void ShowAd()
    {
        adManager.RequestBanner(); // 请求显示广告
    }
}