using UnityEngine;

public class DessertSound : MonoBehaviour
{
    [Header("點心碰撞音效音效")]
    public AudioClip soundHit;

    /// <summary>
    /// 點心管理器
    /// </summary>
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "點心" || collision.gameObject.name == "地板")
        {
            soundManager.PlaySound(soundHit);
        }
    }

}
