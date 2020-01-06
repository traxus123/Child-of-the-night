using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string id;
    [TextArea(1, 6)]
    public string[] noms;
    [TextArea(1, 20)]
    public string[] phrases;
}
