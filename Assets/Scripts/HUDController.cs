using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public PlayerController player;
    public Text life;
    public Text maxLife;
    public Image lifeGauge;
    // Start is called before the first frame update
    void Awake()
    {
        player.OnDamage += Refresh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Refresh()
    {
        life.text = player.life.ToString();
        //maxLife.text = player.maxLife.ToString();
        lifeGauge.rectTransform.sizeDelta = new Vector2(320*((float)player.life/(float)player.maxLife), 32);
    }
}
