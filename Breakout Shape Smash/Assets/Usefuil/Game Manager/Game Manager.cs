using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Ball ball;
    [SerializeField] Paddle paddle;
    [SerializeField] GameObject block;
    int level = 1;
    int setupTimesx = 15;
    int setupTimesy = 3;
    float sizeFactorx = 1f;
    float sizeFactory = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 padPos = paddle.GetComponent<Transform>().position;
        Instantiate(ball, new Vector3(padPos.x, padPos.y + 1, 0), Quaternion.identity);


        // Generate blocks
        Vector3 startingPos = new Vector3(-7.5f, 3.5f, 0);
        for(int j = 0; j <= setupTimesy; j++)
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
            
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LevelUp()
    {
        level++;
    }
}
