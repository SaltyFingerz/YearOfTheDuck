using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerCam : MonoBehaviour
{
    Camera cam;

    GameObject playerObj;
    SpriteRenderer bgGradientSpr;
    player playerScr;

    public float fps = 1;

    float scrHeight;
    float scrWidth;

    Vector2 playerPos;
    public Vector2 playerPosEase;

    // Start is called before the first frame update
    void Start()
    {
        //set cam
        cam = Camera.main;

        //gets player object and position
        playerObj = GameObject.Find("Player");
        playerPos = playerObj.transform.position;
        playerScr = playerObj.GetComponent<player>();

        //gets gradient bg sprite renderer
        bgGradientSpr = GameObject.Find("GradientBG").GetComponent<SpriteRenderer>();

        //sets gradient bg sprite to camera/screen size
        bgGradientSpr.size = new Vector2(cam.pixelWidth, cam.orthographicSize * 2);

        //sets cam position to player pos
        setCam(playerPos.x, playerPos.y);

        //sets gradient bg to playerpos (same as cam pos)
        bgGradientSpr.transform.position = playerPos;


    }

    void setCam(float xPos, float yPos)
    {
        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }

    void setSizeOfGradientBG()
    {
        scrHeight = cam.orthographicSize * 2;
        scrWidth = (scrHeight / Screen.height) * Screen.width;

        bgGradientSpr.size = new Vector2(scrWidth, scrHeight);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //fps to delta time scaling for easing
        fps = 1f / Time.unscaledDeltaTime;

        //updates player pos
        if (playerScr.isAlive)
        {
            playerPos = playerObj.transform.position;
        }

        //sets player pos ease
        playerPosEase += (playerPos - playerPosEase) / (8 * (Mathf.Clamp(fps, 5, 999) / 60));

        //sets cam pos to player pos ease
        setCam(playerPosEase.x, playerPosEase.y);

        //sets gradient bg to playerpos ease (same as cam pos)
        bgGradientSpr.transform.position = playerPosEase;

        //sets size of gradient bg to camera/screen size
        setSizeOfGradientBG();
    }
}
