using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController instance;
    public static int ticker;

    [SerializeField] GameObject fillPrefab;
    [SerializeField] Cell[] allCells;

    public static Action<string> slide;
    public int myScore;
    [SerializeField] Text scoreDisplay;

    int isGameOver;
    [SerializeField] GameObject gameOverPanel;
    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartSpawnFill();
        StartSpawnFill();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFill();
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            ticker = 0;
            isGameOver = 0;
            slide("w");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ticker = 0;
            isGameOver = 0;
            slide("a");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ticker = 0;
            isGameOver = 0;
            slide("s");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ticker = 0;
            isGameOver = 0;
            slide("d");
        }
    }
    public void SpawnFill()
    {
        bool isFull = true;
        for(int i=0; i < allCells.Length; i++)
        {
            if(allCells[i].fill==null)
            {
                isFull = false;
            }
        }
        if(isFull == true)
        {
            return;
        }

        int whichSpawn =UnityEngine.Random.Range(0, allCells.Length);
        if(allCells[whichSpawn].transform.childCount !=0)
            {
            Debug.Log(allCells[whichSpawn].name + "zaten dolduruldu");
            SpawnFill();
            return;
            }
        float chance = UnityEngine.Random.Range(0f, 1f);
        Debug.Log(chance);
        if(chance < .2f)
        {
            return;
        }
        else if (chance < .8f)
        {
            
            GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn].transform);
            Debug.Log(2);
            Fill tempFillComp = tempFill.GetComponent<Fill>();
            allCells[whichSpawn].GetComponent<Cell>().fill = tempFillComp; 
            tempFillComp.FillValueUpdate(2);
        }
        else
        {
            
            GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn].transform);
            Debug.Log(4);
            Fill tempFillComp = tempFill.GetComponent<Fill>();
            allCells[whichSpawn].GetComponent<Cell>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(4);
        }
    }

    public void StartSpawnFill()
    {
        int whichSpawn = UnityEngine.Random.Range(0, allCells.Length);
        if (allCells[whichSpawn].transform.childCount != 0)
        {
            Debug.Log(allCells[whichSpawn].transform.name + "zaten dolduruldu");
            SpawnFill();
            return;
        }

            GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn].transform);
            Debug.Log(2);
            Fill tempFillComp = tempFill.GetComponent<Fill>();
            allCells[whichSpawn].GetComponent<Cell>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(2);
        

           
        
    }

    public void ScoreUpdate(int scoreIn)
    {
        myScore += scoreIn;
        scoreDisplay.text = myScore.ToString();
    }

    public void GameOverCheck()
    {
        isGameOver++;
        if(isGameOver >=16)
        {
            gameOverPanel.SetActive(true);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}