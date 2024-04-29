using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RaycastAndMove))]
public class RaycastAndMoveEditor : Editor
{
    private SerializedProperty playerCameraProp;
    private SerializedProperty objectsToMoveProp;
    private SerializedProperty groundLayerProp;

    private void OnEnable()
    {
        playerCameraProp = serializedObject.FindProperty("playerCamera");
        objectsToMoveProp = serializedObject.FindProperty("objectsToMove");
        groundLayerProp = serializedObject.FindProperty("groundLayer");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(playerCameraProp);
        EditorGUILayout.PropertyField(objectsToMoveProp, true);
        EditorGUILayout.PropertyField(groundLayerProp);

        serializedObject.ApplyModifiedProperties();
    }
}

public class RaycastAndMove : MonoBehaviour
{
    public Camera playerCamera;
    public Transform[] objectsToMove;
    public LayerMask groundLayer;

    private void Update()
    {
        // Стреляем лучи из углов камеры игрока
        Ray[] rays = CalculateRaysFromCamera();

        // Перемещаем объекты в пересечении лучей с землей
        MoveObjects(rays);
    }

    private Ray[] CalculateRaysFromCamera()
    {
        Vector3[] rayDirections = {
            playerCamera.ViewportPointToRay(new Vector3(0, 0, 0)).direction,
            playerCamera.ViewportPointToRay(new Vector3(1, 0, 0)).direction,
            playerCamera.ViewportPointToRay(new Vector3(0, 1, 0)).direction,
            playerCamera.ViewportPointToRay(new Vector3(1, 1, 0)).direction
        };

        Ray[] rays = new Ray[rayDirections.Length];

        for (int i = 0; i < rayDirections.Length; i++)
        {
            rays[i] = new Ray(playerCamera.transform.position, rayDirections[i]);
        }

        return rays;
    }

    private void MoveObjects(Ray[] rays)
    {
        foreach (Ray ray in rays)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                foreach (Transform obj in objectsToMove)
                {
                    obj.position = hit.point;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Визуализируем лучи в редакторе Unity
        Ray[] rays = CalculateRaysFromCamera();
        Gizmos.color = Color.red;

        foreach (Ray ray in rays)
        {
            Gizmos.DrawRay(ray);
        }
    }
}
