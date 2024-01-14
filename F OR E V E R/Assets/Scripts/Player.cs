using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    //Pausing
    public GameObject pausePanel;
    private bool isPaused = false;


    //Death
    public GameObject DeathPanel;
    [SerializeField] Animation deathAnim;


    //Press any key menu
    //public GameObject pressKey;
    
    //GameObjects
    public GameObject Plane;
    [SerializeField] public GameObject PlaneGraphics;
   

    //Rotation
    float rotationSpeed;
    public float maxRotationSpeed;
    

    public float forwardSpeed = 25f, strafeSpeed = 7.5f; //hoverSpeed = 5f

    private float activeForwardSpeed, activeStrafeSpeed; //activeHoverSpeed

    //How fast the plane goes from 0 to moving
    private float strafeAcceleration = 2f;

    private Rigidbody rb;

    Quaternion targetRotation;

    public Collider m_Collider;
    public Animation ShieldAnim;

   
   [SerializeField] AudioSource Music;
    
 
    //Chatgpt
    public Text scoreText;
    public Text highScoreText;

    public float lastScore;
    public float highScore;
    
    void Awake() 
    {
        //Time.timeScale = 0;
        //GameIsPaused = true;
    }

    void Start()
    {
        
        rb = this.gameObject.GetComponent<Rigidbody>();
        StartCoroutine(Shield());    
        
        // Load the high score from player preferences
    highScore = PlayerPrefs.GetFloat("HighScore", 0);
    
    // Display the high score
    highScoreText.text = "High Score: " + FormatTime(highScore);
    }

    void FixedUpdate()
    {

        
       
        if(PlaneGraphics)
        {
        var hInput = Input.GetAxis("Horizontal");
        rotationSpeed = maxRotationSpeed * hInput;

        rb.velocity = new Vector3(hInput * activeForwardSpeed, 0f, forwardSpeed);

        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal")* strafeSpeed, strafeAcceleration * Time.fixedDeltaTime);
          
        //activeHoverSpeed = Input.GetAxisRaw("Hover") * hoverSpeed;

        rb.MovePosition(transform.position + transform.right * activeStrafeSpeed * Time.fixedDeltaTime);   
        }
    }

    private void Update()
    {
        //PressSpace();
        
        

        var hInput = Input.GetAxis("Horizontal");
        //Turning behaviour
        targetRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.AngleAxis(-50 * Mathf.Sign(hInput), Vector3.forward), Mathf.Abs(rotationSpeed / maxRotationSpeed));
        if(PlaneGraphics)
        {
            PlaneGraphics.transform.localRotation = Quaternion.Slerp(PlaneGraphics.transform.localRotation, targetRotation,Time.deltaTime * 4);
        }
        
        if (PlaneGraphics)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }


if(PlaneGraphics)
{
         // Update the score text every frame
    lastScore += Time.deltaTime;
    scoreText.text = "Score: " + FormatTime(lastScore);

    // Update the high score if the current score is higher
    if (lastScore > highScore)
    {
        highScore = lastScore;
        highScoreText.text = "High Score: " + FormatTime(highScore);
        // Save the new high score to player preferences
        PlayerPrefs.SetFloat("HighScore", highScore);
        PlayerPrefs.Save();
    }
}
    }
private string FormatTime(float time)
{
    int minutes = Mathf.FloorToInt(time / 60f);
    int seconds = Mathf.FloorToInt(time % 60f);
    int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);
    return string.Format("{1:00}:{2:000}", minutes, seconds, milliseconds);
}
    
    public IEnumerator Death()
    {
        Destroy(PlaneGraphics);
        yield return new WaitForSeconds(0.5f);
        DeathPanel.SetActive(true);
        deathAnim.Play();


       
       
    }

    void OnCollisionEnter(Collision other) 
    {
        StartCoroutine(Death());
    }


   

    public IEnumerator Shield()
    {
        ShieldAnim.Play();
        m_Collider.enabled = false;
        yield return new WaitForSeconds(3);
        m_Collider.enabled = true;
    }

    void OnTriggerEnter(Collider target)
    {
        if(target.tag == "Shield")
        {
            StartCoroutine(Shield());
        }
    }


     public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
  
    /*void PressSpace()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //GameIsPaused = false;
            Destroy(pressKey);
            Time.timeScale = 1;
            Music.Play();
        }   
    }*/



    
}
        



