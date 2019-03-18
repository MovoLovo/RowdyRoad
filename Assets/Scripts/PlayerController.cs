using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    //Var for the player
    public float pSpeed;

    //Var for GameObject controlling end text
    public GameObject EndGame;

    //Var to hold Obstacle information
    public GameObject Counter;

    //Vars to hold text to display
    public Text wintext;
    public Text scoreText;

    //Pub var for score text
    public int score;

    //Pub var for Pause Button
    public GameObject pauseButton;

    //Priv var for score text
    private string scorePrefix = "Score: ";

    //Var for player movement
    private Rigidbody rb;
    Renderer r;

    public float moveHorizontal;

    public float moveBall;

    Touch touch;

    void Start()
    {
        //Gets rigidbody component
        rb = GetComponent<Rigidbody>();
        r = GetComponent<Renderer>();

        //Sets the game over text to inactive
        EndGame.SetActive(false);
        wintext.enabled = false;
        pauseButton.SetActive(true);
    }

    void FixedUpdate()
    {
        //Gets user input
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.position.x >= Screen.width / 2)
            {
                moveHorizontal = 1.0f;
            }
            else if (touch.position.x <= Screen.width / 2)
            {
                moveHorizontal = -1.0f;
            }
            else
            {
                moveHorizontal = 0.0f;
            }
        }


        //Vectorized player movement
        Vector3 movement = new Vector3(moveHorizontal * pSpeed, 0.0f, 0.0f);

        //Moves player
        rb.AddForce(movement);

        if (transform.position.z < -8.1f)
        {
            rb.AddForce(Vector3.forward * moveBall);
        }

        if (transform.position.z > -7.9f)
        {
            rb.AddForce(Vector3.forward * -moveBall);
        }

        //Checks to see if the player is off screen
        if (transform.position.y <= -2 || r.material.color.b >= 0.999f)
        {
            //If yes, display game over
            EndGame.SetActive(true);
            wintext.enabled = true;
            pauseButton.SetActive(false);
            rb.constraints = RigidbodyConstraints.FreezeAll | RigidbodyConstraints.FreezeAll;
        }

        //Checks to see if game is over
        if (!(wintext.isActiveAndEnabled)) 
        {
            //If no, gets the score from the counter
            score = Counter.GetComponent<AddScore>().score;
        }

        //Makes score text reflect score
        scoreText.text = scorePrefix + score.ToString();
    }
}