using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTerrain : MonoBehaviour
{
    public bool showbounds = false;

    public float hilliness = 1;
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;

    public int zSize = 20;

    public float perlinresolution;

    void Start(){
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        
    }

    void CreateShape(){
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for(int i = 0, z = 0; z <= zSize; z++){
            for(int x = 0; x <= xSize; x++){    
                float y = Mathf.PerlinNoise(((float)x  + this.transform.position.x) * perlinresolution,((float)z  + this.transform.position.z) * perlinresolution) * hilliness;
                vertices[i] = new Vector3(x,y,z);
                i++;
            }   
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

    for(int z = 0; z < zSize; z++){
        for(int x = 0; x < xSize; x++){
            
            triangles[tris + 0] = vert + 0;
            triangles[tris + 1] = vert + xSize + 1;
            triangles[tris + 2] = vert + 1;
            triangles[tris + 3] = vert + 1; 
            triangles[tris + 4] = vert + xSize + 1;
            triangles[tris + 5] = vert + xSize + 2;

            vert++;
            tris += 6;
        }
        vert++;
        UpdateMesh();
    }

        

    }
    void UpdateMesh(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }
    private void OnDrawGizmos(){  
        if(vertices == null)
            return;
        
    if(showbounds){
        for(int i = 0; i < vertices.Length; i++){
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }

    }
}