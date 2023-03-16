using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public MagicBall ball;

    private MagicBall[] balls = new MagicBall[3];
    private int currentBall = 0;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Respawn", 1);
        Invoke("Respawn", 2);
        Invoke("Respawn", 3);
    }

    public void Launch(Vector3 speed)
    {

        for (int i = 0; i < 3; i++)
        {
            currentBall = (currentBall + 1) % 3;
            if (balls[currentBall] != null)
            {
                balls[currentBall].GetComponent<Rigidbody2D>().velocity = speed;
                balls[currentBall].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; // Stop following the player
                balls[currentBall].GetComponent<Collider2D>().enabled = true;
                balls[currentBall].transform.SetParent(null);
                balls[currentBall] = null;
                Invoke("Respawn", 1);
                break;
            }
        }
    }

    void Respawn()
    {
        for (int i = 0; i < 3; i++)
        {
            int ballToRespawn = currentBall;
            if (balls[ballToRespawn] == null)
            {
                balls[ballToRespawn] = Instantiate(ball, gameObject.transform);
                balls[ballToRespawn].gameObject.layer = LayerMask.NameToLayer("PlayerProjectile");
                balls[ballToRespawn].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; // Follow the player
                balls[ballToRespawn].GetComponent<Collider2D>().enabled = false;
                switch (ballToRespawn)
                {
                    case 0:
                        balls[ballToRespawn].transform.localPosition = new Vector3(5, 0, 0);
                        break;
                    case 1:
                        balls[ballToRespawn].transform.localPosition = new Vector3(-5, 0, 0);
                        break;
                    case 2:
                        balls[ballToRespawn].transform.localPosition = new Vector3(0, 10, 0);
                        break;
                }
                break;
            }
            ballToRespawn = (ballToRespawn + 1) % 3;
        }

    }
}
