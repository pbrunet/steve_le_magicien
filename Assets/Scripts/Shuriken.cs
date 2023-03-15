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
        StartCoroutine(Respawn());
    }

    public void Launch(Vector3 speed) {
    
        if(balls[currentBall] != null)
        {
            balls[currentBall].GetComponent<Rigidbody2D>().velocity = speed;
            balls[currentBall].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; // Stop following the player
            balls[currentBall].transform.SetParent(null);
            balls[currentBall] = null;
        }
        currentBall = (currentBall + 1) % 3;
    }

    IEnumerator Respawn()
    {
        int ballToRespawn = 0;
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (balls[ballToRespawn] == null)
            {
                balls[ballToRespawn] = Instantiate(ball, gameObject.transform);
                balls[ballToRespawn].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; // Follow the player
                switch (ballToRespawn)
                {
                    case 0:
                        balls[ballToRespawn].transform.localPosition = new Vector3(5, 0, 0);
                        break;
                    case 1:
                        balls[ballToRespawn].transform.localPosition = new Vector3(-5, 0, 0);
                        break;
                    case 2:
                        balls[ballToRespawn].transform.localPosition = new Vector3(0, 5, 0);
                        break;
                }
                ballToRespawn = (ballToRespawn + 1) % 3;
            }
        }

    }
}
