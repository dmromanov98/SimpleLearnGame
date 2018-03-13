using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 100f;
    public Rigidbody rb;

	void FixedUpdate () {
        if (Input.GetKey("w"))
        {
            rb.AddForce(0, 0, speed * Time.deltaTime);
        }

        if (Input.GetKey("s"))
        {
            rb.AddForce(0, 0, -speed * Time.deltaTime);
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(-speed * Time.deltaTime, 0,0);
        }
        if (Input.GetKey("d"))
        {
            rb.AddForce(speed * Time.deltaTime, 0, 0);
        }
    }
}
