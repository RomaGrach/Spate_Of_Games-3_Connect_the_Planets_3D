using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMouveClik : MonoBehaviour
{

    public GameObject nowCube;
    public LayerMask layer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nowCube != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, layer))
            {
                Debug.Log(hit.collider.tag);
                //Instantiate(cub, hit.point, new Quaternion(0, 0, 0, 0));
                if (hit.collider.tag == "ground")
                {

                    float x = hit.point.x;
                    float y = nowCube.transform.position.y;
                    float z = hit.point.z;
                    nowCube.transform.position = new Vector3(x, y, z);

                }

            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("press");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Instantiate(cub, hit.point, new Quaternion(0, 0, 0, 0));
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "CelestialBody")
                {
                    nowCube = hit.collider.gameObject;

                    //nowCube = ObjectPooler.ME.RequestObject("1", new Vector3(hit.point.x, 10f, 0), new Quaternion(0, 0, 0, 0));

                }

            }

        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            nowCube = null;
        }
        
        
    }

    
}
