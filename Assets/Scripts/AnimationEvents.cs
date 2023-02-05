using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private AudioClip pasito1;
    [SerializeField] private AudioClip pasito2;
    [SerializeField] private AudioClip salto;
    [SerializeField] private AudioClip aterrizaje;
    
    public void PlayPasito1()
    {
        GameManager.Instance.playerAudio.PlayOneShot(pasito1);
    }
    public void PlayPasito2()
    {
        GameManager.Instance.playerAudio.PlayOneShot(pasito2);
    }
    public void PlaySaltito()
    {
        GameManager.Instance.playerAudio.PlayOneShot(salto);
    }
    public void PlayAterrizaje()
    {
        GameManager.Instance.playerAudio.PlayOneShot(aterrizaje);
    }
}
