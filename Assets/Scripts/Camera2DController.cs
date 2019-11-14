using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DController : MonoBehaviour
{
    public GameObject target;
    public Vector2 velocity;
    public float lookAhead = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per physics tick
    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, target.transform.position.x + target.GetComponent<PlayerController>().rotation * lookAhead, ref velocity.x, 0.2f), transform.position.y, transform.position.z);
    }
}
