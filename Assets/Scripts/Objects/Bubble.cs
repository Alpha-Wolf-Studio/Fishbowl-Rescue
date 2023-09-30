using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private static readonly int Next = Animator.StringToHash("Next");
    public float Speed { get; set; }

    [SerializeField] private Vector2 initialSpeed = new Vector2(1.0f, 5.0f);
    [SerializeField] private Vector2 initialScale = new Vector2(0.5f, 2.0f);
    [SerializeField] private List<AnimationCurve> animationsCurves = new List<AnimationCurve>();
    [SerializeField] private int currentCurveX = 0;
    [SerializeField] private int currentCurveZ = 0;
    [SerializeField] private float onTime = 0;
    [SerializeField] private float speedAnimation = 0;
    [SerializeField] private float maxTimeAnimation = 2;


    private void Update ()
    {
        Vector3 newPos = transform.localPosition;

        onTime += Time.deltaTime * speedAnimation;
        if (onTime > maxTimeAnimation)
        {
            onTime -= maxTimeAnimation;
            currentCurveX = Random.Range(0, animationsCurves.Count);
            currentCurveZ = Random.Range(0, animationsCurves.Count);
        }

        newPos.x = animationsCurves[currentCurveX].Evaluate(onTime / maxTimeAnimation);
        newPos.z = animationsCurves[currentCurveZ].Evaluate(onTime / maxTimeAnimation);

        newPos.y += Speed * Time.deltaTime;
        transform.localPosition = newPos;
    }

    public void Activate ()
    {
        gameObject.SetActive(true);

        Speed = Random.Range(initialSpeed.x, initialSpeed.y);
        float randomScale = Random.Range(initialScale.x, initialScale.y);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        maxTimeAnimation = Random.Range(0.5f, 2.0f);
        speedAnimation = Random.Range(0.5f, 2.0f);
        currentCurveX = Random.Range(0, animationsCurves.Count);
        currentCurveZ = Random.Range(0, animationsCurves.Count);
    }

    private void OnTriggerEnter (Collider other)
    {
        if (CompareTag("Limit"))
        {
            gameObject.SetActive(false);
        }
    }
}