using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset = Vector3.zero;
    public float smoothTime = 0.1f;
    public Animator transition;
    public GameObject transitionObj;

    private Vector3 velocity = Vector3.zero;
    private Transform target;
    
    void Start()
    {
        transitionObj.SetActive(true);
        transition.SetTrigger("wipeoutleft");
    }

    void LateUpdate()
    {
        FindActivePlayer();

        if (target == null) return;

        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );
    }

    void FindActivePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
            target = player.transform;
    }
}
