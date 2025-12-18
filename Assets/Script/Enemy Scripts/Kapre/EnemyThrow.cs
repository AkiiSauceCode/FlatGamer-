using UnityEngine;
using System.Collections;

public class EnemyThrow : MonoBehaviour
{
    public GameObject bottle;
    public Transform bottlePos;
    public Animator anim;

    public Transform playerTransform;
    private float timer;
    private GameObject player;

    public float waitduration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > playerTransform.position.x)
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }
        if (transform.position.x < playerTransform.position.x)
        {
            transform.localScale = new Vector3(2, 2, 2);
        }

            float distance = Vector2.Distance(transform.position, player.transform.position);
        



        if (distance < 10) 
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                StartCoroutine(cooldown());
                timer = 0;
            }
        }
        else
        {
            anim.SetBool("isThrowing", false);
        }
    }

    void throwbot() 
    {
        Instantiate(bottle, bottlePos.position, Quaternion.identity);
    }

    IEnumerator cooldown()
    {
        anim.SetBool("isThrowing", true);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("isThrowing", false);
        yield return new WaitForSeconds(waitduration);
    }
}
