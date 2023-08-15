using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    private BannerView bannerView;

    private void Start()
    {
        string appId = "YOUR_APP_ID"; // 在AdMob上获得的应用ID
        MobileAds.Initialize(initStatus => { });
    }

    public void RequestBanner()
    {
        string adUnitId = "YOUR_BANNER_AD_UNIT_ID"; // 在AdMob上获得的横幅广告单元ID
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }

    public void DestroyBanner()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }
}