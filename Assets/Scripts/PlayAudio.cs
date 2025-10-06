using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundEffect : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private List<Music> soundEffects = new List<Music>();

    private AudioSource audioSource;
    private int lastStateHash;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true; 
        lastStateHash = 0;
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        int currentHash = stateInfo.shortNameHash;

        if (currentHash != lastStateHash)
        {
            lastStateHash = currentHash;
            SwitchSound(stateInfo);
        }


        if (!audioSource.isPlaying && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    private void SwitchSound(AnimatorStateInfo stateInfo)
    {
        foreach (var sfx in soundEffects)
        {
            if (stateInfo.IsName(sfx.animationName))
            {
                audioSource.clip = sfx.audio;
                audioSource.Play(); 
                return;
            }
        }

        audioSource.Stop();
        audioSource.clip = null;
    }
}
