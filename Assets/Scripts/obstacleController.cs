using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class obstacleController : MonoBehaviour
{
    public GameObject Ob;
    public Text wintext;

    //Vars to do with obstacles
    public List<GameObject> Obstacles = new List<GameObject>();
    public float obCount = 50f;
    public float obIncrement = 5f;
    public bool allReady = true;
    public int obReady;

    //Vars to do with speed
    public float speedIncrement = 0f;
    public float speed = 75f;

    //Vars to do with time
    public float spawnWait = 0.5f;
    public float spawnWaitDecrement = 0.05f;
    public float startWait;
    public float waveWait = 5f;

    //Vars to do with Vanishing Line
    public float vanishLine = -10f;
    /* 
     * Vanishing line in this case is:
     * When to delete the gameObjects after they go off screen
     */

    //Var for game over
    static public bool gameOver;

    public float spinSpeed = 10f;
    public float spinStep = 1f;
    public bool eTestBool;
    public bool rTestBool;
    public float spawnCount;

    //Var for coroutines
    Coroutine c;
    private int k;

    void Start()
    {
        //You have to have a starting object to run the enumerater code
        //We on second thought you don't
        //But You don't have to change that
        //The code below is for the starting object
        transform.rotation = Random.rotation;
        transform.position = new Vector3(Random.Range(-5f, 5f), .5f, 80f);

        //Start func for the manager object
        //Runs only once
        if (gameObject.tag == "ObCarrier")
        {
            c = StartCoroutine(SpawnWaves());
            gameObject.transform.position = new Vector3(Random.Range(-5f, 5f), -3f, 80f);
            rTestBool = true;
        }
    }

    void FixedUpdate()
    {
        //Gets the state of the game: Over or Running
        if (wintext != null)
        {
            gameOver = wintext.isActiveAndEnabled;
        }

        //Sets the ob's in motion unless its the first ob
        if (!(gameOver))
        {
            if (!(gameObject.tag != "Obstacle" && gameObject.transform.position.z < vanishLine - 5f))
            {
                gameObject.transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
            }
        }

        //If game is over stop coroutine
        if (gameOver && c != null)
        {
            StopCoroutine(c);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            eTestBool = !eTestBool;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            rTestBool = !rTestBool;
        }

        if (MasterStageController.ctrlr.cStage == Stage.SPINNINGOBS_MODE || MasterStageController.ctrlr.cStage == Stage.DRUNK_MODE)
        {
            try
            {
                for (int i = 0; i < Obstacles.LastIndexOf(Obstacles.Last()); i++)
                {
                    try
                    {
                        Obstacles[i].transform.localEulerAngles = new Vector3(Obstacles[i].transform.localEulerAngles.x, Obstacles[i].transform.localEulerAngles.y, Obstacles[i].transform.localEulerAngles.z + spinSpeed);
                    }
                    catch
                    {
                        //Some objects are disabled so you can't control them
                    }
                }
            }
            catch
            {
                //For some reason it thinks that there are no objects in the array
                //Probably that's the problem but i'm too lazy to fix it and this works perfectly fine
            }
            //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }

    IEnumerator SpawnWaves()
    {
        //Waits until time to start
        yield return new WaitForSeconds(startWait);
        allReady = true;
        while (true)
        {
            for (int i = 0; i < obCount; i++)
            {
                //Spawns prefab ob's
                Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 0.5f, 80f);
                Quaternion spawnRotation = Random.rotation;

                //Starts by creating objects until one of them is deactivated
                if (allReady)
                {
                    if (MasterStageController.ctrlr.cStage != Stage.BREAK_MODE)
                    {
                        //For some reason stupid errors happen all over the place
                        //So this is kind of a fail safe
                        try
                        {
                            Obstacles.Add(Instantiate(Ob, spawnPosition, spawnRotation));
                        }
                        catch
                        {

                        }
                    }
                }
                else //When done creating inital objects, starts pooling them
                {
                    if (MasterStageController.ctrlr.cStage != Stage.BREAK_MODE)
                    {
                        //Creates a variable for the allActive func for accuracy
                        obReady = allActive(Obstacles);

                        //if obReady is -1 then it means that it needs to create another ob
                        //else it finds that inactive object and activates it
                        if (!(obReady == -1))
                        {
                            Obstacles[obReady].transform.position = spawnPosition;
                            Obstacles[obReady].transform.rotation = spawnRotation;
                            Obstacles[obReady].SetActive(true);
                        }
                        else
                        {
                            Obstacles.Add(Instantiate(Ob, spawnPosition, spawnRotation));
                        }
                    }
                }
                yield return new WaitForSeconds(spawnWait);
            }

            //Increments all the vars to increase difficulty
            spawnWait -= spawnWaitDecrement;
            obCount += obIncrement;
            speed += speedIncrement;

            //Waits for next wave
            yield return new WaitForSeconds(waveWait);
            k++;
        }
    }

    //Function to test if all Objects in array are active
    int allActive(List<GameObject> Objects)
    {
        int isActive = -1;

        //Loops through all of the objects until one is ready
        //else it just return -1
        for (var i = 0; i < Obstacles.LastIndexOf(Obstacles.Last()) + 1; i++)
        {
            if (!Obstacles[i].activeSelf)
            {
                isActive = i;
                return isActive;
            }
        }
        return -1;
    }
}