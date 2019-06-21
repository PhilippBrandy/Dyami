using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame 
{
    public string Date { get; set; }
    public Vector3 SavePosition { get; set; }
    public string CurrentScene { get; set; }
    public int ItemsCollected { get; set; }
    public int ItemsToCollect { get; set; }
}
