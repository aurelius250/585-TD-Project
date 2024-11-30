using UnityEngine;

public class TurretPlacementManager : MonoBehaviour
{
    public GameObject turretPrefab;
    public LayerMask placementLayer;
    private GameObject turretPreview;
    private bool isPlacementMode = false;

    void Update()
    {
        if (isPlacementMode)
        {
            UpdatePreviewPosition();

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                PlaceTurret();
            }
        }
    }

    public void OnTurretIconTapped()
    {
        isPlacementMode = true;
        turretPreview = Instantiate(turretPrefab);
        turretPreview.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f); 
    }

    void UpdatePreviewPosition()
    {
        Vector2 touchPosition = Input.GetTouch(0).position;
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, placementLayer))
        {
            turretPreview.transform.position = hit.point;
        }
    }

    void PlaceTurret()
    {
        Instantiate(turretPrefab, turretPreview.transform.position, Quaternion.identity);
        Destroy(turretPreview);
        isPlacementMode = false;
    }
}
