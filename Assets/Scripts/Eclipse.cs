using System.Collections.Generic;
using UnityEngine;

public class Eclipse : MonoBehaviour
{
    private const float ECLIPSE_MAX_RADIUS = 6f;

    [SerializeField] private List<GameObject> triggers;

    [SerializeField] private Transform sun;
    [SerializeField] private Transform moon;

    private void FixedUpdate()
    {
        var direction = (moon.position - sun.position).normalized;
        transform.forward = direction;
    }

    public void SetActiveTriggers(bool value)
    {
        foreach (var trigger in triggers)
        {
            trigger.SetActive(value);
        }
    }
    
    public float GetQuality(Vector3 _playerPos)
    {
        var playerPos = new Vector2(_playerPos.x, _playerPos.y);
        var eclipsePos = new Vector2(transform.position.x, transform.position.y);
        var distance = Vector2.Distance(playerPos, eclipsePos);

        return ECLIPSE_MAX_RADIUS / distance;
    }
}
