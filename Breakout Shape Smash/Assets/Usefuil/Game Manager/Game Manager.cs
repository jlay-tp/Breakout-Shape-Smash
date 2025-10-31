using TMPro;
using Unity.AppUI.UI;
using Unity.VisualScripting;
using UnityEngine;

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
    float sizeFactorx = 1f;
    float sizeFactory = 1.5f;
    bool runOnceLevelOver = true;
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

    public void LevelOver()
    {
        paddle.SetActive(false);
       option1.SetActive(true); 
        option2.SetActive(true);
        option3.SetActive(true);
        


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
        paddle.SetActive(true);
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
