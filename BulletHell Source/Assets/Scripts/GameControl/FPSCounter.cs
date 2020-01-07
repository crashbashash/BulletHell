using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private int curFPS = 0;
    private int frames = 0;

    private float fpsTimer = 0;
    private float time2CalcFPS = 3;

    public int CurFPS { get => curFPS; }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(UpdateFrames());
        if(fpsTimer >= time2CalcFPS)
        {
            curFPS = Mathf.RoundToInt(frames / time2CalcFPS);
            frames = 0;
            fpsTimer = 0;
        }
        else
        {
            fpsTimer += Time.deltaTime;
        }
    }

    IEnumerator UpdateFrames()
    {
        yield return new WaitForEndOfFrame();
        frames++;
    }
}
