using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [Tooltip("The camera you want to switch to")]
    public CinemachineVirtualCamera newCamera;
    [Tooltip("Specify an old camera if you want to deactivate it")]
    public CinemachineVirtualCamera oldCamera;
    [Header("Player Controller during transition")]
    public bool active = false;

    [Header("Fading Effect")]
    public bool darkFade = false;
    public CinemachineVirtualCamera fadeCamera;
    
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
        if (other.CompareTag("Player") && !CinemachineCore.Instance.IsLive(newCamera))
        {
            PlayerController p = other.GetComponent<PlayerController>();
            p.active = active;

            if (oldCamera != null)
            {
                oldCamera.gameObject.SetActive(false);
            }

            if (darkFade && fadeCamera)
            {
                fadeCamera.gameObject.SetActive(true);
                fadeCamera.MoveToTopOfPrioritySubqueue();
                StartCoroutine(SwitchCam(1.0f, p, true));
            }
            else
            {
                StartCoroutine(SwitchCam(0f, p));
            }
        }
    }

    IEnumerator SwitchCam(float waitTime, PlayerController player, bool fade = false)
    {
        yield return new WaitForSeconds(waitTime);
        if (fade) fadeCamera.gameObject.SetActive(false);
        newCamera.gameObject.SetActive(true);
        newCamera.MoveToTopOfPrioritySubqueue();
        yield return new WaitForSeconds(1f); // Temps de transition mis en dur tant que pas de transition custom en cinemachine avec cette méthode
        player.active = true;
    }
}
