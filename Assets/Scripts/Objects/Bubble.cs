using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float Speed { get; set; }

    [SerializeField] private Vector2 initialSpeed = new Vector2(1.0f, 5.0f);
    [SerializeField] private Vector2 initialScale = new Vector2(0.5f, 2.0f);

    private void Update ()
    {
        Vector3 newPos = transform.position;
        newPos.y += Speed * Time.deltaTime;
        transform.localPosition = newPos;
    }

    public void Activate ()
    {
        gameObject.SetActive(true);

        Speed = Random.Range(initialSpeed.x, initialSpeed.y);
        float randomScale = Random.Range(initialScale.x, initialScale.y);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }

    private void OnTriggerEnter (Collider other)
    {
        if (CompareTag("Limit"))
        {
            gameObject.SetActive(false);
        }
    }
}