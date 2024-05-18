using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Transformation : MonoBehaviour
{
    public int Class;
    public float Mass = 1;
    public float MaxClassMassFactor = 10f;
    public float old_mass;
    public int Base_Timer;
    public GameObject CelestialBody;
    public float currTime;
    private float startTime;
    private bool black_hole = false;
    
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        if (black_hole) CelestialBody = FindObjectOfType<GameManager>().GetComponent<GameManager>().CelestialBodies[10].Items[0];
        else CelestialBody = FindObjectOfType<GameManager>().GetComponent<GameManager>().CelestialBodies[Class].Items[Random.Range(0, FindObjectOfType<GameManager>().GetComponent<GameManager>().CelestialBodies[Class].Items.Count)]; 
        GetComponent<Transform>().localScale *= CelestialBody.GetComponent<Collision>().Scale;
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
            GetComponent<SphereCollider>().enabled = false;
            GameObject body = Instantiate(CelestialBody, pos, transform.rotation);
            body.GetComponent<Rigidbody>().mass = Mass;
            body.GetComponent<Rigidbody>().velocity = vel;
            body.GetComponent<Rigidbody>().angularVelocity = drag;
            Destroy(gameObject);
        }
    }
}
