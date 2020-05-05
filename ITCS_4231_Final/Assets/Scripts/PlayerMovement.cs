using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    public Transform player;
    public Transform cam;
    float heading = 0;
    public int speed = 24;

    Vector2 input;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        heading += Input.GetAxis("Mouse X") * Time.deltaTime * 270;
        player.rotation = Quaternion.Euler(0, heading, 0);

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward = camForward.normalized;
        camRight = camRight.normalized;

        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }
        

        transform.position += (camForward * input.y + camRight * input.x) * Time.deltaTime * speed;
    }

    
}
