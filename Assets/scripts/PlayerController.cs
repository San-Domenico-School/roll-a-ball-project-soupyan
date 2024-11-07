using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI countText;
    private float speed; //push strength
    private float xDirection; //move left/right
    private float zDirection;//move front/ back
    private int count;
    public GameObject winTextObject;
    // Start is called before the first frame update
    void Start()
    {
        speed = 7;
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        MoveBall();
    }
    private void MoveBall() // usees input to move ball
    {
        xDirection = Input.GetAxis("Horizontal");
        zDirection = Input.GetAxis("Vertical");
        Vector3 Direction = new Vector3(xDirection, 0, zDirection);
        GetComponent<Rigidbody>().AddForce(Direction * speed);
    }
    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 23)
        {
            winTextObject.SetActive(true);
            
        }

    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        { 
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();


        }
    }
}



    // listens for player input



