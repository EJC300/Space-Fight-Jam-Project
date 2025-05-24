using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    [SerializeField] private float BoundaryX;
    [SerializeField] private float BoundaryY;
    [SerializeField] private float BoundaryZ;
    [SerializeField] private float distanceThreshold;
    [SerializeField] private Transform player;
    public bool shifted;
    //Spawn Astreroids and Saucers
    public Vector3 originShifted;
   

    public Vector3 Wrap(Vector3 pos)
    {
        Vector3 wrappedPosition = pos;
        if (wrappedPosition.x >= BoundaryX)
        {
            wrappedPosition.x -= BoundaryX;
        }
         if (wrappedPosition.x < -BoundaryX)
        {
            wrappedPosition.x += BoundaryX;
        }
        if (wrappedPosition.y >= BoundaryY)
        {
            wrappedPosition.y -= BoundaryY;
        }
         if (wrappedPosition.y < -BoundaryY)
        {
            wrappedPosition.y += BoundaryY;
        }
        if (wrappedPosition.z >= BoundaryZ)
        {
            wrappedPosition.z -= BoundaryZ;
        }
        if (wrappedPosition.z < -BoundaryZ)
        {
            wrappedPosition.z += BoundaryZ;
        }
        return player.position + wrappedPosition;
    }

    public void ResetPositionAtThreshold()
    {
        Vector3 cameraPos = player.position;
        shifted = cameraPos.magnitude > distanceThreshold;
        if (cameraPos.magnitude> distanceThreshold)
        {

            for (int z = 0; z < SceneManager.sceneCount; z++)
            {
                foreach (GameObject g in SceneManager.GetSceneAt(z).GetRootGameObjects())
                    g.transform.position -= cameraPos;
            }
            originShifted = (Vector3.zero- cameraPos);
        }
    
    }
    public void WrapAround(Transform transformToWrap)
    {
        transformToWrap.position = Wrap(transformToWrap.position);
    }

    private void LateUpdate()
    {
       ResetPositionAtThreshold();
    }
    private void Start()
    {
      
        instance = this;
    }


}
