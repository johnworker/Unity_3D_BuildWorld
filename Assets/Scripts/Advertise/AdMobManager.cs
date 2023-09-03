using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobManager : MonoBehaviour
{
    private BannerView bannerView;

    private void Start()
    {
        // 初始化 AdMob SDK
        MobileAds.Initialize(initStatus => { });

        // 創建一個橫幅廣告
        bannerView = new BannerView("YOUR_BANNER_AD_UNIT_ID", AdSize.Banner, AdPosition.Bottom);

        // 載入並顯示橫幅廣告
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
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