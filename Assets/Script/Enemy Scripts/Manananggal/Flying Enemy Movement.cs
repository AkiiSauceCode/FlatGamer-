using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] public bool chase = false;
    [SerializeField] private Transform startingPoint;
    
    
    private GameObject player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(player == null) return;
        
        if (chase == true)
        {
             Chase();
              Flip();
        } 
        else
        {
            ReturnToStartPoint();
        }

        
       
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);     
    }
    private void ReturnToStartPoint()
    {
        if (transform.position.x > startingPoint.transform.position.x )
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);

         
    }
    
    private void Flip()
    {
        if (transform.position.x > player.transform.position.x )
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    

    



}
