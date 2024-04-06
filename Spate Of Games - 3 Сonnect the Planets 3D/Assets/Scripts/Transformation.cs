using System.Collections;
using System.Collections.Generic;
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
            float tempMass = CelestialBody.GetComponent<Rigidbody>().mass;
            CelestialBody.GetComponent<Rigidbody>().mass = Mass;
            Instantiate(CelestialBody, transform.position, transform.rotation);
            CelestialBody.GetComponent<Rigidbody>().mass = tempMass;
            Destroy(gameObject);
            startTime = Time.time;
        }
    }
}
