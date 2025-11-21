using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int power = 0;
    public static Paddle instance;
    void Start()
    {
        instance = this;    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 convertedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(convertedPosition.x, transform.position.y, 0);
    }
    public int getPower()
    {
        return power;
    }

    public void addPower(int value)
    {
        power += value;
    }
}
