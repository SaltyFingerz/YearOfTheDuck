using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBG : MonoBehaviour
{
    FollowerCam camScr;
    Camera cam;

    public float divider;
    public float moveSpd;

    int offsetWidth = 24;
    int offsetHeight = 16;

    int offsetX = 0;
    int offsetY = 0;

    float moveOffsetX = 0;

    // Start is called before the first frame update
    void Start()
    {
        //gets clouds bg object
        cam = Camera.main;
        camScr = cam.GetComponent<FollowerCam>();
    }

    void setCloudPos()
    {
        transform.position = new Vector3((camScr.playerPosEase.x / divider) + moveOffsetX + offsetWidth * offsetX, (camScr.playerPosEase.y / divider) + offsetHeight * offsetY, 0);
    }

    void checkCloudOffset()
    {
        if (moveOffsetX <= -24)
        {
            moveOffsetX += 24;
        }

        if (cam.transform.position.x - transform.position.x > offsetWidth)
        {
            offsetX += 1;
            setCloudPos();

            while (cam.transform.position.x - transform.position.x > offsetWidth)
            {
                offsetX += 1;
                setCloudPos();
            }
        }

        if (cam.transform.position.x - transform.position.x < -offsetWidth)
        {
            offsetX -= 1;
            setCloudPos();

            while (cam.transform.position.x - transform.position.x < -offsetWidth)
            {
                offsetX -= 1;
                setCloudPos();
            }
        }

        if (cam.transform.position.y - transform.position.y > offsetHeight)
        {
            offsetY += 1;
            setCloudPos();

            while (cam.transform.position.y - transform.position.y > offsetHeight)
            {
                offsetY += 1;
                setCloudPos();
            }
        }

        if (cam.transform.position.y - transform.position.y < -offsetHeight)
        {
            offsetY -= 1;
            setCloudPos();

            while (cam.transform.position.y - transform.position.y < -offsetHeight)
            {
                offsetY -= 1;
                setCloudPos();
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //sets clouds bg pos and check offset stuff
        if (moveSpd != 0)
        {
            moveOffsetX -= moveSpd / ((Mathf.Clamp(camScr.fps, 5, 999) / 60));
        }

        setCloudPos();
        checkCloudOffset();
    }
}
