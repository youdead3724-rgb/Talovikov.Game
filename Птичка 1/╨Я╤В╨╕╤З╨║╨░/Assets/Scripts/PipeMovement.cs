using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        transform.Translate(Vector3.left * (speed * Time.deltaTime), Space.World);
    }
}
