using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    #region Public shit
    public string aiName;
    public Color primaryColor;
    #endregion

    #region Private shit
    //Components
    private EnemyController aiController;
    private GunController gunController;

    //AI info
    private int id = 0;
    private bool shooting = false;

    //AI stage shit
    private int aiStage = 0;
    private float stageTime = 0f;
    private float stageTimer = 0f;
    private bool startStageTimer = false;

    #endregion

    #region Getters/Setters
    public int ID { get => id; set => id = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        aiController = GetComponent<EnemyController>();
        gunController = GetComponent<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shooting)
            gunController.Shoot();
        if (startStageTimer)
        {
            if(stageTimer > 0)
                stageTimer -= Time.deltaTime;
            else
            {
                startStageTimer = false; aiStage++;
            }
        }

        if (aiName == "S.Spirit")
            SSpirit();
        else if (aiName == "Striker")
            Striker();
        else if (aiName == "Spreet")
            Spreet();
    }

    private void SSpirit()
    {
        /*
        if(aiController.CurPoint >= 170 && aiController.CurPoint < 180 && aiStage == 0) //Stop to shoot (Stage 0)
        {
            if (stageTime == 0) { stageTime = 1f; stageTimer = stageTime; }
            if (stageTimer >= 0)
            {
                aiController.Moving = false;
                gunController.Shoot();
                stageTimer -= Time.deltaTime;
            }
            else
            {
                Debug.Log(stageTimer.ToString("F2"));
                aiController.Moving = true;
                aiStage = 1;
                stageTime = 0;
            }
        }
        else if (aiController.CurPoint >= 210 && aiController.CurPoint < 315 && aiStage == 1) //Move and shoot (Stage 1)
        {
            gunController.Shoot();
        }
        else if (aiController.CurPoint >= 315 && aiStage == 1) //Increment stage after "Move and shoot" stage
        {
            aiStage = 2;
        }
        else if (aiController.CurPoint >= aiController.PathCreator.path.NumPoints - 5) //Destroy S.Spirit when it reaches the end of its path
        {
            Destroy(gameObject);
        }
        else //Move if nothing else todo
        {
            aiController.Moving = true;
        }
        */
    }

    private void Striker()
    {
        if(id == 0 || id == 1 || id == 2 || id == 3)
        {
            int curStage = aiController.CurStage;
            if (curStage == 0) //Move and shoot (start stage 0)
            {
                shooting = true;
            }
            else if (curStage == 1) //Move and Shoot (end stage 0)
            {
                shooting = false;
                aiStage = 1;
            }
            else if (curStage == 2 && !startStageTimer && aiStage == 1) //Stop and Shoot (start stage 1)
            {
                aiController.MovingForward = false;
                shooting = true;
                stageTime = 3f; stageTimer = stageTime; startStageTimer = true;
            }
            else if (curStage == 2 && aiStage == 2) //Stop and Shoot (end stage 1)
            {
                aiController.MovingForward = true;
                shooting = false;
            }
            else if (curStage == 3 && aiStage == 2) //Destroy enemy at the end of the path
            {
                KillAI();
                aiStage++;
            }
        }
    }

    private void Spreet()
    {
        //Bottom Spreet
        if (id == 0 || id == 1)
        {
            int curStage = aiController.CurStage;
            if (curStage == 0 && aiStage == 0) //Move and shoot forward (stage 0)
            {
                shooting = true;
                aiStage++;
            }
            else if (curStage == 1 && aiStage == 1 && !startStageTimer) //Wait for x seconds (stage 1)
            {
                aiController.MovingForward = false;
                shooting = false;
                stageTime = 2f; stageTimer = stageTime; startStageTimer = true;
            }
            else if (curStage == 1 && aiStage == 2) //Move and shoot backward (stage 2)
            {
                aiController.MovingBackward = true;
                shooting = true;
                aiStage++;
            }
            else if (curStage == 0 && aiStage == 3 && !startStageTimer) //Wait for x seconds (stage 3)
            {
                aiController.MovingBackward = false;
                shooting = false;
                stageTime = 2f; stageTimer = stageTime; startStageTimer = true;
            }
            else if (curStage == 0 && aiStage == 4) //Move and shoot forward (stage 4)
            {
                aiController.MovingForward = true;
                shooting = true;
                aiStage++;
            }
            else if (curStage == 1 && aiStage == 5 && !startStageTimer) //Wait for x seconds (stage 5)
            {
                aiController.MovingForward = false;
                shooting = false;
                stageTime = 1f; stageTimer = stageTime; startStageTimer = true;
            }
            else if (curStage == 1 && aiStage == 6) //Move forward and fuck off (stage 6)
            {
                aiController.MovingForward = true;
                shooting = true;
            }
            else if (curStage == 2)
            {
                KillAI();
            }
        }
    }

    private void KillAI()
    {
        aiController.MovingForward = false;
        aiController.MovingBackward = false;
        shooting = false;
        Destroy(gameObject, 2f);
    }
}