using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoveScript : MonoBehaviour
{
    public GameObject player;
    public float moveStrength;
    public float jumpStrength;
    public float move_jump_Strength;
    public float speed;
    public bool onGround = true;
    public Sprite jumpSprite;
    public Sprite moveSprite;
    public Sprite deadSprite;
    public float ceiling = 50;
    public float floor = -100;
    public GameObject canvas;
    public bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        if (onGround && alive)
        {
            //player.transform.Translate(new Vector3(horizontalInput, verticalInput, 0f) * speed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpStrength;
                
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.right * moveStrength;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.left * moveStrength;
            }
            if ((Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W)))
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0.85f,1.4f) * move_jump_Strength;
            }
            if ((Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)))
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.85f, 1.4f) * move_jump_Strength;
            }
        }
        if (player.transform.position.y < floor || player.transform.position.y > ceiling || !alive)
        {
            GameOver();            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            alive = false;
        }
        else if(collision.gameObject.tag == "Platform")
        {
            onGround = true;
            if(player.GetComponent<SpriteRenderer>().sprite != moveSprite)
            {
                player.GetComponent<SpriteRenderer>().sprite = moveSprite;
                Debug.Log("Sprite");
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "platform")
            onGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
        player.GetComponent<SpriteRenderer>().sprite = jumpSprite;
    }

    public void GameOver()
    {
        player.GetComponent<SpriteRenderer>().sprite = deadSprite;
        canvas.SetActive(true);
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void exit()
    {
        Application.Quit();
    }
}
