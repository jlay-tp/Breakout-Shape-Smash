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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.ToString());
        if (other.gameObject.tag == "side")
        {
            xspeed *= -1;

        }
        else if (other.gameObject.tag == "top")
        {
            yspeed = yspeed * -1;
        }
    }
}
