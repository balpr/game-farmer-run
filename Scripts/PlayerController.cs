using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public float jumpForce;
	public bool gameOver = false;
	public ParticleSystem explosionParticle;
	public ParticleSystem dirtParticle;
	public AudioClip jumpSound;
	public AudioClip crashSound;

	public GameObject resetText;
	public GameObject startText;
	// public float gravityModifier;

	private Rigidbody rb;
	private bool isOnGround = true;
	private Animator playerAnim;
	private AudioSource playerAudio;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        Time.timeScale = 0;
        startText.SetActive(true);
        resetText.SetActive(false);

        // Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver && Time.timeScale == 1)
        {
        	rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        	isOnGround = false;
        	playerAnim.SetTrigger("Jump_trig");
        	dirtParticle.Stop();
        	playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        if(Time.timeScale == 0)
        {
        	if(Input.GetKeyDown(KeyCode.Space))
        	{
        		startText.SetActive(false);
        		Time.timeScale = 1;
        	}
        }

        if(gameOver)
        {
        	resetText.SetActive(true);
        	if(Input.GetKeyDown(KeyCode.R))
        	{
        		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        	}
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
    	if(collision.gameObject.CompareTag("Ground") && !gameOver)
    	{
    		isOnGround = true;
    		dirtParticle.Play();
    	}
    	else if(collision.gameObject.CompareTag("Obstacle"))
    	{
    		gameOver = true;
    		Debug.Log("Game Over!");
    		playerAnim.SetBool("Death_b", true);
    		playerAnim.SetInteger("DeathType_int", 1);
    		explosionParticle.Play();
    		dirtParticle.Stop();
    		playerAudio.PlayOneShot(crashSound, 1.0f);
    	}
    }
}
