using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamController : MonoBehaviour
{
    //Var for player 
    public GameObject player;

    public float drunkSpeed = 1;

    //Vars for math
    private Vector3 offset;
    private Vector3 playerVec;

    private bool testBool;
    private int spinWait = 30;
    private int spinWaitCounter;
    private float spinDir;
    private Text gameText;

    void Start()
    {
        //Stores the offset between the ball and the player for later use
        offset = transform.position - player.transform.position;
        gameText = player.GetComponent<PlayerController>().wintext;
    }

    void LateUpdate()
    {
        //Creates a new v3 to store camera offset and player position mix
        //This is done so the camera doesn't follow the ball when it gets knocked back
        playerVec = new Vector3(player.transform.position.x, player.transform.position.y, -8.0f);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            testBool = !testBool;
        }

        if ((MasterStageController.ctrlr.cStage == Stage.WORLDSPIN_MODE || MasterStageController.ctrlr.cStage == Stage.DRUNK_MODE) && !gameText.isActiveAndEnabled)
        {
            rotationChange();
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + (spinDir * drunkSpeed));

            //switch (Input.GetAxisRaw("Horizontal"))
            //{
            //    case 1f:
            //        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + drunkSpeed);
            //        break;
            //    case -1f:
            //        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z - drunkSpeed);
            //        break;
            //    default:
            //        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + (spinDir * drunkSpeed));
            //        break;
            //}
        }

        //Sets the position of the camera
        transform.position = playerVec + offset;
    }

    void rotationChange()
    {
        if(spinWaitCounter >= spinWait)
        {
            float temp = Random.Range(-1f, 1f);

            if(temp >= 0)
            {
                spinDir = 1f;
            }
            else
            {
                spinDir = -1f;
            }

            spinWaitCounter = 0;
        }
        spinWaitCounter++;
    }
}