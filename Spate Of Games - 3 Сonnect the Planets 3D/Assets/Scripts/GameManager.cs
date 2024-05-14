using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float GravitationalConstant = 1;
    public float RepelForce = 20f;
    public float SpeedLimit = 20f;
    public float SimulationPeriod = 0.3f;
    public int maxClass = 1;
    public GameObject ExplosionEffect;
    public List<ItemWave> CelestialBodies;
    /*
    public GameObject[][] CelestialBodies = new GameObject[6][];
    public GameObject[] Class_0;
    public GameObject[] Class_1;
    public GameObject[] Class_2;
    public GameObject[] Class_3;
    public GameObject[] Class_4;
    */
    private GameObject[] CurrentCelestialBodies;
    private float StartTime;
    private float CurrTime;

    [System.Serializable]
    public class ItemWave
    {
        public List<GameObject> Items; // ןנוהלועû
        public GameObject Get(int index)
        {
            return Items[index];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        CurrTime = Time.time - StartTime;
        if (CurrTime > SimulationPeriod)
        {
            CurrentCelestialBodies = GameObject.FindGameObjectsWithTag("CelestialBody");
            foreach (GameObject Body1 in CurrentCelestialBodies)
            {
                foreach (GameObject Body2 in CurrentCelestialBodies)
                {
                    if (Body1 != Body2)
                    {
                        AddGravitationalForce(Body1, Body2, GravitationalConstant, RepelForce, SpeedLimit, (Body1.GetComponent<Collision>().Repel && Body1.GetComponent<Collision>().Repelant == Body2));
                    }
                }
            }
            StartTime = CurrTime;
        }
        
    }
    void AddGravitationalForce(GameObject attractor, GameObject target, float G, float RepelF, float limit, bool Repel)
    {
        Rigidbody RigidAttractor = attractor.GetComponent<Rigidbody>();
        Rigidbody RigidTarget = target.GetComponent<Rigidbody>();
        Vector3 difference = RigidAttractor.position - RigidTarget.position;
        float distance = difference.magnitude;
        Vector3 Direction = difference.normalized;
        ForceMode Fmode;
        float Magnitude;
        if (Repel)
        {
            Fmode = ForceMode.Impulse;
            Magnitude = -RepelF * RigidAttractor.mass * RigidTarget.mass;
        }
        else
        {
            Fmode = ForceMode.Force;
            float mass = RigidAttractor.mass * RigidTarget.mass * G;
            Magnitude = G * mass / Mathf.Pow(distance, 2);
        }
        
        Vector3 Force = Direction * Magnitude;
        target.GetComponent<Rigidbody>().AddForce(Force, Fmode);
        float x = RigidTarget.velocity.x;
        float z = RigidTarget.velocity.z;
        if (x > limit) x = limit;
        if (z > limit) z = limit;
        RigidTarget.velocity = new Vector3(x, 0f, z);
    }
}
