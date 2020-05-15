using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPrefabs : MonoBehaviour
{
    static public TestPrefabs instance;
    public GameObject startPrefab;
    public GameObject endPrefab;

    public Transform startRoot;
    public Transform endRoot;

    private void Awake()
    {
        instance = this;
    }

    public void MakeStart(Vector3 position)
    {
        GameObject clone = Instantiate(startPrefab, position, Quaternion.identity, startRoot);
        clone.name = "Start";
    }

    public void MakeEnd(Vector3 position)
    {
        GameObject clone = Instantiate(endPrefab, position, Quaternion.identity, endRoot);
        clone.name = "End";
    }
}
