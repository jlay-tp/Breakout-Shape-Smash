using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.AppUI.UI;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Rendering;
using static Unity.Collections.AllocatorManager;


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
    [SerializeField] GameObject item1;
    [SerializeField] GameObject item2;
    [SerializeField] GameObject item3;
    [SerializeField] GameObject ball1;
    [SerializeField] GameObject ball2;
    [SerializeField] GameObject ball3;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject restart;
    public static GameManager instance;
    int level = 1;
    int setupTimesx = 15;
    int setupTimesy = 3;
    int o1ind;
    int o2ind;
    int o3ind;
    int coins = 0;
    [SerializeField] TextMeshProUGUI coinText;
    float sizeFactorx = 1f;
    float sizeFactory = 1.5f;
    bool runOnceLevelOver = true;
    bool doubleNext = false;
    bool isGameOver = false;


    Upgrade[] upgrades = new Upgrade[12];
    GameObject[] items = new GameObject[9];
    int shopI1;
    int shopI2;
    int shopI3;
    GameObject[] balls = new GameObject[9];
    int shopB1;
    int shopB2;
    int shopB3;

    public enum appliedObject { PADDLE,  RANDOM_BALL, ALL_BALLS, NONE};
    public enum ballStats { SPEED, POWER, SIZE, NA};
    public enum paddleStats { LENGTH, POWER, NA };
    public enum other { NEW_BALL, COINS, ITEM, DOUBLE_NEXT, NA}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public class Upgrade
    {
        public appliedObject applied;
        public ballStats ballStats;
        public paddleStats paddleStats;
        public other other;
        public string text;
        public int value;
         
        public Upgrade(appliedObject appliedObject, string text, int value, ballStats bs, paddleStats ps, other o)
        {
            this.applied = appliedObject;
            this.ballStats = bs;
            this.paddleStats = ps;
            this.other = o;
            this.text = text;
            this.value = value;
        }
    }
    void Start()
    {
        paddle.SetActive(true);
        instance = this;

      
        upgrades[0] = new Upgrade(appliedObject.PADDLE, "Give the paddle +1 length", 1, ballStats.NA, paddleStats.LENGTH, other.NA);
        upgrades[1] = new Upgrade(appliedObject.RANDOM_BALL, "Give a random ball +2 speed", 2, ballStats.SPEED, paddleStats.NA, other.NA);
        upgrades[2] = new Upgrade(appliedObject.ALL_BALLS, "Give all balls +1 power", 1, ballStats.POWER, paddleStats.NA, other.NA);
        upgrades[3] = new Upgrade(appliedObject.NONE, "Double the value of the next upgrade", 2, ballStats.NA, paddleStats.NA, other.DOUBLE_NEXT);
        upgrades[4] = new Upgrade(appliedObject.NONE, "Gain 50 coins", 50, ballStats.NA, paddleStats.NA, other.COINS);
        upgrades[5] = new Upgrade(appliedObject.RANDOM_BALL, "Give a random ball +2 power", 2, ballStats.POWER, paddleStats.NA, other.NA);
        upgrades[6] = new Upgrade(appliedObject.RANDOM_BALL, "Give a random ball +2 size", 2, ballStats.SIZE, paddleStats.NA, other.NA);
        upgrades[7] = new Upgrade(appliedObject.PADDLE, "Give the paddle +1 to power", 1, ballStats.NA, paddleStats.POWER, other.NA);
        upgrades[8] = new Upgrade(appliedObject.ALL_BALLS, "Give all balls + 1 speed", 1, ballStats.SPEED, paddleStats.NA, other.NA);
        upgrades[9] = new Upgrade(appliedObject.ALL_BALLS, "Give all balls +1 size", 1, ballStats.SIZE, paddleStats.NA, other.NA);
        upgrades[10] = new Upgrade(appliedObject.NONE, "Instantly get a low level ball", 1, ballStats.NA, paddleStats.NA, other.NEW_BALL);
        upgrades[11] = new Upgrade(appliedObject.NONE, "Instantly get a low level item", 1, ballStats.NA, paddleStats.NA, other.ITEM);
        Setup();
        
            
        
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = coins.ToString();
        
       GameObject[] blocks =  GameObject.FindGameObjectsWithTag("Block");
        if(blocks.Length == 0)
        {
            if (runOnceLevelOver && !isGameOver)
            {
                foreach(GameObject ball in GameObject.FindGameObjectsWithTag("Ball")){
                    Destroy(ball);
                }
                LevelOver();
                runOnceLevelOver=false;
                
            }
            
        }
    }

    public int GetLevel()
    {
        return level;
    }

    public void Setup()
    {
        
        gameOver.SetActive(false);
        restart.SetActive(false);
        Vector3 padPos = paddle.GetComponent<Transform>().position;
        Instantiate(ball, new Vector3(padPos.x, padPos.y + 1, 0), Quaternion.identity);  //Spawn all balls here with foreach
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
        isGameOver = false;
    }

    public void ShopSetup() { 
       paddle.SetActive(false);
       NextLevel.SetActive(false);
       Shop.SetActive(false);
        item1.SetActive(true);
        item2.SetActive(true);
        item3.SetActive(true);
         ball1.SetActive(true);
        ball2.SetActive(true);
        ball3.SetActive(true);
        // item ind and ball ind may need to be declared globally
        int[] itemInd = new int[items.Length];
        for(int i = 0;i < itemInd.Length; i++)
        {
            itemInd[i] = i;
        }
        int[] ballInd = new int[balls.Length];
        for (int y = 0; y < balls.Length; y++) { ballInd[y] = y; }
        int ir1 = UnityEngine.Random.Range(0, itemInd.Length);
        shopI1 = itemInd[ir1];
        itemInd = fix(ir1, itemInd);
        int ir2 = UnityEngine.Random.Range(0, itemInd.Length);
        shopI2 = itemInd[ir2];
        itemInd = fix(ir2, itemInd);
        int ir3 = UnityEngine.Random.Range(0, itemInd.Length);
        shopI3 = itemInd[ir3];
        itemInd = fix(ir3, itemInd);
        int br1 = UnityEngine.Random.Range(0, ballInd.Length);
        shopB1 = ballInd[br1];
        ballInd = fix(br1, ballInd);
        int br2 = UnityEngine.Random.Range(0, ballInd.Length);
        shopB2 = ballInd[br2];
        ballInd = fix(br2, ballInd);
        int br3 = UnityEngine.Random.Range(0, ballInd.Length);
        shopB3 = ballInd[br3];
        ballInd = fix(br3, ballInd);

        // set text of text mesh pro ugui and text of button with coin amount 


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
        int r1 = UnityEngine.Random.Range(0, indices.Length);
        int rand1 = indices[r1];
        o1ind = rand1;
        indices = fix(r1, indices);
        int r2 = UnityEngine.Random.Range(0, indices.Length);
        int rand2 = indices[r2];
        o2ind = rand2;
        indices = fix(r2, indices);
        int r3 = UnityEngine.Random.Range(0, indices.Length);
        int rand3 = indices[r3];
        o3ind = rand3;

        paddle.SetActive(false);
        option1.SetActive(true);
        option1.GetComponent<TextMeshProUGUI>().text = upgrades[rand1].text;
        option2.SetActive(true);
        option2.GetComponent<TextMeshProUGUI>().text = upgrades[rand2].text;
        option3.SetActive(true);
        option3.GetComponent<TextMeshProUGUI>().text = upgrades[rand3].text;

        

    }

    public void Apply(int choice)
    {
        if (doubleNext)
        {
            doubleNext = false;
            Apply(choice);
        }
        int ind = choice;
        if (choice == 1)
        {
            ind = o1ind;
        }
        else if (choice == 2)
        {
            ind = o2ind;
        }
        else
        {
            ind = o3ind;
        }

        Upgrade upgrade = upgrades[ind];
        switch (upgrade.applied)
        {
            case appliedObject.ALL_BALLS:
                switch (upgrade.ballStats)
                {
                    case ballStats.POWER:
                        // find game objects with tag "Ball"
                        // for each increase its power stat by value
                        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
                        foreach (GameObject ball in balls)
                        {
                            ball.GetComponent<Ball>().increasePower(upgrade.value);
                        }
                        break;
                    case ballStats.SPEED:
                        // find game objects with tag "Ball"
                        // for each increase its speed stat by value
                        GameObject[] ballsSpeed = GameObject.FindGameObjectsWithTag("Ball");
                        foreach (GameObject ball in ballsSpeed)
                        {
                            ball.GetComponent<Ball>().increaseSpeed(upgrade.value);
                        }
                        break;
                    case ballStats.SIZE:
                        // find game objects with tag "Ball"
                        // get their transform component
                        // increase x and y scale by value
                        GameObject[] ballsSize = GameObject.FindGameObjectsWithTag("Ball");
                        foreach (GameObject ball in ballsSize)
                        {
                            ball.GetComponent<Ball>().increaseSize(upgrade.value);
                        }
                        break;
                }
                break;
            case appliedObject.RANDOM_BALL:
                switch (upgrade.ballStats)
                {
                    case ballStats.POWER:
                        // find game objects with tag "Ball"
                        // generate a random number between 0 and size of array and increase its power stat by value
                        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
                        int rand = UnityEngine.Random.Range(0, balls.Length);
                        balls[rand].GetComponent<Ball>().increasePower(upgrade.value);
                        break;
                    case ballStats.SPEED:
                        // find game objects with tag "Ball"
                        // generate a random number between 0 and size of array and increase its speed stat by value
                        GameObject[] ballsspeed = GameObject.FindGameObjectsWithTag("Ball");
                        int randspeed = UnityEngine.Random.Range(0, ballsspeed.Length);
                        ballsspeed[randspeed].GetComponent<Ball>().increaseSpeed(upgrade.value);
                        break;
                    case ballStats.SIZE:
                        // find game objects with tag "Ball"
                        // generate a random number between 0 and size of array and get their transform component
                        // increase x and y scale by value
                        GameObject[] ballssize = GameObject.FindGameObjectsWithTag("Ball");
                        int randsize = UnityEngine.Random.Range(0, ballssize.Length);
                        ballssize[randsize].GetComponent<Ball>().increaseSize(upgrade.value);
                        break;
                }
                break;
            case appliedObject.PADDLE:
                switch (upgrade.paddleStats)
                {
                    case paddleStats.POWER:
                        paddle.GetComponent<Paddle>().addPower(upgrade.value);
                        break;
                    case paddleStats.LENGTH:
                        Vector3 lScale = paddle.GetComponent<Transform>().localScale;
                        paddle.GetComponent<Transform>().localScale = new Vector3(lScale.x + upgrade.value, lScale.y, lScale.z);
                        break;
                }
                break;
            case appliedObject.NONE:
                switch (upgrade.other)
                {
                    case other.COINS:
                        // increase # of coins using coins variable in game manager
                        break;
                    case other.DOUBLE_NEXT:
                        doubleNext = true;
                        break;
                    case other.NEW_BALL:
                        // call the ball adding function from game manager or get a reference to whatever object adds balls to the user
                        break;
                    case other.ITEM:
                        // generate a random number and use it to pick a random item to give to the user
                        break;
                }
                break;
        }
        
        
            
    }
    

    public void UpdateCoins(int value)
    {
        coins += value;
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

    // actual game over method has to account for #of balls player has 
    public void GameOver()
    {
        isGameOver = true;
        coins = 0;
        paddle.SetActive (false );
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks)
        {
            block.SetActive(false );
        }
        gameOver.SetActive(true);
        restart.SetActive(true );
    }

 
    public void temporary()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks)
        {
            block.GetComponent<Block>().LoseLife(block.GetComponent<Block>().GetLives());
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            GameOver();

        }
    }
}
