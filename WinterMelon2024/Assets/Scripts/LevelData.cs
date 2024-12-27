using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "New Data Object/ New Level Data")]
public class LevelData : ScriptableObject
{
    public string levelName;
    [TextArea(5, 7)] public string levelDescription;
    public Sprite levelArt;
}
