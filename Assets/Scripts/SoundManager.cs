using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }
}
