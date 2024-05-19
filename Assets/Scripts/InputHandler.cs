using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 1000f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _maxDistance) && hit.collider.TryGetComponent(out Cube cube))
            {
                StartCoroutine(cube.Segmentation());
            }
        }
    }
}
