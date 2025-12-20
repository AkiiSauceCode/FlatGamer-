using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CloseWindow : MonoBehaviour
{

    public Animator TutorialOpen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickNext()
    {
        StartCoroutine(TutorialEnd());
    }

    IEnumerator TutorialEnd()
    {
        TutorialOpen.SetTrigger("Close");
        yield return new WaitForSeconds(0.59f);
        SceneManager.LoadScene("Lvl1");
    }

}
