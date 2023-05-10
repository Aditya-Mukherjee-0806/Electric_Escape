using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMoveScript : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public float MOVE_SPEED = 10f;
    public float JUMP_STRENGTH = 12.5f;
    public float MOVE_JUMP_STRENGTH = 10f;
    //public float speed;
    bool playerOnGround = true;
    public Sprite jumpSprite;
    public Sprite moveSprite;
    public Sprite deadSprite;
    const float CEILING = 50;
    const float FLOOR = -100;
    public GameObject gameOverScreen;
    bool isPlayerAlive = true;
    public GameObject healthSystem;
    public Sprite empty_heart;
    int numberOfEmptyHearts = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        if (playerOnGround && isPlayerAlive)
        {
            //player.transform.Translate(new Vector3(horizontalInput, verticalInput, 0f) * speed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.up * JUMP_STRENGTH;
                
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.right * MOVE_SPEED;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.left * MOVE_SPEED;
            }
            if ((Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W)))
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0.85f,1.4f) * MOVE_JUMP_STRENGTH;
            }
            if ((Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)))
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.85f, 1.4f) * MOVE_JUMP_STRENGTH;
            }
        }
        if (player.transform.position.y < FLOOR || player.transform.position.y > CEILING || !isPlayerAlive)
        {
            GameOver();            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            numberOfEmptyHearts++;
            healthSystem = GameObject.FindGameObjectWithTag(numberOfEmptyHearts.ToString());
            healthSystem.GetComponent<Image>().sprite = empty_heart;
            if (numberOfEmptyHearts == 3)
                isPlayerAlive = false;
            /*player.transform.position = new Vector3(0f, -1.125f, 0f);
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            enemy.transform.position = new Vector3(15f, -1.45f, 0f);
            enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;*/
        }
        else if(collision.gameObject.tag == "Platform")
        {
            playerOnGround = true;
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
            playerOnGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            playerOnGround = false;
            player.GetComponent<SpriteRenderer>().sprite = jumpSprite;
        }
    }

    public void GameOver()
    {
        player.GetComponent<SpriteRenderer>().sprite = deadSprite;
        gameOverScreen.SetActive(true);
    }
}
