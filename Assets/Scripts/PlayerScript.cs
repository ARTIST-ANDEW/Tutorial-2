using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;  
    public AudioClip musicClipThree;  
    public AudioSource musicSource;
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;
    public Text winText;
    public Text lives;
    private bool facingRight = true;
    Animator anim;
    bool m_Jump;

    private int scoreValue = 0;
    private int livesValue = 3;

    public float jumpforce;
    private int level;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives.text = livesValue.ToString();
        winText.text = "";
        level = 1;
        anim = GetComponent<Animator>();
        m_Jump = false;
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        if(livesValue > 0)
        {
            if (Input.GetKey(KeyCode.D))
            {
                anim.SetInteger("State",2);

            }
            if (Input.GetKey(KeyCode.A))
            {
                anim.SetInteger("State",2);

            }
            if (Input.GetKey(KeyCode.W))
            {
                m_Jump = true;

            }
            //else m_Jump = false;
            
            if (m_Jump == false)
                anim.SetBool("Jump", false);

            if (m_Jump == true)
                anim.SetBool("Jump", true);
        }    
    }

    void FixedUpdate()
    {
        if(livesValue > 0)
        {
            float hozMovement = Input.GetAxis("Horizontal");
            float vertMovement = Input.GetAxis("Vertical");
            rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
            if(facingRight == false && hozMovement > 0)
            {
                Flip();
            }
            else if(facingRight == true && hozMovement < 0)
            {
                Flip();
            }
        
            else if(hozMovement == 0 && vertMovement == 0)
            {
                anim.SetInteger("State",0);
            }
        }
        
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            musicSource.clip = musicClipOne;
            musicSource.Play();
            scoreValue += 1;
            if(scoreValue == 4 && level == 1)
            { 
                level = 2;
                transform.position = new Vector2(53, 6);
                livesValue = 3;
                lives.text = livesValue.ToString();
            }
            if(scoreValue == 8)
            {
                winText.text = "You win! This game was made by Andrew Sisk."; 
                musicSource.clip = musicClipTwo;
                musicSource.Play();  
            }
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            musicSource.clip = musicClipThree;
            musicSource.Play();
            if(livesValue == 0)
            {
                winText.text = "You Lose."; 
                anim.SetInteger("State",4);   
                
            }
            Destroy(collision.collider.gameObject);
            lives.text = livesValue.ToString();

        }
        if (collision.collider.tag == "Ground")
        {
            m_Jump = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(livesValue > 0)
        {
            if (collision.collider.tag == "Ground")
            {
            
                if (Input.GetKey(KeyCode.W))
                {
                    rd2d.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse); 
                

                
                }
            }
        }
        
    }

}