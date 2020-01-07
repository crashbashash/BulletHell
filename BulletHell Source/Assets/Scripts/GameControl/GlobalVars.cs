using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVars : MonoBehaviour
{
    public GameObject[] bullets = new GameObject[2];
    public GameObject[] enemies = new GameObject[2];

    private int cash = 0;

    public int Cash { get => cash; set => cash = value; }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Cash"))
            cash = PlayerPrefs.GetInt("Cash");
        else
            PlayerPrefs.SetInt("Cash", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
