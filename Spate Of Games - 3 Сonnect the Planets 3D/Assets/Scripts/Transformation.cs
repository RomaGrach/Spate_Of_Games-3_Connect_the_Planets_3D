using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Transformation : MonoBehaviour
{
    public int Class;
    public float Mass = 1;
    public int Base_Timer;
    public GameObject CelestialBody;
    public float currTime;
    private float startTime;
    
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        GameObject[] CelestialBodies = FindObjectOfType<GameManager>().GetComponent<GameManager>().CelestialBodies[Class];
        int ind = Random.Range(0, CelestialBodies.Length);
        Debug.Log(ind);
        CelestialBody = CelestialBodies[ind];
        Debug.Log(CelestialBody);
    }

    // Update is called once per frame
    void Update()
    {
        currTime = Time.time - startTime;
        if (currTime >= Base_Timer)
        {
            Vector3 vel = GetComponent<Rigidbody>().velocity;
            Vector3 drag = GetComponent<Rigidbody>().angularVelocity;
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Debug.Log(vel);
            Debug.Log(drag);
            GetComponent<SphereCollider>().enabled = false;
            Instantiate(CelestialBody, pos, transform.rotation);
            CelestialBody.GetComponent<Rigidbody>().mass = Mass;
            CelestialBody.GetComponent<Rigidbody>().velocity = vel;
            CelestialBody.GetComponent<Rigidbody>().angularVelocity = drag;
            Debug.Log(CelestialBody.GetComponent<Rigidbody>().velocity);
            Debug.Log(CelestialBody.GetComponent<Rigidbody>().angularVelocity);
            Destroy(gameObject);
        }
    }
}
