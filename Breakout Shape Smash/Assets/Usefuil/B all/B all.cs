using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float xspeed = 5.0f;
    [SerializeField] float yspeed = 5.0f;
    public int power = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(xspeed, yspeed, 0) * Time.deltaTime;
    }

    public int GetPower()
    {
        return power;
    }

    public void increasePower(int value)
    {
        power += value;
    }
    public void increaseSpeed(int value)
    {
        xspeed += value;
        yspeed += value;
    }
    public void increaseSize(int value)
    {
        this.GetComponent<Transform>().localScale = this.GetComponent<Transform>().localScale * value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int rand = Random.Range(1, 4);
        if (collision.gameObject.tag == "side")
        {
            xspeed = xspeed *  -1;

        }
        else if (collision.gameObject.tag == "top" || collision.gameObject.tag == "Paddle")
        {
            yspeed = yspeed * -1;
        }
        else if(collision.gameObject.tag == "Block")
        {
            float ydif = transform.position.y - collision.transform.position.y;
            if(ydif < 0.45f && ydif > -0.45f) {
                xspeed = xspeed * -1;
            }
            else
            {
                yspeed = yspeed * -1;
                if (rand == 1)
                {
                    xspeed = xspeed * -1;
                }
            }
        }

        
    }
}
