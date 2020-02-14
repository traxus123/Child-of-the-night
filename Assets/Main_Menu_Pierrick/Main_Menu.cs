using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    // Start is called before the first frame update

    public void Play_Pressed() 
    {
        SceneManager.LoadScene("SampleScene");//CHANGER LE NOM DE LA SCENE A CHARGER
    }

    public void Quit_Pressed()
    {
        Application.Quit();
    }
}
