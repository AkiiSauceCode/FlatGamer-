using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Animator wipeoutAnimator;

    public GameObject SettingsMenu;
    public AudioSource ButtonClick;

    public void OnClickPlay()
    {
        ButtonClick.Play();
        wipeoutAnimator.SetTrigger("wipeout");
        StartCoroutine(LoadScener());
    }

    public void OnClickNames()
    {
        ButtonClick.Play();
        SettingsMenu.SetActive(true);
    }

    public void OnClickQuit()
    {
        ButtonClick.Play();
        Application.Quit();
    }
        IEnumerator LoadScener()
    {
        yield return new WaitForSeconds(0.999f);
        SceneManager.LoadScene("Cutscene");
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SettingsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
