using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    
    public Collider m_Collider;
    public Animation ShieldAnim;
    public AudioSource pickupsfx;
    public GameObject Model;


    void OnTriggerEnter(Collider target)
    {
        if(target.tag == "Player")
        {
            StartCoroutine(shield());
        }
        pickupsfx.Play();
        Model.SetActive(false);
    }

    public IEnumerator shield()
    {
        ShieldAnim.Play();
        m_Collider.enabled = false;
        yield return new WaitForSeconds(3);
        m_Collider.enabled = true;
    }
}
