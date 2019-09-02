using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class CompletePlayerController : MonoBehaviour
{

    public float speed;                //Floating point variable to store the player's movement speed.
    public Text anxiexty_text;          //Stores the anxiety ratio text.
    public int crowd_panic_rate;       //Controls the rate at which the crowd enemy raises 
    public int pick_up_restore;
    public int salesman_panic_rate;
    

    private Rigidbody2D rb2d;
    private int anxiety;

    private bool stress_ball_obtained; //checks to see if the player has obtained the stress ball
    private bool breathing; //checks to see if the player is currently breathing
    private bool music_ready; //checks to see if the music player is on cooldown



    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        anxiety = 50;
        pick_up_restore = 15;
        stress_ball_obtained = false;
        breathing = false;
        salesman_panic_rate = 10;
        music_ready = true;


        SetAnxietyText();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {

        if (breathing == false)
        {
            //Store the current horizontal input in the float moveHorizontal.
            float moveHorizontal = Input.GetAxis("Horizontal");

            //Store the current vertical input in the float moveVertical.
            float moveVertical = Input.GetAxis("Vertical");

            //Use the two store floats to create a new Vector2 variable movement.
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

            //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
            rb2d.AddForce(movement * speed);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (stress_ball_obtained == true)
            {
                anxiety = anxiety - 40;
                stress_ball_obtained = false;
                SetAnxietyText();
            }
            else
            {
                print("space key was pressed");
            }
        }
        else if (Input.GetKeyDown("z") && breathing == false)
        {
            StartCoroutine("breathing_routine");
        }
        else if (Input.GetKeyDown("z") && breathing == true)
        {
            StopCoroutine("breathing_routine");
            breathing = false;
        }
        else if (Input.GetKeyDown("x") && music_ready == true)
        {
            music_ready = false;
            anxiety = anxiety - 20;
            Invoke("music_cooldown_complete", 5);
            SetAnxietyText();
        }





        if (anxiety == 100)
        {
            FindObjectOfType<GameManager>().EndGame();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("good_pickup"))
        {
            anxiety = anxiety - pick_up_restore;
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("stress_ball"))
        {
            stress_ball_obtained = true;
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("crowd"))
        {
            StartCoroutine("crowd_panic");
        }
        else if (other.gameObject.CompareTag("bad_pickup"))
        {
            anxiety = anxiety - pick_up_restore;
            other.gameObject.SetActive(false);
            Invoke("unhealthy_pickup", 3);
        }
        else if (other.CompareTag("salesman"))
        {
            StartCoroutine("salesman_panic");
        }
        else if (other.CompareTag("friends"))
        {
            StartCoroutine("friends_panic");
        }
        else if (other.CompareTag("crush"))
        {
            StartCoroutine("crush_panic");
            StartCoroutine("friends_panic");
        }
        else if (other.CompareTag("bully"))
        {
            anxiety = anxiety + 40;
        }

        SetAnxietyText();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("crowd"))
        {
            StopCoroutine("crowd_panic");
        }
        else if (other.CompareTag("salesman"))
        {
            StopCoroutine("salesman_panic");
        } else if (other.CompareTag("crush"))
        {
            StopCoroutine("crush_panic");
            StopCoroutine("friends_panic");
        } else if (other.gameObject.CompareTag("friends"))
        {
            StopCoroutine("friends_panic");
        }
    }

    void music_cooldown_complete()
    {
        music_ready = true;
    }

    void unhealthy_pickup()
    {
        anxiety = anxiety + 40;
        SetAnxietyText();
    }

    void SetAnxietyText()
    {
        if (anxiety < 0)
        {
            anxiety = 0;
        }
        anxiexty_text.text = anxiety.ToString() + "%";
    }

    void SetAnxietyTextBad()
    {
        anxiexty_text.text = anxiety.ToString() + "%";
    }



    IEnumerator breathing_routine()
    {
        for (int current_anxiety = anxiety; current_anxiety >= 1; current_anxiety -= 1)
        {
            breathing = true;
            anxiety = current_anxiety;
            SetAnxietyText();
            yield return new WaitForSeconds(.3f);
        }
    }




    IEnumerator crowd_panic()
    {
        for (int current_anxiety = anxiety; current_anxiety <= 100; current_anxiety += crowd_panic_rate)
        {
            anxiety = current_anxiety;
            SetAnxietyText();
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator salesman_panic()
    {
        for (int current_anxiety = anxiety; current_anxiety <= 100; current_anxiety += salesman_panic_rate)
        {
            anxiety = current_anxiety;
            SetAnxietyText();
            yield return new WaitForSeconds(0.8f);
        }
    }

    IEnumerator boss_panic()
    {
        for (int current_anxiety = anxiety; current_anxiety <= 100; current_anxiety += 1)
        {
            anxiety = current_anxiety;
            SetAnxietyText();
            yield return new WaitForSeconds(.3f);
        }
    }

    IEnumerator friends_panic()
    {
        for (float current_speed = speed; current_speed <= 100; current_speed -= 0.1f)
        {
            speed = current_speed;
            if (speed <= 2.0f)
            {
                speed = 2.0f;
            }
            yield return new WaitForSeconds(.1f);
        }
    }


    IEnumerator crush_panic()
    {
        for (int current_anxiety = anxiety; current_anxiety <= 100; current_anxiety += 1)
        {
            anxiety = current_anxiety;
            SetAnxietyText();
            yield return new WaitForSeconds(0.6f);
        }
    }

}