using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public float switchCooldown = 3f; // default 3 seconds
    private float switchTimer = 0f;

    private GameObject activePlayer;

    void Start()
    {
        // Start with player1 active
        player1.SetActive(true);
        player2.SetActive(false);
        activePlayer = player1;
    }

    void Update()
    {
        // Reduce cooldown
        if (switchTimer > 0f)
            switchTimer -= Time.deltaTime;

        // Check for switch input and cooldown
        if (Input.GetKeyDown(KeyCode.K) && switchTimer <= 0f)
        {
            SwitchPlayer();
        }
    }

    void SwitchPlayer()
    {
        player1.SetActive(false);
        player2.SetActive(true);

        player2.transform.position = player1.transform.position;

        // Reset cooldown
        switchTimer = switchCooldown;

        Debug.Log("Player switched! Next switch available in " + switchCooldown + " seconds.");
    }
}
