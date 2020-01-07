using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemyController : MonoBehaviour
{
    //Public shit
    [Range(0, 15)]
    public float speed;
    [Range(0,50)]
    public int health;

    //Private shit
    private bool movingForward;
    private bool movingBackward;
    private float distanceTravelled;
    private int curStage = -1;
    private PathCreator pathCreator;
    private EnemyAI enemyAI;
    private Transform[] stageGOList;

    public bool MovingForward { set => movingForward = value; }
    public bool MovingBackward { set => movingBackward = value; }
    public int CurStage { get => curStage; }
    public PathCreator PathCreator { get => pathCreator; set => pathCreator = value; }

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            if (GameObject.Find(enemyAI.aiName + enemyAI.ID + "_Path"))
            {
                pathCreator = GameObject.Find(enemyAI.aiName + enemyAI.ID + "_Path").GetComponent<PathCreator>();
                stageGOList = GameObject.Find(enemyAI.aiName + enemyAI.ID + "_Path").GetComponentsInChildren<Transform>();

                Transform[] stageListFormat = new Transform[stageGOList.Length - 1];
                for (int i = 0; i < stageListFormat.Length; i++)
                    stageListFormat[i] = stageGOList[i + 1];

                stageGOList = stageListFormat;

                UpdateCurrentStage(true);

                movingForward = true;
            }
            else
                Debug.LogError(string.Format("No path found for {0} ID {1}", enemyAI.aiName, enemyAI.ID));
        }
        else
            Debug.LogError(string.Format("No EnemyAI component found on {0} ID {1}", enemyAI.aiName, enemyAI.ID));
    }

    // Update is called once per frame
    void Update()
    {
        if(pathCreator != null && stageGOList != null && (movingForward||movingBackward))
        {
            UpdateCurrentStage(false);
        }
    }

    private void FixedUpdate()
    {
        if (pathCreator != null && stageGOList != null && (movingForward || movingBackward))
        {
            if (movingForward)
                distanceTravelled += speed * Time.deltaTime;
            else
                distanceTravelled -= speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        }
    }

    private void OnGUI()
    {
        if(stageGOList != null)
        {
            GUI.Label(new Rect(0, 0, 100, 50), "CurStage: " + curStage + "/" + (stageGOList.Length-1));
        }
    }

    private void UpdateCurrentStage(bool debug)
    {
        if(stageGOList != null)
        {
            int numStages = stageGOList.Length;
            for(int i = 0; i < numStages; i++)
            {
                Vector2 pointPos = stageGOList[i].position;
                if(Vector2.Distance(transform.position, pointPos) <= 1f)
                {
                    curStage = i;
                }
                if (debug)
                    Debug.Log(stageGOList[i].name);
            }
        }
    }
}
