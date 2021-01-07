using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    private float _launchForce = 500;
    [SerializeField]
    private float _maxDragDistance = 5;

    private Vector2 _startPosition;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRanderer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRanderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
    }

    private void OnMouseDown()
    {
        _spriteRanderer.color = Color.red;
    }

    private void OnMouseUp()
    {
        var currentPosition = _rigidbody2D.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();

        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(direction * _launchForce);

        _spriteRanderer.color = Color.white;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance > _maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + direction * _maxDragDistance;
        }

        if (desiredPosition.x > _startPosition.x)
            desiredPosition.x = _startPosition.x;

        _rigidbody2D.position = desiredPosition;
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);

        _rigidbody2D.position = _startPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
    }
}
