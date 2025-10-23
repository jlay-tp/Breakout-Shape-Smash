using UnityEngine;

public class Block : MonoBehaviour
{
    int lives = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void LoseLife(int num)
    {
        lives = lives - num;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            LoseLife(collision.gameObject.GetComponent<Ball>().GetPower());
        }
    }
}
