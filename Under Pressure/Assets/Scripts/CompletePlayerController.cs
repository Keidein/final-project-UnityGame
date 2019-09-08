using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class CompletePlayerController : MonoBehaviour
{
    public float speed;                //Floating point variable to store the player's movement speed.
    public int crowd_panic_rate;       //Controls the rate at which the crowd enemy raises 
    public int pick_up_restore;
    public int salesman_panic_rate;

    public Text anxiexty_text;          //Stores the anxiety ratio text.
    public GameObject breathing_icon;
    public GameObject music_icon;
    public GameObject stress_icon;

    private Rigidbody2D rb2d;
    public int anxiety;
    [SerializeField] private health_bar_controller healthBar;

    private bool stress_ball_obtained; //checks to see if the player has obtained the stress ball
    private bool breathing; //checks to see if the player is currently breathing
    private bool music_ready; //checks to see if the music player is on cooldown

    //check to see that all the tasks in the first level have been completed.
    private bool check_one_ready;
    private bool check_two_ready;
    private bool check_three_ready;
    private bool check_four_ready;
    //check to see if the player is in the appropiate zones.
    private bool in_zone_one;
    private bool in_zone_two;
    private bool in_zone_three;
    private bool in_zone_four;

    //ready to leave
    private bool exit_ready;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        anxiety = 50;       
        pick_up_restore = 15;
        stress_ball_obtained = false;
        breathing = false;
        music_ready = true;

        //set up the booleans for the first level tasks
        //check to see that all the tasks in the first level have been completed.
        check_one_ready = false;
        check_two_ready = false;
        check_three_ready = false;
        check_four_ready = false;
        //check to see if the player is in the appropiate zones.
        in_zone_one = false;
        in_zone_two = false;
        in_zone_three = false;
        in_zone_four = false;
        //check to see if the player is on the exit door
        exit_ready = false;

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
        //ABILITY KEY INPUTS
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

        //Check First Level Tasks
        if (Input.GetKeyDown("c") && in_zone_one == true)
        {
            print("1");
            check_one_ready = true;
        }
        else if (Input.GetKeyDown("c") && in_zone_two == true)
        {
            print("2");
            check_two_ready = true;
        }
        else if (Input.GetKeyDown("c") && in_zone_three == true)
        {
            print("3");
            check_three_ready = true;
        }
        else if (Input.GetKeyDown("c") && in_zone_four == true)
        {
            print("4");
            check_four_ready = true;
        }

        //Finish first level
        if (Input.GetKeyDown("c") && check_one_ready == true && check_two_ready == true && check_three_ready == true && check_four_ready == true && exit_ready == true)
        {
            FindObjectOfType<GameManager>().EndGame();
        }

        //UI INFORMATION
        if (stress_ball_obtained == true)
        {
            stress_icon.SetActive(true);
        } else if (stress_ball_obtained == false)
        {
            stress_icon.SetActive(false);
        } if (music_ready == false)
        {
            music_icon.SetActive(false);
        } if (breathing == true)
        {
            breathing_icon.SetActive(false);
        }
        else if (breathing == false)
        {
            breathing_icon.SetActive(true);
        }

        //END THE GAME IF ANXIETY REACHES 100
        //ALSO PREVENTS ANXIETY FROM GOING OVER 100
        if (anxiety >= 100)
        {
            anxiety = 100;
            SetAnxietyText();
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check through to see what the player is colliding with
        //And also begin the code for each seperate pickup / enemy
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
        else if (other.gameObject.CompareTag("check_point_one"))
        {
            in_zone_one = true;
        }
        else if (other.gameObject.CompareTag("check_point_two"))
        {
            in_zone_two = true;
        }
        else if (other.gameObject.CompareTag("check_point_three"))
        {
            in_zone_three = true;
        }
        else if (other.gameObject.CompareTag("check_point_four"))
        {
            in_zone_four = true;
        } else if (other.gameObject.CompareTag("exit_door"))
        {
            exit_ready = true;
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
        } else if (other.gameObject.CompareTag("check_point_one"))
        {
            in_zone_one = false;
        }
        else if (other.gameObject.CompareTag("check_point_two"))
        {
            in_zone_two = false;
        }
        else if (other.gameObject.CompareTag("check_point_three"))
        {
            in_zone_three = false;
        }
        else if (other.gameObject.CompareTag("check_point_four"))
        {
            in_zone_four = false;
        } else if (other.gameObject.CompareTag("exit_door"))
        {
            exit_ready = false;
        }
    }

    void music_cooldown_complete()
    {
        music_ready = true;
        music_icon.SetActive(true);
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

    //dictate behaviour for when player is within crush hitbox
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

    //dictate behaviour for when player is within crush hitbox
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