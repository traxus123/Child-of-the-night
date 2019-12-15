using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public CinemachineVirtualCamera oldCamera;
    public CinemachineVirtualCamera newCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRANSITION");
        if (other.CompareTag("Player"))
        {
            if (oldCamera == null)
            {
                newCamera.MoveToTopOfPrioritySubqueue();
                newCamera.gameObject.SetActive(true);
            }
            else
            {
                oldCamera.gameObject.SetActive(false);
                newCamera.gameObject.SetActive(true);
            }
        }
    }
}
