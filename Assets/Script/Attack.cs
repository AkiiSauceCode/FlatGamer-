using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator anim;

    public float cooldown = 2;
    private float timer;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }


    public void attack() 
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true);
            timer = cooldown;
        }
    }

    public void finishattacking() 
    {
        anim.SetBool("isAttacking", false);
    }
}
