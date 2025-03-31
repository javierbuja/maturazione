using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private Vector3 offset;
    private float zCoordinate;
    private bool isClicked = false;
    private float timer = 8f;

    private void OnMouseDown()
    {
        if (!isClicked)
        {
            isClicked = true;
            StartCoroutine(DestroyAfterTime());
        }

        Camera cam = Camera.main ?? FindObjectOfType<Camera>();
        if (cam == null)
        {
            Debug.LogError("Nessuna camera trovata!");
            return;
        }

        zCoordinate = cam.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPosition(cam);
    }

    private void OnMouseDrag()
    {
        Camera cam = Camera.main ?? FindObjectOfType<Camera>();
        if (cam == null) return;

        transform.position = GetMouseWorldPosition(cam) + offset;
    }

    private Vector3 GetMouseWorldPosition(Camera cam)
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoordinate;
        return cam.ScreenToWorldPoint(mousePoint);
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
