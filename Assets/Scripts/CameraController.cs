using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    private Quaternion quaternion;
    private void Awake()
    {
        Instance = this;
        if (PlayerPrefs.HasKey("Rotation"))
        {
            quaternion = JsonUtility.FromJson<Quaternion>(PlayerPrefs.GetString("Rotation"));
            transform.rotation = quaternion;
        }

        if (!Input.gyro.enabled)
            Input.gyro.enabled = true;
    }

    public void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    void Update()
    {
        if (Input.gyro.enabled)
            transform.Rotate(-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y * 1.5f, Input.gyro.rotationRateUnbiased.z);

        quaternion = transform.rotation;
        PlayerPrefs.SetString("Rotation", JsonUtility.ToJson(quaternion));
    }
}