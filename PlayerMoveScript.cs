using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMoveScript : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public float moveStrength;
    public float jumpStrength;
    public float move_jump_Strength;
    //public float speed;
    bool onGround = true;
    public Sprite jumpSprite;
    public Sprite moveSprite;
    public Sprite deadSprite;
    float ceiling = 50;
    float floor = -100;
    public GameObject gameOverScreen;
    bool alive = true;
    public GameObject heart;
    public Sprite empty_heart;
    int c = 0;
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
            c++;
            heart = GameObject.FindGameObjectWithTag(c.ToString());
            heart.GetComponent<Image>().sprite = empty_heart;
            if (c == 3)
                alive = false;
            /*player.transform.position = new Vector3(0f, -1.125f, 0f);
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            enemy.transform.position = new Vector3(15f, -1.45f, 0f);
            enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;*/
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
        if(collision.gameObject.tag == "Platform")
        {
        onGround = false;
        player.GetComponent<SpriteRenderer>().sprite = jumpSprite;
        }
    }

    public void GameOver()
    {
        player.GetComponent<SpriteRenderer>().sprite = deadSprite;
        gameOverScreen.SetActive(true);
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
