using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    public float cooldown = 1f;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        Collider other = collision.collider;
        if (other.tag == "Wall")
        {
            if (Time.time - startTime > cooldown) 
            { 
                Vector3 pos = transform.position;
                transform.position = new Vector3(-pos.x, pos.y, -pos.z);
                startTime = Time.time;
            }

        }
    }
}
