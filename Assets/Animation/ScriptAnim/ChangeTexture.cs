using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTexture : MonoBehaviour
{
    public Texture2D texture;
    Renderer rend;
    int textureID = 0;
    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        int newTextureID = texture.GetInstanceID();
        if(newTextureID != textureID)
        {
            rend.material.SetTexture("_BaseMap", texture);
            textureID = newTextureID;
        }

        
    }
}
