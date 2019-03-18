using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MasterStageController : MonoBehaviour
{
    public static MasterStageController ctrlr;
    public Stage cStage = Stage.START_STAGE;

    public int level;
    public int levelSec;
    public int levelLen;

    public int START_MODE_len;
    public int DRUNK_MODE_len;
    public int SPINNINGOBS_MODE_len;
    public int BREAK_MODE_len;
    public int NORMAL_MODE_len;
    public int WORLDSPIN_MODE_len;

    public ArrayList levels;

    private void Start()
    {
        ctrlr = this;
        stageSwitch();
        levels = levelRandom(level);
    }

    private void Update()
    {
        if (ctrlr.levelLen <= 0)
        {
            ctrlr.levelSec++;

            if (levelSec > levels.Count)
            {
                ctrlr.levelSec = 0;
                ctrlr.level++;
                ctrlr.levels = levelRandom(level);
                ctrlr.cStage = (Stage)ctrlr.levels[0];
                stageSwitch();
                print(levels.Count);

                //TODO: New Level Text Initiate
            }
            else
            {
                ctrlr.cStage = (Stage)levelSec;
                stageSwitch();
            }
        }
    }

    void stageSwitch()
    {
        switch (ctrlr.cStage)
        {
            case Stage.START_STAGE:
                //Add Start code
                ctrlr.levelLen = START_MODE_len;

                break;
            case Stage.DRUNK_MODE:
                //Camera spinning randomly
                ctrlr.levelLen = DRUNK_MODE_len;

                break;
            case Stage.WORLDSPIN_MODE:
                //Camera spinning controlled by player
                ctrlr.levelLen = WORLDSPIN_MODE_len;

                break;
            case Stage.SPINNINGOBS_MODE:
                //Objects spin when they start
                ctrlr.levelLen = SPINNINGOBS_MODE_len;

                break;
            case Stage.BREAK_MODE:
                //Give the plays a quick break for pacing
                ctrlr.levelLen = BREAK_MODE_len;

                break;
            default:
                //Normal mode
                ctrlr.levelLen = NORMAL_MODE_len;

                break;
        }
        print(ctrlr.cStage);
    }

    ArrayList levelRandom(int l) 
    {
        ArrayList a = new ArrayList();

        for (int i = 0; i < l; i++)
        {
            // Minus 1 so that break mode is excluded
            int randInt = Random.Range(0, System.Enum.GetValues(typeof(Stage)).Length)-1;
            a.Add(randInt);
        }

        a.Add(Stage.BREAK_MODE);
        return a;
    }
}

public enum Stage
{
    START_STAGE,
    SPINNINGOBS_MODE,
    WORLDSPIN_MODE,
    DRUNK_MODE,
    NORMAL_MODE,
    BREAK_MODE
}