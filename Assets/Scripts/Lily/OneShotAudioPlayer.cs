using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotAudioPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip mAttackClip;
    public AudioClip mHurtClip;

    public AudioSource mAudioSource;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAttack()
    {
        mAudioSource.PlayOneShot(mAttackClip);
    }

    public void PlayHurt()
    {
        mAudioSource.PlayOneShot(mHurtClip);
    }
}
