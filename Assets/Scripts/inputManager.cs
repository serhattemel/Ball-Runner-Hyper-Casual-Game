using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class inputManager : MonoBehaviour
{

    [SerializeField] public Image countDown;
    public bool coolingDown;
    public float waitTime = 4f;

    public int lane;
    private bool isMoving = false;
    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;
    [SerializeField] private float jumpVal, dashDist;
    public bool detectSwipeAfterRelease = false;
    public GameObject ball;
    public int dashCount;
    public Text DashCounter;
    public float SWIPE_THRESHOLD = 20f;


    private void Start()
    {
        dashDist = 20;
        dashCount = 3;
        lane = 0;
        dashCount = 3;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);
        isMoving = false;
    }
    void Update()
    {
        if (countDown.fillAmount == 0)
        {
            dashCount++;
            countDown.fillAmount = 1;
        }
        if (dashCount < 3&& countDown.fillAmount != 0)
        {   
            countDown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        }
        
        if (dashCount == 3)
        {
            countDown.gameObject.SetActive(false);
        }
        else
        {
            countDown.gameObject.SetActive(true);
        }
        
        DashCounter.text = dashCount.ToString();
        
        foreach (Touch touch in Input.touches)
        {
            
            if (touch.tapCount == 2)
            {
                if (dashCount>0&& isMoving==false)
                {
                    coolingDown = true;
                    isMoving = true;
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + dashDist);
                    dashCount--;
                    StartCoroutine(Wait());


                }
            }

            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPos = touch.position;
                fingerDownPos = touch.position;
            }

            //Detects Swipe while finger is still moving on screen
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeAfterRelease)
                {
                    fingerDownPos = touch.position;
                    DetectSwipe();
                }
            }

            //Detects swipe after finger is released from screen
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPos = touch.position;
                DetectSwipe();
            }
        }
    }

    void DetectSwipe()
    {

        if (isMoving == false && VerticalMoveValue() > SWIPE_THRESHOLD && VerticalMoveValue() > HorizontalMoveValue())
        {
            Debug.Log("Vertical Swipe Detected!");
            if (fingerDownPos.y - fingerUpPos.y > 0)
            {
                OnSwipeUp();
            }
            else if (fingerDownPos.y - fingerUpPos.y < 0)
            {
                OnSwipeDown();
            }
            fingerUpPos = fingerDownPos;

        }
        else if (isMoving == false && HorizontalMoveValue() > SWIPE_THRESHOLD && HorizontalMoveValue() > VerticalMoveValue())
        {
            Debug.Log("Horizontal Swipe Detected!");
            if (fingerDownPos.x - fingerUpPos.x > 0)
            {
                OnSwipeRight();
            }
            else if (fingerDownPos.x - fingerUpPos.x < 0)
            {
                OnSwipeLeft();
            }
            fingerUpPos = fingerDownPos;

        }
        else
        {
            Debug.Log("No Swipe Detected!");
        }
    }

    float VerticalMoveValue()
    {
        return Mathf.Abs(fingerDownPos.y - fingerUpPos.y);
    }

    float HorizontalMoveValue()
    {
        return Mathf.Abs(fingerDownPos.x - fingerUpPos.x);
    }

    void OnSwipeUp()
    {
        if (transform.position.y <= 0.8f)
        {
            Vector3 jump = new Vector3(0.0f, jumpVal, 0.0f);
            
            GetComponent<Rigidbody>().AddForce(jump);
            isMoving = true;
            StartCoroutine(Wait());
        }
    }

    void OnSwipeDown()
    {
        //Do something when swiped down
    }
    void OnSwipeLeft()
    {
        if (lane == 0)
        {
            ball.GetComponent<Animator>().Play("animasyon_1");
            //transform.position = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
            lane -= 1;
            isMoving = true;
            StartCoroutine(Wait());
        }
        else if(lane == 1)
        {
            ball.GetComponent<Animator>().Play("animasyon_3");
            //transform.position = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
            lane -= 1;
            isMoving = true;
            StartCoroutine(Wait());
        }
    }

    void OnSwipeRight()
    {
        if (lane == 0)
        {
            ball.GetComponent<Animator>().Play("animasyon_2");
            //transform.position = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z);
            lane += 1;
            isMoving = true;
            StartCoroutine(Wait());
        }
        else if (lane == -1)
        {
            ball.GetComponent<Animator>().Play("animasyon_4");
            //transform.position = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z);
            lane += 1;
            isMoving = true;
            StartCoroutine(Wait());
        }
    }
}
