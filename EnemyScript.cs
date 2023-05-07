using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject enemy;
    public GameObject target;
    public float enemyMoveStrength;
    public float within_range;
    float oldPos;
    bool movingLeft = true;
    bool oldMovingDirection = true;
    bool alive = true;
    //public bool isPlayerGrounded;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = enemy.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive)
            return;
        /*isPlayerGrounded = Physics.Raycast(target.transform.position, Vector3.down, 1f);
        if (!isPlayerGrounded)
            return;*/

        //get the distance between the player and enemy (this object)
        float dist = Vector3.Distance(target.transform.position, enemy.transform.position);

        // Get the player's position
        Vector3 playerPos = target.transform.position;

        // Get the enemy's position
        Vector3 enemyPos = enemy.transform.position;

        // Calculate the direction to move towards the player
        Vector3 direction = new Vector3(playerPos.x - enemyPos.x, 0f, 0f);

        // Normalize the direction vector to have a magnitude of 1
        direction.Normalize();

        //check if it is within the range you set
        if (dist <= within_range)
        {
            //move to target(player) 
            //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, enemyMoveStrength * Time.deltaTime);
            enemy.transform.position += direction * enemyMoveStrength * Time.deltaTime;
        }

        if (enemy.transform.position.x > oldPos)
            movingLeft = false;
        else
            movingLeft = true;

        if(movingLeft && movingLeft != oldMovingDirection)
        {
            enemy.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            Debug.Log("Flip");
        }
        else if(!movingLeft && movingLeft != oldMovingDirection)
        {
            enemy.transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);
            Debug.Log("Flip");
        }
    }

    private void LateUpdate()
    {
        oldPos = enemy.transform.position.x;
        oldMovingDirection = movingLeft;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
            alive = false;
    }
}
