using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkCollider : MonoBehaviour
{
    void Update()
    {
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Ray ray = new Ray(transform.position, fwd);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10))
            {
                if (hit.collider.name == "Wall")
                {
                    Debug.Log("Wall");
                }
            }
        }

    }
}
