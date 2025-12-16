using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject SettingsMenu;
    public void OnClickPlay()
    {

        // play the cutscene or wat
        SceneManager.LoadScene("OverWork");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickSettings()
    {
        SettingsMenu.SetActive(true);
    }

    public void OnClickBackSettings()
    {
        SettingsMenu.SetActive(false);
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
