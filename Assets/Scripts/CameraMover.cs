using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Vector3 DefaultLocation;
    public Quaternion DefaultRotation;
    public Vector3 OtherView;
    public Quaternion OtherRotation;
    public float moveDuration = 1f;
    private bool isMoving = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Move to OtherView and OtherRotation
    public void MoveToOtherView()
    {
        if (!isMoving)
        {
            StartCoroutine(AnimateTransform(DefaultLocation, DefaultRotation, OtherView, OtherRotation));
        }
    }

    // Move to DefaultLocation and DefaultRotation
    public void MoveToDefaultLocation()
    {
        if (!isMoving)
        {
            StartCoroutine(AnimateTransform(OtherView, OtherRotation, DefaultLocation, DefaultRotation));
        }
    }
    private System.Collections.IEnumerator AnimateTransform(Vector3 fromPosition, Quaternion fromRotation, Vector3 toPosition, Quaternion toRotation)
    {
        isMoving = true;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.localPosition = Vector3.Lerp(fromPosition, toPosition, elapsedTime / moveDuration);
            transform.localRotation = Quaternion.Slerp(fromRotation, toRotation, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Finalize position and rotation
        transform.localPosition = toPosition;
        transform.localRotation = toRotation;

        isMoving = false;
    }

}
