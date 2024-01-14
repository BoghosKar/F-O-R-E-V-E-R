using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRandom : MonoBehaviour
{
    public float chance;
    void Start()
    {
        float rand = Random.Range(0,100);

        if(rand > chance){
            Destroy(this.gameObject);
        }
    }
}
