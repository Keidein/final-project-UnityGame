using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Dylans code
    // Importing the movement script.
    public Movement movement;

    // Ethans code
    #region Ethans Variables
    public int crowd_panic_rate;        //Controls the rate at which the crows enemy raises
    public int pick_up_restore = 15;
    public int salesman_panic_rate;
    public int anxiety;

    public Text anxiety_text;           //Stores the anxiety ratio text.
    public GameObject breathing_icon;
    public GameObject music_icon;
    public GameObject stress_icon;

    [SerializeField] private health_bar_controller healthBar;

    private bool stress_ball_obtained;  //checks to see if the player has obtained the stress ball
    private bool breathing;             //checks to see if the player is currently breathing
    private bool music_ready;           //checks to see if the music player is on cooldown

    //check to see that all the tasks in the first level have been completed.
    private bool check_one_ready = false;
    private bool check_two_ready = false;
    private bool check_three_ready = false;
    private bool check_four_ready = false;

    //check to see if the player is in the appropiate zones.
    private bool in_zone_one = false;
    private bool in_zone_two = false;
    private bool in_zone_three = false;
    private bool in_zone_four = false;

    //ready to leave
    private bool exit_ready;
    #endregion

    // Taken from Ethans code, but modified to reduce line count.
    #region Ethans update method
    void Update()
    {
        //ABILITY KEY INPUTS
        if (Input.GetKeyDown("space") && stress_ball_obtained)
        {
            anxiety -= 40;
            stress_ball_obtained = false;
            SetAnxietyText();
        }

        if (Input.GetKeyDown("z") && !breathing)
        {
            StartCoroutine("breathing_routine");
        } else if (Input.GetKeyDown("z") && breathing)
        {
            StopCoroutine("breathing_routine");
            breathing = false;
        }

        if (Input.GetKeyDown("x") && music_ready)
        {
            music_ready = false;
            anxiety -= 20;
            Invoke("music_cooldown_complete", 5);
            SetAnxietyText();
        }

        //Check First Level Tasks
        if (Input.GetKeyDown("c") && in_zone_one) check_one_ready = true;
        else if (Input.GetKeyDown("c") && in_zone_two) check_two_ready = true;
        else if (Input.GetKeyDown("c") && in_zone_three) check_three_ready = true;
        else if (Input.GetKeyDown("c") && in_zone_four) check_four_ready = true;

        //Finish first level
        if (Input.GetKeyDown("c") && check_one_ready && check_two_ready && check_three_ready && check_four_ready)
        {
            FindObjectOfType<GameManager>().EndGame();
        }

        //UI INFORMATION
        if (stress_ball_obtained) stress_icon.SetActive(true);
        else if (!stress_ball_obtained) stress_icon.SetActive(false);

        if (!music_ready) music_icon.SetActive(false);

        if (breathing) breathing_icon.SetActive(false);
        else if (!breathing) breathing_icon.SetActive(true);

        //END THE GAME IF ANXIETY REACHES 100
        //ALSO PREVENTS ANXIETY FROM GOING OVER 100
        if (anxiety >= 100)
        {
            anxiety = 100;
            SetAnxietyText();
            FindObjectOfType<GameManager>().EndGame();
        }
    }
    #endregion

    #region Dylans FixedUpdate() so the player can move.
    void FixedUpdate()
    {
        if (!breathing) // Taken from Ethans code, don't run the code inside if the player is breathing.
        {
            // Fixed directional input.
            if (Input.GetKey("w"))
            {
                movement.MoveUp();
            }

            if (Input.GetKey("s"))
            {
                movement.MoveDown();
            }

            if (Input.GetKey("d"))
            {
                movement.MoveRight();
            }

            if (Input.GetKey("a"))
            {
                movement.MoveLeft();
            }

            // Diagonal movement input.
            if (Input.GetKey("w") && Input.GetKey("d"))
            {
                movement.UpRight();
            }

            if (Input.GetKey("w") && Input.GetKey("a"))
            {
                movement.UpLeft();
            }

            if (Input.GetKey("s") && Input.GetKey("d"))
            {
                movement.DownRight();
            }

            if (Input.GetKey("s") && Input.GetKey("a"))
            {
                movement.DownLeft();
            }
        }
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("good_pickup"))
        {
            anxiety -= pick_up_restore;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("stress_ball"))
        {
            stress_ball_obtained = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("crowd")) StartCoroutine("crowd_panic");

        if (other.gameObject.CompareTag("bad_pickup"))
        {
            anxiety -= pick_up_restore;
            Destroy(other.gameObject);
            Invoke("unhealthy_pickup", 3);
        }

        if (other.gameObject.CompareTag("salesman")) StartCoroutine("salesman_panic");
        if (other.gameObject.CompareTag("friends")) StartCoroutine("friends_panic");
        if (other.gameObject.CompareTag("crush"))
        {
            StartCoroutine("crush_panic");
            StartCoroutine("crush_panic_two");
        }

        if (other.gameObject.CompareTag("bully")) anxiety += 40;

        if (other.gameObject.CompareTag("check_point_one")) in_zone_one = true;
        if (other.gameObject.CompareTag("check_point_two")) in_zone_two = true;
        if (other.gameObject.CompareTag("check_point_three")) in_zone_three = true;
        if (other.gameObject.CompareTag("check_point_four")) in_zone_four = true;
        if (other.gameObject.CompareTag("exit_door")) exit_ready = true;
    }

    void OnTriggerExit(Collider other)
    {
        //Check through to see what the player is colliding with
        //And also begin the code for each seperate pickup / enemy
        if (other.gameObject.CompareTag("crowd")) StopCoroutine("crowd_panic");
        if (other.gameObject.CompareTag("salesman")) StopCoroutine("salesman_panic");
        if (other.gameObject.CompareTag("crush"))
        {
            StopCoroutine("crush_panic");
            StopCoroutine("crush_panic_two");
        }
        if (other.gameObject.CompareTag("friends")) StopCoroutine("Friends_panic");
        if (other.gameObject.CompareTag("check_point_one")) in_zone_one = false;
        if (other.gameObject.CompareTag("check_point_two")) in_zone_two = false;
        if (other.gameObject.CompareTag("check_point_three")) in_zone_three = false;
        if (other.gameObject.CompareTag("check_point_four")) in_zone_four = false;
        if (other.gameObject.CompareTag("exit_door")) exit_ready = false;
    }

    #region Ethans functions
    #region Functions for the UI and pickups.
    void music_cooldown_complete()
    {
        music_ready = true;
        music_icon.SetActive(true);
    }

    void unhealthy_pickup()
    {
        anxiety += 40;
        SetAnxietyText();
    }

    void SetAnxietyText()
    {
        if (anxiety < 0) anxiety = 0;
        anxiety_text.text = anxiety.ToString() + "%";
    }
    #endregion

    #region IEnumerators
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
    /*IEnumerator friends_panic()
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
    }*/

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
    #endregion
    #endregion
}
