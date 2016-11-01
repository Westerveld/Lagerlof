using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + GameManager.gameSpeed * Time.deltaTime, Camera.main.transform.position.z);
    }

}
