using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    string checkpointsReached = "noCheckpoint";

    public void AddReachedCheckpoint(string nameOfCheckpoint)
    {
        checkpointsReached += nameOfCheckpoint;
    }

    public string GetNamesOfReachedCheckpoints()
    {
        return checkpointsReached;
    }
}
