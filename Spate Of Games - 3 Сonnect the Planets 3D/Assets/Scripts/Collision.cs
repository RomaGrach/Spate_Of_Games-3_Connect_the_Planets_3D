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
    private GameObject Field;
    private bool Spawned = false;
    public bool Repel = false;
    public GameObject Repelant;
    private float rotationalVelocity = 1f;
    public Vector3 Velocity;
    private Vector3 ObjectVel;
    private Vector3 otherObjectVel;
    private GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().mass = GetComponent<Rigidbody>().mass * Random.Range(0.8f, 1);
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, rotationalVelocity * Random.Range(0.8f, 1)/ (GetComponent<Rigidbody>().mass / 2f), 0);
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        explosion = gameManager.GetComponent<GameManager>().ExplosionEffect;
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
            Instantiate(explosion, pos, rot);
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
                else if (Class > 2 * other.GetComponent<Collision>().Class)
                {
                    GetComponent<Rigidbody>().mass += other.GetComponent<Rigidbody>().mass;
                    Destroy(other.gameObject);
                }
            }
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
}
