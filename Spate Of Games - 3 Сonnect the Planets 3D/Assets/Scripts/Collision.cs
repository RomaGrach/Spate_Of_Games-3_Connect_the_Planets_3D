using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Collision : MonoBehaviour
{
    public int Class;
    public GameObject protoCelestialBody;
    
    
    private float _rotationalVelocity = 1f;
    private float _velocity = 1f;
    public Vector3 Velocity;
    public float DefaultMass = 1f;
    public float MaxClassMassFactor = 10f;
    public float Scale = 1f;

    public bool Repel = false;
    public GameObject Repelant;

    private float initScale = 1f;
    private bool Spawned = false;
    private Vector3 ObjectVel;
    private Vector3 otherObjectVel;
    private GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Rigidbody>().mass = GetComponent<Rigidbody>().mass * Random.Range(0.8f, 1);
        //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, rotationalVelocity * Random.Range(0.8f, 1)/ (GetComponent<Rigidbody>().mass / 2f), 0);
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, _rotationalVelocity * Random.Range(0.8f, 1) / (GetComponent<Rigidbody>().mass / 2f), 0);
        GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-_velocity, _velocity) / (GetComponent<Rigidbody>().mass / 2f), 0 ,Random.Range(-_velocity, _velocity) / (GetComponent<Rigidbody>().mass / 2f));
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        explosion = gameManager.GetComponent<GameManager>().ExplosionEffect;
        gameManager.GetComponent<GameManager>().maxClass = Mathf.Max(Class, gameManager.GetComponent<GameManager>().maxClass);
        ReScale(Scale);

    }

    // Update is called once per frame
    void Update()
    {
        Velocity = GetComponent<Rigidbody>().velocity;
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        Collider other = collision.collider;
        if (other.tag == "CelestialBody")
        {
            ObjectVel = GetComponent<Rigidbody>().velocity;
            otherObjectVel = other.GetComponent<Rigidbody>().velocity;
            ContactPoint contact = collision.GetContact(0);
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            if ((GetComponent<Rigidbody>().mass > other.GetComponent<Rigidbody>().mass) && Class > 9)
            {
                GetComponent<Rigidbody>().mass += other.GetComponent<Rigidbody>().mass;
                Destroy(other.gameObject);
            }
            else Instantiate(explosion, pos, rot);
            if (Class == other.GetComponent<Collision>().Class)
            {
                if ((GetComponent<Rigidbody>().mass > other.GetComponent<Rigidbody>().mass) && !Spawned)
                {
                    InitiateTransform(other);
                }
                else if (GetComponent<Rigidbody>().mass == other.GetComponent<Rigidbody>().mass) GetComponent<Rigidbody>().mass -= GetComponent<Rigidbody>().mass * Random.Range(0.001f, 0.01f);
            }
            else if (Class > 2 * other.GetComponent<Collision>().Class)
            {
                GetComponent<Rigidbody>().mass += other.GetComponent<Rigidbody>().mass;
                Destroy(other.gameObject);
            }
            else GetComponent<Rigidbody>().mass -= GetComponent<Rigidbody>().mass * Random.Range(0.001f, 0.01f);
            if (GetComponent<Rigidbody>().mass >= Class * MaxClassMassFactor) InitiateTransform(other);
        }
    }
    private void OnCollisionStay(UnityEngine.Collision collision)
    {
        Collider other = collision.collider;
        if (other.tag == "CelestialBody") {
            if (Class > 2 * other.GetComponent<Collision>().Class)
            {
                GetComponent<Rigidbody>().mass += other.GetComponent<Rigidbody>().mass;
                Destroy(other.gameObject);
            }
            else
            {
                Repelant = other.gameObject;
                Repel = true;
            }
            
        }
    }
    private void OnCollisionExit(UnityEngine.Collision collision)
    {
        Collider other = collision.collider;
        if (other.tag == "CelestialBody")
        {
            Repel = false;
        }
    }
    private void ReScale(float scale)
    {
        GetComponent<Transform>().localScale = GetComponent<Transform>().localScale * scale;
    }
    private void InitiateTransform(Collider other)
    {
        Vector3 pos1 = transform.position;
        Vector3 pos2 = other.transform.position;
        Destroy(other.gameObject);
        GameObject obj = Instantiate(protoCelestialBody, position: pos1 + 0.5f * (pos2 - pos1), rotation: transform.rotation);
        obj.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
        obj.GetComponent<Transformation>().Class = Class + 1;
        obj.GetComponent<Transformation>().Mass = GetComponent<Rigidbody>().mass + other.GetComponent<Rigidbody>().mass;
        obj.GetComponent<Transformation>().old_mass = DefaultMass;
        Spawned = true;
        Destroy(gameObject);
    }
}
