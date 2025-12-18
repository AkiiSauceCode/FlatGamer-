using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator wipeoutAnimator;

    public GameObject SettingsMenu;
    public void OnClickPlay()
    {
        Debug.Log("Play button clicked.");
        wipeoutAnimator.SetTrigger("wipeout");
        StartCoroutine(LoadScener());
    }

    public void OnClickQuit()
    {
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
