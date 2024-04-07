using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    [System.Serializable]
    public class ItemWave
    {
        public List<GameObject> Items; // предметы
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > TimeGener)
        {
            timer = 0;
            Generate(CountGen);
        }

        timer += Time.deltaTime;

    }


    public void Generate(int a)
    {
        for (int k = 0; k < a; k++)
        {
            float x = Random.Range(RighDownAng.x,LeftUpAng.x);
            float y = high;
            float z = Random.Range(LeftUpAng.y, RighDownAng.y);
            int Clas = Random.Range(MinClas, MaxClas);
            GameObject obj = ItemsBase[Clas].Items[Random.Range(MinClas, ItemsBase[Clas].Items.Count)];
            GameObject nowCube = Instantiate(obj, new Vector3(x, y, z), transform.rotation);
            ItemsOn.Add(nowCube);

        }
    }
}
