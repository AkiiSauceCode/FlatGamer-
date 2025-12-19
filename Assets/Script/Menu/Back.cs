using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void OnClickBack()
    {
        SceneManager.LoadScene("StartMenu");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
