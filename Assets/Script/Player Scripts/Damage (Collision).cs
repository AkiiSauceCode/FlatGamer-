using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = 10;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
    }
}
