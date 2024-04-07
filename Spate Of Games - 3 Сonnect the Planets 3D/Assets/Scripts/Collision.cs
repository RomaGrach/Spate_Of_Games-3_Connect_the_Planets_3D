using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Collision : MonoBehaviour
{
    public int Class;
    public GameObject protoCelestialBody;
    private GameObject Field;
    private bool Spawned = false;
    private float repelForce = 1f;
    private float rotationalVelocity = 1f;
    private Vector3 ObjectVel;
    private Vector3 otherObjectVel;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().mass = GetComponent<Rigidbody>().mass * Random.Range(0.8f, 1);
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, rotationalVelocity * Random.Range(0.8f, 1)/ (GetComponent<Rigidbody>().mass / 2f), 0);
        repelForce = FindFirstObjectByType<GameManager>().GetComponent<GameManager>().repelForce;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CelestialBody")
        {
            ObjectVel = GetComponent<Rigidbody>().velocity;
            otherObjectVel = other.GetComponent<Rigidbody>().velocity;
            if (Class == other.GetComponent<Collision>().Class)
            {
                if ((GetComponent<Rigidbody>().mass > other.GetComponent<Rigidbody>().mass) && !Spawned)
                {
                    Vector3 pos1 = transform.position;
                    Vector3 pos2 = other.transform.position;
                    Destroy(other.gameObject);
                    GameObject obj = Instantiate(protoCelestialBody, position: pos1 + 0.5f * (pos2 - pos1), rotation: transform.rotation);
                    obj.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
                    obj.GetComponent<Transformation>().Class = Class + 1;
                    obj.GetComponent<Transformation>().Mass = GetComponent<Rigidbody>().mass + other.GetComponent<Rigidbody>().mass;
                    Spawned = true;
                    Destroy(gameObject);
                }
                else if (GetComponent<Rigidbody>().mass == other.GetComponent<Rigidbody>().mass) GetComponent<Rigidbody>().mass -= GetComponent<Rigidbody>().mass * Random.Range(0.1f, 0.2f);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CelestialBody") { 
            
            other.GetComponent<Rigidbody>().velocity = new Vector3(-otherObjectVel.x, 0, -otherObjectVel.z) * repelForce;
            GetComponent<Rigidbody>().velocity = new Vector3(ObjectVel.x, 0, ObjectVel.z) * repelForce;
        }
    }
}
