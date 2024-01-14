using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    [Header("Basic Setup:")]

    [Tooltip("This is the object the world will generate around")]
    public GameObject Player;
    [Tooltip("The MapLoadingManager script goes here. It's used to deload/reload already existing tiles")]
    public MapLoadingManager MapLoadingManager;
    
    [Tooltip("The distance between each object. You can use this to create gaps.")]
    public int ObjectDistance;
    
    [Tooltip("This is what will be spawned")]
    public GameObject ObjectToSpawn;
    
    [Header("Extra settings:")]

    [Tooltip("Objects will spawn in this distance")]
    public int RenderRadious = 10;

    [Tooltip("The color of the sphere, that will show the area where new chunks will begin to load")]
    public Color MapSpawnDistanceGizmo = Color.blue;

    [Tooltip("If true, a random offset will be applied to all spawned objects")]
    public bool RandomOffset;

    [Tooltip("The minimum random distance. Can be negative, which will make the object go the other way.")]
    public Vector3 Minimumoffset; 
    
    [Tooltip("The maximum offset")]
    public Vector3 Maximumoffset;

    
    [Tooltip("If true, the objects will be randomly rotated on spawning")]
    public bool RandomRotate;







    private int XPlayerMove => (int)(Player.transform.position.x - StartPosition.x);
    private int ZPlayerMove => (int)(Player.transform.position.z - StartPosition.z);
    private int XPlayerLocation => (int)Mathf.Floor(Player.transform.position.x/ObjectDistance) * ObjectDistance;
    private int ZPlayerLocation => (int)Mathf.Floor(Player.transform.position.z/ObjectDistance) * ObjectDistance;

    private Hashtable tileobjs = new Hashtable();
    private Vector3 StartPosition;

    void Start(){
        if(Player == null){
            Debug.LogError("No player assigned to the Map manager.");
        }
    }
    void Update()
    {
        if(Player == null){
            return;
        }
        
        if(StartPosition == Vector3.zero){
            for(int x = -RenderRadious; x < RenderRadious; x++){
                for (int z = -RenderRadious; z < RenderRadious; z++)
                {
                    
                    Vector3 pos = new Vector3((x * ObjectDistance + XPlayerLocation), 0, (z * ObjectDistance + ZPlayerLocation));

                    if(!tileobjs.Contains(pos)){
                        GameObject OBJ = Instantiate(ObjectToSpawn, pos, Quaternion.identity);
                        if(RandomRotate){
                           OBJ.transform.Rotate(0,Random.Range(-360, 360), 0); 
                        }
                        if(RandomOffset){
                           OBJ.transform.position += new Vector3(Random.Range(Minimumoffset.x, Maximumoffset.x), Random.Range(Minimumoffset.y, Maximumoffset.y), Random.Range(Minimumoffset.z, Maximumoffset.z)); 
                        }
                        
                        tileobjs.Add(pos, OBJ);
                        OBJ.transform.parent = this.transform;
                        MapLoadingManager.ObjectsPooled.Add(OBJ);
                    }
                }
            }
        }

        if(HasPlayerMoved()){
            for(int x = -RenderRadious; x < RenderRadious; x++){
                for (int z = -RenderRadious; z < RenderRadious; z++)
                {
                    
                    Vector3 pos = new Vector3((x * ObjectDistance + XPlayerLocation), 0, (z * ObjectDistance + ZPlayerLocation));
                    if(!tileobjs.Contains(pos)){
                        GameObject OBJ = Instantiate(ObjectToSpawn, pos, Quaternion.identity);
                        if(RandomRotate){
                           OBJ.transform.Rotate(0,Random.Range(-360, 360), 0); 
                        }
                        if(RandomOffset){
                           OBJ.transform.position += new Vector3(Random.Range(Minimumoffset.x, Maximumoffset.x), Random.Range(Minimumoffset.y, Maximumoffset.y), Random.Range(Minimumoffset.z, Maximumoffset.z)); 
                        }
                        tileobjs.Add(pos, OBJ);
                        OBJ.transform.parent = this.transform;
                    }
                }
            }
        }
    }

    bool HasPlayerMoved(){
        if(Mathf.Abs(XPlayerMove) >= ObjectDistance || Mathf.Abs(ZPlayerMove) >= ObjectDistance){
            return true;
        }else{
            return false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = MapSpawnDistanceGizmo;
        Gizmos.DrawWireSphere(Player.transform.position, RenderRadious * ObjectDistance);
    }
}
