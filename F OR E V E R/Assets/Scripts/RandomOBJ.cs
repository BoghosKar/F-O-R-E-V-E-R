using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOBJ: MonoBehaviour
{
     public GameObject[] options;
    void Start()
    {
        int rand = Random.Range(0, options.Length);

        options[rand].SetActive(true);
    }

}