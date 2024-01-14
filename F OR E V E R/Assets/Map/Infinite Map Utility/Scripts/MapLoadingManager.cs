using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoadingManager : MonoBehaviour
{



    [Header("Setup:")]

    [Tooltip("Objects will load out if far away from this, also they will load back in if close to this object.")]
    public Transform player;
    [Tooltip("How far the game renders stuff. Needs to be a bit more than the value on the InfiniteMap script")]
    public float RenderDistance;

    
    [Tooltip("The color of the Sphere that will show the area where chunks will be loaded in")]
    public Color MapSpawnLoadGizmo = Color.green;
    
    [Tooltip("How often the game refreshes the tiles")]
    public float RefreshTime;    
    
    
    
    [Header("Other:")]
    public List<GameObject> ObjectsPooled = new List<GameObject>();
    void Start(){
        StartCoroutine(waitfordeload());

    }
    IEnumerator waitfordeload(){
        yield return new WaitForSeconds(RefreshTime);
        for (int i = 0; i < ObjectsPooled.Count; i++)
        {
            float dist = Vector3.Distance(ObjectsPooled[i].transform.position, player.position);
            if(dist > RenderDistance * 10){
                ObjectsPooled[i].SetActive(false);
            }else{
                ObjectsPooled[i].SetActive(true);
            }
        }
        StartCoroutine(waitfordeload());
    }


    void OnDrawGizmos()
    {
        Gizmos.color = MapSpawnLoadGizmo;
        Gizmos.DrawWireSphere(player.position, RenderDistance * 10);
    }
}
