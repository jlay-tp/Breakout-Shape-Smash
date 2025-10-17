using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Ball ball;
    [SerializeField] Paddle paddle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 padPos = paddle.GetComponent<Transform>().position;
        Instantiate(ball, new Vector3(padPos.x, padPos.y + 1, 0) , Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
