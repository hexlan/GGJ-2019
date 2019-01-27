using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelOneWisp : MonoBehaviour
{

    private Vector3[] positions =
    {
        new Vector3(-13, 1, -34),
        new Vector3(-35, 1, -45)
    };
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void moveWisp()
    {
        GetComponent<NavMeshAgent>().destination = positions[index];
        index++;
    }
}
