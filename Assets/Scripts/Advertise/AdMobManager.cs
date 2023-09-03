using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobManager : MonoBehaviour
{
    private string bannerAdUnitId = "ca-app-pub-9310459943689922/9612758068";
    private BannerView bannerView;

    private void Start()
    {
        // 初始化 AdMob SDK
        MobileAds.Initialize(initStatus => { });

        // 創建一個橫幅廣告
        bannerView = new BannerView(bannerAdUnitId, AdSize.Banner, AdPosition.Bottom);

        // 載入並顯示橫幅廣告
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);

        bannerView.Show();
    }

    private void OnDestroy()
    {
        // 在銷毀時釋放橫幅廣告
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }
}