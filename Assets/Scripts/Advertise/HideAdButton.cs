using UnityEngine;
using UnityEngine.UI;

public class HideAdButton : MonoBehaviour
{
    public AdManager adManager;
    public Button hideAdButton;

    private void Start()
    {
        hideAdButton.onClick.AddListener(HideAd);
    }

    private void HideAd()
    {
        adManager.DestroyBanner(); // 销毁广告
    }
}