using UnityEngine;
using SpeechIO;
public class SoundEffects : MonoBehaviour
{
    public AudioClip collisionClipPaddle;
    public AudioClip collisionClipWall;
    private AudioSource audioSource;
    public SpeechOut speechOut;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        speechOut = new SpeechOut();
    }
    public void PlayCollisionPaddle()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(collisionClipPaddle);
        // return gameOverClip.length;
    }
    public void PlayCollisionWall()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(collisionClipWall);
    }
}

