using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "New Scenario", order = 1)]

public class ScenarioData : ScriptableObject
{
    public Vector3[] FirstWalls;


    public string prefabName;

    public int numberOfPrefabsToCreate;
   
}
