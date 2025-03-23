using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioSource vfxAudioSource;
    
    public AudioClip musicClip;
    public AudioClip coinClip;
    public AudioClip winClip;
    public AudioClip loseClip;
    public AudioClip jumpClip;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicAudioSource.clip = musicClip;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsGameOver() || GameManager.instance.IsGameWin())
        {
            if (musicAudioSource.isPlaying)
            {
                musicAudioSource.Stop();
            }
        }
        else
        {
            if (!musicAudioSource.isPlaying)
            {
                musicAudioSource.Play();
            }
        }
    }


    public void PlaySFX(AudioClip clip)
    {
        vfxAudioSource.clip = clip;
        vfxAudioSource.PlayOneShot(clip);
    }
    
    public void StopMusic()
    {
        musicAudioSource.Stop();
    }
}
