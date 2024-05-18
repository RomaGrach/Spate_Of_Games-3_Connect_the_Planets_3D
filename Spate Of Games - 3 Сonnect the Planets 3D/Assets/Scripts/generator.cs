using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class generator : MonoBehaviour
{
    public List<ItemWave> ItemsBase;
    public List<GameObject> ItemsOn;
    float timer;
    public Vector2 LeftUpAng;
    public Vector2 RighDownAng;
    public float high;
    public int MaxClas;
    public int MinClas;
    public float TimeGener;
    public int CountGen;
    private int currClass;
    private float modifiedTimeGener = 0f;
    GameManager gameManager;
    [System.Serializable]
    public class ItemWave
    {
        public List<GameObject> Items; // предметы
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>().GetComponent<GameManager>();
        currClass = Random.Range(MinClas, MaxClas);
        timer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timer > modifiedTimeGener)
        {
            Generate(CountGen, currClass);
            currClass = Random.Range(MinClas, MaxClas);
            modifiedTimeGener = TimeGener * currClass;
            MaxClas = gameManager.maxClass;
            timer = 0;
        }

        timer += Time.deltaTime;

    }
    public void Generate(int a, int clas)
    {
        
        for (int k = 0; k < a; k++)
        {
            float x = Random.Range(RighDownAng.x,LeftUpAng.x);
            float y = high;
            float z = Random.Range(LeftUpAng.y, RighDownAng.y);
            GameObject obj = gameManager.CelestialBodies[clas].Get(Random.Range(0, gameManager.GetComponent<GameManager>().CelestialBodies[clas].Items.Count));
            float mas = obj.GetComponent<Rigidbody>().mass;
            Vector3 vel = obj.GetComponent<Rigidbody>().angularVelocity;
            obj.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, Random.Range(0.8f, 1) / (obj.GetComponent<Rigidbody>().mass / 2f), 0);
            obj.GetComponent<Rigidbody>().mass = mas * Random.Range(0.8f, 1);
            GameObject nowCube = Instantiate(obj, new Vector3(x, y, z), transform.rotation);
            obj.GetComponent<Rigidbody>().angularVelocity = vel;
            obj.GetComponent<Rigidbody>().mass = mas;
            ItemsOn.Add(nowCube);

        }
        
    }
}
