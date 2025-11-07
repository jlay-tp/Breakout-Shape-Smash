using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.AppUI.UI;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Rendering;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] GameObject paddle;
    [SerializeField] GameObject block;
    [SerializeField] GameObject NextLevel;
    [SerializeField] GameObject Shop;
    [SerializeField] GameObject option1;
    [SerializeField] GameObject option2;
    [SerializeField] GameObject option3;
    public static GameManager instance;
    int level = 1;
    int setupTimesx = 15;
    int setupTimesy = 3;
    int o1ind;
    int o2ind;
    int o3ind;
    float sizeFactorx = 1f;
    float sizeFactory = 1.5f;
    bool runOnceLevelOver = true;
    [SerializeField] GameObject u1;
    [SerializeField] GameObject u2;
    [SerializeField] GameObject u3;
    [SerializeField] GameObject u4;
    [SerializeField] GameObject u5;

    GameObject[] upgrades = new GameObject[5];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        paddle.SetActive(true);
        instance = this;
        


        Setup();
        
            
        
    }

    // Update is called once per frame
    void Update()
    {
        
       GameObject[] blocks =  GameObject.FindGameObjectsWithTag("Block");
        if(blocks.Length == 0)
        {
            if (runOnceLevelOver)
            {
                LevelOver();
                runOnceLevelOver=false;
            }
            
        }
    }

    public int GetLevel()
    {
        return level;
    }

    void Setup()
    {
        //Spawn all balls here with foreach 
        Vector3 padPos = paddle.GetComponent<Transform>().position;
        Instantiate(ball, new Vector3(padPos.x, padPos.y + 1, 0), Quaternion.identity);
        paddle.SetActive(true);
        Vector3 startingPos = new Vector3(-7.5f, 3.5f, 0);
        for (int j = 0; j <= setupTimesy; j++)
        {

            Instantiate(block, startingPos, Quaternion.identity);
            for (int i = 0; i < setupTimesx; i++)
            {
                startingPos = new Vector3(startingPos.x + sizeFactorx, startingPos.y, 0);
                Instantiate(block, startingPos, Quaternion.identity);
            }
            startingPos = startingPos + new Vector3(0, -1 * sizeFactory, 0);
            startingPos = new Vector3(-7.5f, startingPos.y, 0);
        }
        runOnceLevelOver = true;
    }

    public void ShopSetup() { 
       paddle.SetActive(false);
       NextLevel.SetActive(false);
       Shop.SetActive(false);
        
    }
    private int[] fix(int index, int[] prev)
    {
        int[] indices = new int[prev.Length - 1];
        int currentIndex = 0;

        for (int i = 0; i < prev.Length; i++)
        {
            if (i != index)
            {
                indices[currentIndex] = prev[i];
                currentIndex++;
            }
        }

        return indices;
    }
    public void LevelOver()
    {
        int[] indices = new int[upgrades.Length];
        for(int i = 0; i < upgrades.Length; i++)
        {
            indices[i] = i;
        }
        int rand1 = indices[UnityEngine.Random.Range(0, indices.Length)];
        o1ind = rand1;
        indices = fix(rand1, indices);
        int rand2 = indices[UnityEngine.Random.Range(0, indices.Length)];
        o2ind = rand2;
        indices = fix(rand2, indices);
        int rand3 = UnityEngine.Random.Range(0, indices.Length);
        o3ind = rand3;
        indices = fix(rand3, indices);

        paddle.SetActive(false);
        option1.SetActive(true);
        option1.GetComponent<TextMeshProUGUI>().text = upgrades[rand1].GetComponent<upgradeReal>().text;
        option2.SetActive(true);
        option2.GetComponent<TextMeshProUGUI>().text = upgrades[rand2].GetComponent<upgradeReal>().text;
        option3.SetActive(true);
        option3.GetComponent<TextMeshProUGUI>().text = upgrades[rand3].GetComponent<upgradeReal>().text;



    }

    public void Apply(int choice)
    {
        GameObject app = upgrades[0];
        if (choice == 1)
        {
           app = upgrades[o1ind];
        }
        else if (choice == 2)
        {
            app = upgrades[o2ind];
        }
        else
        {
            app = upgrades[o3ind];
        }

       
        
    }
    public void CompletedUpgrades()
    {
        Shop.SetActive(true);
        NextLevel.SetActive(true);
        option1.SetActive(false);
        option2.SetActive(false );
        option3.SetActive(false);
    }

   public void LevelUp()
    {
        
        Shop.SetActive(false);
        NextLevel.SetActive(false);
        paddle.SetActive(false ) ;
        level++;
        Setup();
    }


    public void temporary()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks)
        {
            block.GetComponent<Block>().LoseLife(block.GetComponent<Block>().GetLives());
        }
    }
}
