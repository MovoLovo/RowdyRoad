using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    //Vars for Ob
    public GameObject Ob;

    //Vars for score
    public int unrealScore;
    public int score;

    //Don't think I need this but its here
    private void Start()
    {
        Ob.GetComponent<obstacleController>();
    }

    //Does this when obstacle enter this objects collider
    void OnTriggerEnter(Collider other)
    {
        //If correct game object type
        if(other.gameObject.tag == "Obstacle")
        {
            //Adds a fo-score and real score
            unrealScore++;
            score++;
            MasterStageController.ctrlr.levelLen--;

            //Real score needs to be keep while fo-score is used to 
            //Add 100 points when the game is over
            //if (unrealScore % (int)Ob.GetComponent<obstacleController>().obCount == 0)
            //{
            //    score += 100;
            //    unrealScore = 0;
            //}

            //Sets the gameObject to inactive after it touches the counter
            other.gameObject.SetActive(false);

            //Tells the obstacleController to stop creating obstacles
            Ob.GetComponent<obstacleController>().allReady = false;
        }
    }
}
