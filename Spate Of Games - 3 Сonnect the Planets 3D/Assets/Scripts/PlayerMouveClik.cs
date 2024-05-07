using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMouveClik : MonoBehaviour
{

    public GameObject nowCube;
    public LayerMask layer;
    public float speed =1;
    public AnimationCurve slowingCurve; // Кривая замедления


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nowCube != null)
        {
            Debug.Log("1111111111");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, layer))
            {
                Debug.Log("222222222");
                Debug.Log(hit.collider.tag);
                //Instantiate(cub, hit.point, new Quaternion(0, 0, 0, 0));
                if (hit.collider.tag == "ground")
                {
                    /*
                    Debug.Log("33333333");
                    float x = hit.point.x * speed;
                    float y = nowCube.transform.position.y * speed;
                    float z = hit.point.z * speed;

                    nowCube.transform.position = new Vector3(x, y, z);
                    Debug.Log("+++++++");
                    */

                    
                    float x = (hit.point.x - nowCube.transform.position.x) * speed;
                    float y = nowCube.transform.position.y * speed;
                    float z = (hit.point.z - nowCube.transform.position.x) * speed;

                    Vector3 a = hit.point - nowCube.transform.position;
                    Debug.Log("a");
                    Rigidbody rb = nowCube.GetComponent<Rigidbody>();
                    rb.AddForce(a.x * speed, 0, a.z * speed, ForceMode.Force);

                    rb.velocity *= a.magnitude / rb.velocity.magnitude;

                    /*
                    Vector3 targetPosition = hit.point;

                    // Вычисляем направление и дистанцию к целевой точке
                    Vector3 direction = targetPosition - nowCube.transform.position;
                    direction = new Vector3(direction.x, 0, direction.z);
                    float distance = direction.magnitude;

                    // Нормализуем направление
                    direction.Normalize();

                    // Вычисляем текущую скорость с использованием кривой замедления
                    float currentSpeed = speed * slowingCurve.Evaluate(1 - Mathf.Clamp01(distance));

                    // Применяем силу с учетом замедления
                    Rigidbody rb = nowCube.GetComponent<Rigidbody>();
                    rb.velocity = direction * currentSpeed;
                    */

                    Debug.Log("+++++++");
                    

                }

            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("4444444444");
            Debug.Log("press");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("555555555");
                //Instantiate(cub, hit.point, new Quaternion(0, 0, 0, 0));
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "CelestialBody") //CelestialBody
                {
                    Debug.Log("666666666");
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
