using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EFAudio : MonoBehaviour
{
    private AudioSource _audioSource;

    public void Init(AudioClip clip)
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(clip);
    }
    
    void Update()
    {
        if (_audioSource.isPlaying==false)
        {
            PoolManager.Instance.PushObj(GameManager.Instance.GameConf.EFAudio,gameObject);
        }
    }
}
