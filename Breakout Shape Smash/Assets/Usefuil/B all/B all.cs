using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float xspeed = 5.0f;
    [SerializeField] float yspeed = 5.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(xspeed, yspeed, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
            if(ydif < 2 && ydif > -2) {
                xspeed = xspeed * -1;
            }
            else
            {
                yspeed = yspeed * -1;
            }
        }
    }
}
