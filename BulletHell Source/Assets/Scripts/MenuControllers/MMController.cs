using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMController : MonoBehaviour
{
    private GameObject MainMenu;
    private GameObject ShopMenu;
    private GameControl gameControl; 
    private Text HSTxt;

    private void Start()
    {
        GameObject.Find("Title").GetComponent<Text>().text = "Bullet Hell!!!";
        MainMenu = GameObject.Find("MainMenu");
        ShopMenu = GameObject.Find("ShopMenu");
        HSTxt = GameObject.Find("HighScore").GetComponent<Text>();
        gameControl = GameObject.Find("Game Control").GetComponent<GameControl>();
        ShopMenu.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if (MainMenu.activeSelf)
        {
            if(HSTxt != null) { HSTxt.text = "Highscore: " + gameControl.HighScore.ToString("F0"); }
        }
    }

    public void Shop()
    {
        GameObject.Find("Title").GetComponent<Text>().text = "Shop";
        MainMenu.SetActive(false);
        ShopMenu.SetActive(true);
        GetComponent<SMController>().ChangeShopState(0);
    }
}
