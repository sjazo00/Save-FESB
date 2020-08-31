using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Dialogue
{
    [TextArea(3,25)]
    public string[] sentences;
    public string name;
}
