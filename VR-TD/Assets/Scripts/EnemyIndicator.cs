using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    public Camera mainCamera;
    public RectTransform canvasRect;
    public GameObject arrowPrefab;

    private List<GameObject> enemies;
    private List<GameObject> arrows;

    private void Start()
    {
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        arrows = new List<GameObject>();

        foreach (var enemy in enemies)
        {
            GameObject arrow = Instantiate(arrowPrefab, canvasRect);
            arrow.SetActive(false);
            arrows.Add(arrow);
        }
        
        mainCamera = Camera.main;
        
        canvasRect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null) continue;

            Vector3 enemyPosition = enemies[i].transform.position;
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(enemyPosition);

            if (screenPosition.z > 0 && IsOnScreen(screenPosition))
            {
                arrows[i].SetActive(false);
            }
            else
            {
                arrows[i].SetActive(true);
                PositionArrow(arrows[i].GetComponent<RectTransform>(), screenPosition);
            }
        }
    }

    private bool IsOnScreen(Vector3 screenPosition)
    {
        return screenPosition.x >= 0 && screenPosition.x <= Screen.width &&
               screenPosition.y >= 0 && screenPosition.y <= Screen.height;
    }

    private void PositionArrow(RectTransform arrow, Vector3 screenPosition)
    {
        Vector2 clampedPosition = screenPosition;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, 50, Screen.width - 50);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, 50, Screen.height - 50);

        Vector3 direction = (screenPosition - new Vector3(Screen.width / 2, Screen.height / 2)).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.rotation = Quaternion.Euler(0, 0, angle - 90);

        arrow.anchoredPosition = clampedPosition - new Vector2(Screen.width / 2, Screen.height / 2);
    }
}
