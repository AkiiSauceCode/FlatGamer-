using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public AudioSource backgroundMusic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundMusic.Play();
        backgroundMusic.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
