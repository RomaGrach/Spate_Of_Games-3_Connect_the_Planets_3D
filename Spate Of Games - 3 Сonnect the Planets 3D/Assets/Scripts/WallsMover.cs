using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class WallsMover : MonoBehaviour
{
    public LayerMask layer;
    public Vector3[] WallsPoint;
    public GameObject[] Walls;
    public generator GEN;
    public float Smesh = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        WallsPoint = CalculateRaysFromCamera();
        bool eror = false;
        for (int i = 0; i < Walls.Length; i++)
        {
            if (WallsPoint[i].x == 0 && WallsPoint[i].y == 0 && WallsPoint[i].z == 0)
            {
                eror = true;
            }
        }
        if (!eror)
        {
            for (int i = 0; i < Walls.Length; i++)
            {
                Walls[i].transform.position = new Vector3(WallsPoint[i].x, Walls[i].transform.position.y, WallsPoint[i].z);
                
            }

            if (GEN)
            {
                Vector2 LUA = new Vector2(WallsPoint[0].x * Smesh, WallsPoint[1].z * Smesh);
                Vector2 RDA = new Vector2(WallsPoint[2].x * Smesh, WallsPoint[3].z * Smesh);

                GEN.LeftUpAng = LUA;
                GEN.RighDownAng = RDA;
            }

        }


        
    }

    private Vector3[] CalculateRaysFromCamera()
    {
        Vector3[] rayDirections = {
            Camera.main.ViewportPointToRay(new Vector3(0, 0.5f, 0)).direction,
            Camera.main.ViewportPointToRay(new Vector3(0.5f, 1, 0)).direction,// 0.5f 1
            Camera.main.ViewportPointToRay(new Vector3(1, 0.5f, 0)).direction, // 1 0.5f
            Camera.main.ViewportPointToRay(new Vector3(0.5f, 0, 0)).direction
        };
        Debug.DrawRay(Camera.main.transform.position, rayDirections[0] * 100, Color.green);
        Debug.DrawRay(Camera.main.transform.position, rayDirections[1] * 100, Color.red);
        Debug.DrawRay(Camera.main.transform.position, rayDirections[2] * 100, Color.blue);
        Debug.DrawRay(Camera.main.transform.position, rayDirections[3] * 100, Color.yellow);
        //Ray[] rays = new Ray[rayDirections.Length];
        //Gizmos.color = Color.red;
        Vector3[] pose = new Vector3[4];
        for (int i = 0; i < rayDirections.Length; i++)
        {
            //rays[i] = new Ray(Camera.main.transform.position, rayDirections[i]);
            Ray ray = new Ray(Camera.main.transform.position, rayDirections[i]);
            RaycastHit hit;
            //Gizmos.color = Color.green;
            //Gizmos.DrawRay(ray);
            //Debug.DrawRay(Camera.main.transform.position, rayDirections[i] *100, Color.green);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                if (hit.collider.tag == "ground")
                {
                    pose[i] = hit.point;
                }else
                {
                    Debug.LogError(hit.collider.tag);
                }
            }
            else
            {
                Debug.LogError("1");
            }

        }
        //Gizmos.DrawRay(Ray[0]);
        return pose;
    }
}
