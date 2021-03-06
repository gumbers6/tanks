using System;
using UnityEngine;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{

    /////*******************************************/////
    /////                   VARS                    /////  
    /////*******************************************/////

    public Track trackLeft;
    public Track trackRight;

    public string keyMoveForward;
    public string keyMoveReverse;
    public string keyRotateRight;
    public string keyRotateLeft;
    public string shoot;

    bool moveForward = false;
    bool moveReverse = false;
    float moveSpeed = 0f;
    float moveSpeedReverse = 0f;
    float moveAcceleration = 0.1f;
    float moveDeceleration = 0.20f;
    float moveSpeedMax = 1.5f;

    // Health
    public int health = 5;
    public Image HealthBar;

    bool isShooting = false;
    bool rotateRight = false;
    bool rotateLeft = false;
    float rotateSpeedRight = 0f;
    float rotateSpeedLeft = 0f;
    float rotateAcceleration = 2.5f;
    float rotateDeceleration = 10f;
    float rotateSpeedMax = 130f;

    public GameObject bulletprefab;
    float TIMESINCESHOOTING = 0f;
    public GameObject shoot1;
    public GameObject shoot2;
    int alternating = 0;

    /////*******************************************/////
    /////                 UPDATE                    /////  
    /////*******************************************/////

    void Update()
    {

        TIMESINCESHOOTING += Time.deltaTime;
        rotateLeft = (Input.GetKeyDown(keyRotateLeft)) ? true : rotateLeft;
        rotateLeft = (Input.GetKeyUp(keyRotateLeft)) ? false : rotateLeft;
        if (rotateLeft)
        {
            rotateSpeedLeft = (rotateSpeedLeft < rotateSpeedMax) ? rotateSpeedLeft + rotateAcceleration : rotateSpeedMax;
        }
        else
        {
            rotateSpeedLeft = (rotateSpeedLeft > 0) ? rotateSpeedLeft - rotateDeceleration : 0;
        }
        transform.Rotate(0f, 0f, rotateSpeedLeft * Time.deltaTime);

        rotateRight = (Input.GetKeyDown(keyRotateRight)) ? true : rotateRight;
        rotateRight = (Input.GetKeyUp(keyRotateRight)) ? false : rotateRight;
        if (rotateRight)
        {
            rotateSpeedRight = (rotateSpeedRight < rotateSpeedMax) ? rotateSpeedRight + rotateAcceleration : rotateSpeedMax;
        }
        else
        {
            rotateSpeedRight = (rotateSpeedRight > 0) ? rotateSpeedRight - rotateDeceleration : 0;
        }
        transform.Rotate(0f, 0f, rotateSpeedRight * Time.deltaTime * -1f);

        moveForward = (Input.GetKeyDown(keyMoveForward)) ? true : moveForward;
        moveForward = (Input.GetKeyUp(keyMoveForward)) ? false : moveForward;
        if (moveForward)
        {
            moveSpeed = (moveSpeed < moveSpeedMax) ? moveSpeed + moveAcceleration : moveSpeedMax;
        }
        else
        {
            moveSpeed = (moveSpeed > 0) ? moveSpeed - moveDeceleration : 0;
        }
        transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);

        moveReverse = (Input.GetKeyDown(keyMoveReverse)) ? true : moveReverse;
        moveReverse = (Input.GetKeyUp(keyMoveReverse)) ? false : moveReverse;
        if (moveReverse)
        {
            moveSpeedReverse = (moveSpeedReverse < moveSpeedMax) ? moveSpeedReverse + moveAcceleration : moveSpeedMax;
        }
        else
        {
            moveSpeedReverse = (moveSpeedReverse > 0) ? moveSpeedReverse - moveDeceleration : 0;
        }
        transform.Translate(0f, moveSpeedReverse * Time.deltaTime * -1f, 0f);

        if (moveForward | moveReverse | rotateRight | rotateLeft)
        {
            trackStart();
        }
        else
        {
            trackStop();
        }


        isShooting = (Input.GetKeyDown(shoot)) ? true : isShooting;
        isShooting = (Input.GetKeyUp(shoot)) ? false : isShooting;
        if (isShooting && TIMESINCESHOOTING > 0.5f)
        {
            if(alternating % 2 == 0)
            {
                Instantiate(bulletprefab, shoot1.transform.position, shoot1.transform.rotation);
            }
            else
            {
                Instantiate(bulletprefab, shoot2.transform.position, shoot2.transform.rotation);
            }
            
            TIMESINCESHOOTING = 0;
            alternating++;
        }


    }

    /////*******************************************/////
    /////                METHODS                    /////  
    /////*******************************************/////

    void trackStart()
    {
        trackLeft.animator.SetBool("isMoving", true);
        trackRight.animator.SetBool("isMoving", true);
    }

    void trackStop()
    {
        trackLeft.animator.SetBool("isMoving", false);
        trackRight.animator.SetBool("isMoving", false);
    }

    public void Hit()
    {
        health -= 1;
        HealthBar.GetComponent<RectTransform>().localScale = new Vector3(health / 5f, 1,1);
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

}