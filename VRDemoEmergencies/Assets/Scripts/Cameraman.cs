using UnityEngine;

public class Cameraman : MonoBehaviour
{
    public Transform CameraHoldingPosition;
    private void Start()
    {
        TakeMainCamera();
    }

    private void TakeMainCamera()
    {
        Camera.main.transform.SetParent(CameraHoldingPosition);
        Camera.main.transform.localPosition = Vector3.zero;
        Camera.main.transform.localRotation = Quaternion.identity;

    }
    private void Update()
    {
        CopyDeviceRotation();
    }

    private void CopyDeviceRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, DeviceRotation.GetQuaternion(), 0.1f);
    }


    /// <summary>
    /// Returns the rotation angle of given device axis. Use Vector3.right to obtain pitch, Vector3.up for yaw and Vector3.forward for roll.
    /// This is for landscape mode. Up vector is the wide side of the phone and forward vector is where the back camera points to.
    /// </summary>
    /// <returns>A scalar value, representing the rotation amount around specified axis.</returns>
    /// <param name="axis">Should be either Vector3.right, Vector3.up or Vector3.forward. Won't work for anything else.</param>
    float GetAngleByDeviceAxis(Vector3 axis)
    {
        Quaternion deviceRotation = DeviceRotation.GetQuaternion();
        Quaternion eliminationOfOthers = Quaternion.Inverse(
            Quaternion.FromToRotation(axis, deviceRotation * axis)
        );
        Vector3 filteredEuler = (eliminationOfOthers * deviceRotation).eulerAngles;

        float result = filteredEuler.z;
        if (axis == Vector3.up)
        {
            result = filteredEuler.y;
        }
        if (axis == Vector3.right)
        {
            // incorporate different euler representations.
            result = (filteredEuler.y > 90 && filteredEuler.y < 270) ? 180 - filteredEuler.x : filteredEuler.x;
        }
        return result;
    }

    public static class DeviceRotation
    {
        private static bool gyroInitialized = false;

        public static bool HasGyroscope
        {
            get
            {
                return SystemInfo.supportsGyroscope;
            }
        }

        public static Quaternion GetQuaternion()
        {
            if (!gyroInitialized)
            {
                InitGyro();
            }

            return HasGyroscope
                ? ReadGyroscopeRotation()
                : Quaternion.identity;
        }

        private static void InitGyro()
        {
            if (HasGyroscope)
            {
                Input.gyro.enabled = true;                // enable the gyroscope
                Input.gyro.updateInterval = 0.0167f;    // set the update interval to it's highest value (60 Hz)
            }
            gyroInitialized = true;
        }

        private static Quaternion ReadGyroscopeRotation()
        {
            return new Quaternion(0.5f, 0.5f, -0.5f, 0.5f) * Input.gyro.attitude * new Quaternion(0, 0, 1, 0);
        }
    }
}
