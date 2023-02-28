using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject Wall;
    public GameObject Road1, Road2;
    private bool WhichPlane;
    private void Start()
    {
        WhichPlane = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Wall")
        {
            Wall.transform.position = new Vector3(Wall.transform.position.x, Wall.transform.position.y, Wall.transform.position.z+20);
            switch (WhichPlane)
            {
                case false:
                    Road1.transform.position = new Vector3(Road1.transform.position.x, Road1.transform.position.y, Road1.transform.position.z + 40);
                    WhichPlane = true;
                    break;
                case true:
                    Road2.transform.position = new Vector3(Road2.transform.position.x, Road2.transform.position.y, Road2.transform.position.z + 40);
                    WhichPlane = false;
                    break;
            }

        }
    }

}
