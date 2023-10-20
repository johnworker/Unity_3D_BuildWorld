using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSound : MonoBehaviour
{
    [Header("撞擊音效")]
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
        if (collision.gameObject.tag == "點心" || collision.gameObject.name == "巧克力球")
        {
            soundManager.PlaySound(soundHit);
        }
    }

}
