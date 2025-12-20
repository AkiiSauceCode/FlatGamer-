using System.Collections;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public AudioSource ButtonClick;
    public GameObject SettingsMenu;

    public void OnClickClose()
    {
        ButtonClick.Play();
        StartCoroutine(CloseWindow());
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CloseWindow()
    {
        yield return new WaitForSeconds(0.50f);
        SettingsMenu.SetActive(false);
    }
}
