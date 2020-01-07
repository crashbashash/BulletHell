using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //Public shit
    public GameObject boundsGO;

    //Private shit
    #region Bools used to tell what the player is doing
    private bool paused = false;
    private bool shoot = false;
    private bool forward = false;
    private bool backward = false;
    private bool up = false;
    private bool down = false;
    #endregion

    #region Keycodes used to determine when a player wants to do something
    private KeyCode shootKey;
    private KeyCode forwardKey;
    private KeyCode backwardKey;
    private KeyCode upKey;
    private KeyCode downKey;
    #endregion

    #region Movement axis variables
    private float xAxis = 0;
    private float yAxis = 0;
    #endregion

    #region Map/Player Bounds shit
    private SpriteRenderer boundsSR;
    private GameObject leftBorder;
    private GameObject rightBorder;
    private GameObject topBorder;
    private GameObject botBorder;
    #endregion

    //Getters and Setters
    #region Getters/Getters for bools
    public bool Paused { get => paused; set => paused = value; }
    public bool Shoot { get => shoot; }
    public bool Forward { get => forward; }
    public bool Backward { get => backward; }
    public bool Up { get => up; }
    public bool Down { get => down; }
    #endregion

    #region Getters/Setters for keycodes
    public KeyCode ShootKey { set => shootKey = value; }
    public KeyCode ForwardKey { set => forwardKey = value; }
    public KeyCode BackwardKey { set => backwardKey = value; }
    public KeyCode UpKey { set => upKey = value; }
    public KeyCode DownKey { set => downKey = value; }
    #endregion

    #region Getters/Setters for movement axis
    public float XAxis { get => xAxis; }
    public float YAxis { get => yAxis; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        shootKey = KeyCode.Space;
        forwardKey = KeyCode.D;
        backwardKey = KeyCode.A;
        upKey = KeyCode.W;
        downKey = KeyCode.S;

        if (boundsGO != null)
            boundsSR = boundsGO.GetComponent<SpriteRenderer>();

        leftBorder = GameObject.Find("Left Border");
        rightBorder = GameObject.Find("Right Border");
        topBorder = GameObject.Find("Top Border");
        botBorder = GameObject.Find("Bot Border");
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInput(); //Check player input

        CheckMapBounds(); //Check if player is intersecting with map bounds

        CalculateMovement(); //Calculate the players movement on the x/y axis
    }

    private void CheckForInput()
    {
        #region Check if action keys pressed
        //Check if player is trying to shoot
        if (Input.GetKeyDown(shootKey))
            shoot = true;
        else if (Input.GetKeyUp(shootKey))
            shoot = false;
        #endregion

        #region Check if movement keys are pressed
        //Check if player is trying to move forward
        if (Input.GetKeyDown(forwardKey)
)
            forward = true;
        else if (Input.GetKeyUp(forwardKey))
            forward = false;

        //Check if player is trying to move backward
        if (Input.GetKeyDown(backwardKey))
            backward = true;
        else if (Input.GetKeyUp(backwardKey))
            backward = false;

        //Check if player is trying to move up
        if (Input.GetKeyDown(upKey))
            up = true;
        else if (Input.GetKeyUp(upKey))
            up = false;

        //Check if player is trying to move down
        if (Input.GetKeyDown(downKey))
            down = true;
        else if (Input.GetKeyUp(downKey))
            down = false;
        #endregion
    }

    private void CheckMapBounds()
    {
        Bounds leftBounds = leftBorder.GetComponent<SpriteRenderer>().bounds;
        Bounds rightBounds = rightBorder.GetComponent<SpriteRenderer>().bounds;
        Bounds topBounds = topBorder.GetComponent<SpriteRenderer>().bounds;
        Bounds botBounds = botBorder.GetComponent<SpriteRenderer>().bounds;
        Bounds pBounds = boundsSR.bounds;

        if (pBounds.Intersects(rightBounds))
            forward = false;
        else if (pBounds.Intersects(leftBounds))
            backward = false;
        else if (pBounds.Intersects(topBounds))
            up = false;
        else if (pBounds.Intersects(botBounds))
            down = false;
    }

    private void CalculateMovement()
    {
        xAxis = 0;
        yAxis = 0;
        #region Update movement axis
        if (!paused)
        {
            if (forward) //Check if moving forward
                xAxis += 1;
            if (backward) //Check if moving backward
                xAxis -= 1;
            if (up) //Check if moving up
                yAxis += 1;
            if (down) //Check if moving down
                yAxis -= 1;
        }
        #endregion
    }
}
