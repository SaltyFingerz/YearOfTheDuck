using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerCam : MonoBehaviour
{
    Camera cam;

    GameObject playerObj;
    SpriteRenderer bgGradientSpr;
    GameObject bgCloudsObj;

    float fps = 1;

    float scrHeight;
    float scrWidth;

    Vector2 playerPos;
    Vector2 playerPosEase;

    int cloudOffsetWidth = 24;
    int cloudOffsetHeight = 16;

    int bgCloudsOffsetX = 0;
    int bgCloudsOffsetY = 0;

    // Start is called before the first frame update
    void Start()
    {
        //set cam
        cam = Camera.main;

        //gets player object and position
        playerObj = GameObject.Find("Player");
        playerPos = playerObj.transform.position;

        //gets gradient bg sprite renderer
        bgGradientSpr = GameObject.Find("GradientBG").GetComponent<SpriteRenderer>();

        //gets clouds bg object
        bgCloudsObj = GameObject.Find("CloudsBG");

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

    void setCloudPos()
    {
        bgCloudsObj.transform.position = new Vector3((playerPosEase.x / 2) + cloudOffsetWidth * bgCloudsOffsetX, (playerPosEase.y / 2) + cloudOffsetHeight * bgCloudsOffsetY, 0);
    }

    void checkCloudOffset()
    {
        if (transform.position.x - bgCloudsObj.transform.position.x > cloudOffsetWidth)
        {
            bgCloudsOffsetX += 1;
            setCloudPos();

            while (transform.position.x - bgCloudsObj.transform.position.x > cloudOffsetWidth)
            {
                bgCloudsOffsetX += 1;
                setCloudPos();
            }
        }

        if (transform.position.x - bgCloudsObj.transform.position.x < -cloudOffsetWidth)
        {
            bgCloudsOffsetX -= 1;
            setCloudPos();

            while (transform.position.x - bgCloudsObj.transform.position.x < -cloudOffsetWidth)
            {
                bgCloudsOffsetX -= 1;
                setCloudPos();
            }
        }

        if (transform.position.y - bgCloudsObj.transform.position.y > cloudOffsetHeight)
        {
            bgCloudsOffsetY += 1;
            setCloudPos();

            while (transform.position.y - bgCloudsObj.transform.position.y > cloudOffsetHeight)
            {
                bgCloudsOffsetY += 1;
                setCloudPos();
            }
        }

        if (transform.position.y - bgCloudsObj.transform.position.y < -cloudOffsetHeight)
        {
            bgCloudsOffsetY -= 1;
            setCloudPos();

            while (transform.position.y - bgCloudsObj.transform.position.y < -cloudOffsetHeight)
            {
                bgCloudsOffsetY -= 1;
                setCloudPos();
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //fps to delta time scaling for easing
        fps = 1f / Time.unscaledDeltaTime;

        //updates player pos
        playerPos = playerObj.transform.position;

        //sets player pos ease
        playerPosEase += (playerPos - playerPosEase) / (8 * (Mathf.Clamp(fps, 5, 999) / 60));

        //sets cam pos to player pos ease
        setCam(playerPosEase.x, playerPosEase.y);

        //sets gradient bg to playerpos ease (same as cam pos)
        bgGradientSpr.transform.position = playerPosEase;

        //sets size of gradient bg to camera/screen size
        setSizeOfGradientBG();

        //sets clouds bg pos and check offset stuff
        setCloudPos();
        checkCloudOffset();
    }
}
