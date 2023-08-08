using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// 音效來源：喇叭
    /// </summary>
    private AudioSource aud;

    private void Start()
    {
        // 音效來源 = 取得元件<音效來源>();
        aud = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="sound">想要播放的音效</param>
    public void PlaySound(AudioClip sound)
    {
        // 音效來源.播放一次(音效);
        aud.PlayOneShot(sound);
    }
}
