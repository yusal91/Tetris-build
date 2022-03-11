using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] groups;        //array means that it's a whole bunch of GameObjects, and not just one.


    // Start is called before the first frame update
    void Start()
    {
        // Spawn initial Group
        spawnNext();
    }

    public void spawnNext()
    {
        int i = Random.Range(0, groups.Length);                                // Random Index
        
        //transform.position is the Spawner's position, Quaternion.identity is the default rotation.
        Instantiate(groups[i], transform.position, Quaternion.identity);       // Spawn Group at current Position
    }
}
