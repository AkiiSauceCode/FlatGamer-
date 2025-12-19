using UnityEngine;

public class ReloadBawang : MonoBehaviour
{
    public GameObject bawangPrefab;
    public Transform spawnPoint;
    

    void SpawnBawang()
    {
        Instantiate(bawangPrefab, spawnPoint.position, spawnPoint.rotation);
    }


}
