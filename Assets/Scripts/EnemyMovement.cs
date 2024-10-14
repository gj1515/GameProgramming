using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float rotationChangeInterval = 0.5f; // ȸ�� ���� ���� (��)

    private float rotationTimer;

    // Start is called before the first frame update
    void Start()
    {
        rotationTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        rotationTimer += Time.deltaTime;

        // rotationChangeInterval���� Y���� �������� ȸ���� �����ϰ� ����
        if (rotationTimer >= rotationChangeInterval)
        {
            float randomYRotation = Random.Range(0f, 360f);
            transform.rotation = Quaternion.Euler(0, randomYRotation, 0);
            rotationTimer = 0f;
        }

        // �������� �̵�
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
