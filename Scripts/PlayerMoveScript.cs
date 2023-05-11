using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMoveScript : MonoBehaviour
{
    public float MOVE_SPEED = 10f;
    public float JUMP_STRENGTH = 12.5f;
    public float MOVE_JUMP_STRENGTH = 10f;
    public GameObject player;
    public GameObject enemy;
    public GameObject gameOverScreen;
    public GameObject healthSystem;
    public Sprite jumpSprite;
    public Sprite restSprite;
    public Sprite deadSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite empty_heart;
    //public float speed;
    bool playerIsOnGround = false;
    bool isPlayerAlive = true;
    bool playerIsMovingRight;
    bool playerIsMovingLeft;
    bool playerIsAtRest;
    int numberOfEmptyHearts = 0;
    float oldPositionOfPlayer;
    const float CEILING = 50;
    const float FLOOR = -100;
    // Start is called before the first frame update
    void Start()
    {
        oldPositionOfPlayer = transform.position.x;
        playerIsAtRest = true;
        playerIsMovingLeft = false;
        playerIsMovingRight = false;
    }

    private void LateUpdate()
    {
        oldPositionOfPlayer = player.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        if (playerIsOnGround && isPlayerAlive)
        {
            //player.transform.Translate(new Vector3(horizontalInput, verticalInput, 0f) * speed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.up * JUMP_STRENGTH;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.right * MOVE_SPEED;
                player.GetComponent<SpriteRenderer>().sprite = rightSprite;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.left * MOVE_SPEED;
                player.GetComponent<SpriteRenderer>().sprite = leftSprite;
            }
            else
            {
                player.GetComponent<SpriteRenderer>().sprite = restSprite;
            }
            if ((Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W)))
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(1f,1.5f) * MOVE_JUMP_STRENGTH;
            }
            if ((Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)))
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f, 1.5f) * MOVE_JUMP_STRENGTH;
            }
        }

        /*if(player.transform.position.x > oldPositionOfPlayer && isPlayerOnGround)
        {
            playerIsMovingRight = true;
            playerIsMovingLeft = false;
            playerIsAtRest = false;
        }
        else if(player.transform.position.x < oldPositionOfPlayer && isPlayerOnGround)
        {
            playerIsMovingLeft = true;
            playerIsMovingRight = false;
            playerIsAtRest = false;
            
        }
        else if(player.transform.position.x == oldPositionOfPlayer && isPlayerOnGround)
        {
            playerIsAtRest = true;
            playerIsMovingLeft = false;
            playerIsMovingRight = false;
        }
        else
        {
            playerIsMovingLeft = false;
            playerIsMovingRight = false;
            playerIsAtRest = false;
        }

        if(playerIsAtRest && player.GetComponent<SpriteRenderer>().sprite != restSprite)
        {
            player.GetComponent<SpriteRenderer>().sprite = restSprite;
        }
        else if(playerIsMovingLeft && player.GetComponent<SpriteRenderer>() != leftSprite)
        {
            player.GetComponent<SpriteRenderer>().sprite = leftSprite;
        }
        else if(playerIsMovingRight && player.GetComponent<SpriteRenderer>() != rightSprite)
        {
            player.GetComponent<SpriteRenderer>().sprite = rightSprite;
        }*/

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
            playerIsOnGround = true;
            /*if(player.GetComponent<SpriteRenderer>().sprite != restSprite)
            {
                player.GetComponent<SpriteRenderer>().sprite = restSprite;
                Debug.Log("Sprite");
            }*/
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "platform")
            playerIsOnGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            playerIsOnGround = false;
            player.GetComponent<SpriteRenderer>().sprite = jumpSprite;
        }
    }

    public void GameOver()
    {
        player.GetComponent<SpriteRenderer>().sprite = deadSprite;
        gameOverScreen.SetActive(true);
    }
}
