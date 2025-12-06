using UnityEngine;

public class EnemyThrow : MonoBehaviour
{
    public GameObject bottle;
    public Transform bottlePos;

    private float timer;
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);

        if (distance < 4) 
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer = 0;
                throwbot();
            }
        }
    }

    void throwbot() 
    {
        Instantiate(bottle, bottlePos.position, Quaternion.identity);
    }
}
