using UnityEngine;
using UnityEngine.UI;

public class SMController : MonoBehaviour
{
    private GameObject MainMenu;
    private GameObject ShopMenu;

    #region Shop UI Elements
    //Upgrade Titles
    Text U1T;
    Text U2T;
    Text U3T;
    Text U4T;

    //Upgrade Buttons
    Button U1B;
    Button U2B;
    Button U3B;
    Button U4B;

    //Upgrade Progress Text
    Text U1P;
    Text U2P;
    Text U3P;
    Text U4P;

    //Upgrade Cost Text
    Text U1C;
    Text U2C;
    Text U3C;
    Text U4C;
    #endregion

    private int curState;

    #region Upgrade Progress Variables
    //Upgrade progress
    private int U1_Progress = 0;
    private int U2_Progress = 0;
    private int U3_Progress = 0;
    private int U4_Progress = 0;

    //Uprade max progress
    private int U1_Max_Progress = 0;
    private int U2_Max_Progress = 0;
    private int U3_Max_Progress = 0;
    private int U4_Max_Progress = 0;

    //Upgrade Cost
    private int U1_Cost = 0;
    private int U2_Cost = 0;
    private int U3_Cost = 0;
    private int U4_Cost = 0;
    #endregion

    private void Awake()
    {
        MainMenu = GameObject.Find("MainMenu");
        ShopMenu = GameObject.Find("ShopMenu");

        //Upgrade Buttons
        U1B = GameObject.Find("U1_Button").GetComponent<Button>();
        U2B = GameObject.Find("U2_Button").GetComponent<Button>();
        U3B = GameObject.Find("U3_Button").GetComponent<Button>();
        U4B = GameObject.Find("U4_Button").GetComponent<Button>();

        //Upgrade Title
        U1T = GameObject.Find("U1_Title").GetComponent<Text>();
        U2T = GameObject.Find("U2_Title").GetComponent<Text>();
        U3T = GameObject.Find("U3_Title").GetComponent<Text>();
        U4T = GameObject.Find("U4_Title").GetComponent<Text>();

        //Upgrade Progress Text
        U1P = GameObject.Find("U1_Progress").GetComponent<Text>();
        U2P = GameObject.Find("U2_Progress").GetComponent<Text>();
        U3P = GameObject.Find("U3_Progress").GetComponent<Text>();
        U4P = GameObject.Find("U4_Progress").GetComponent<Text>();

        //Upgrade Cost Text
        U1C = GameObject.Find("U1_Price").GetComponent<Text>();
        U2C = GameObject.Find("U2_Price").GetComponent<Text>();
        U3C = GameObject.Find("U3_Price").GetComponent<Text>();
        U4C = GameObject.Find("U4_Price").GetComponent<Text>();
    }

    public void BackToMM()
    {
        GameObject.Find("Title").GetComponent<Text>().text = "Bullet Hell!!!";
        MainMenu.SetActive(true);
        ShopMenu.SetActive(false);
    }

    public void ChangeShopState(int state)
    {
        curState = state;
        switch (state)
        {
            case 0:
                U1_Progress = 1; U1_Max_Progress = 4; U1_Cost = 1000;
                U2_Progress = 2; U2_Max_Progress = 4; U2_Cost = 1500;
                U3_Progress = 1; U3_Max_Progress = 4; U3_Cost = 3000;
                U4_Progress = 2; U4_Max_Progress = 3; U4_Cost = 4000;
                SetupState0();
                break;
        }
    }

    private void SetupState0()
    {
        #region Setup Upgrade Titles
        U1T.text = "Damage";

        U2T.text = "Speed";

        U3T.text = "MultiShot";

        U4T.text = "MultiTarget";
        #endregion

        #region Setup Upgrade Progress
        U1P.text = U1_Progress + "/" + U1_Max_Progress;
        U2P.text = U2_Progress + "/" + U2_Max_Progress;
        U3P.text = U3_Progress + "/" + U3_Max_Progress;
        U4P.text = U4_Progress + "/" + U4_Max_Progress;
        #endregion

        #region Setup Upgrade Cost
        U1C.text = U1_Cost + "P";
        U2C.text = U2_Cost + "P";
        U3C.text = U3_Cost + "P";
        U4C.text = U4_Cost + "P";
        #endregion

        #region Setup Upgrade Buttons
        U1B.onClick.RemoveAllListeners();
        U1B.onClick.AddListener(() => { Upgrade("Damage"); });

        U2B.onClick.RemoveAllListeners();
        U2B.onClick.AddListener(() => { Upgrade("Speed"); });

        U3B.onClick.RemoveAllListeners();
        U3B.onClick.AddListener(() => { Upgrade("MultiShot"); });

        U4B.onClick.RemoveAllListeners();
        U4B.onClick.AddListener(() => { Upgrade("MultiTarget"); });
        #endregion
    }

    private void Upgrade(string toUpgrade)
    {
        #region Shop State 0
        if(toUpgrade == "Damage")
        {
            if(U1_Progress < U1_Max_Progress)
            {
                U1_Progress++;
                U1_Cost += 1000;
            }
        }
        else if(toUpgrade == "Speed")
        {
            if(U2_Progress < U2_Max_Progress)
            {
                U2_Progress++;
                U2_Cost += 600;
            }
        }
        else if(toUpgrade == "MultiShot")
        {
            if(U3_Progress < U3_Max_Progress)
            {
                U3_Progress++;
                U3_Cost += 1500;
            }
        }
        else if(toUpgrade == "MultiTarget")
        {
            if(U4_Progress < U4_Max_Progress)
            {
                U4_Progress++;
                U4_Cost += 2500;
            }
        }
        SetupState0();
        #endregion
    }
}
