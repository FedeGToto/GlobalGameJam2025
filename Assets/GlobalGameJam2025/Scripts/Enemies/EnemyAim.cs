using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    [SerializeField] private GameObject aimObject;

    private PlayerManager player;

    private void Start()
    {
        player = GameManager.Instance.Player;
    }

    private void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        aimObject.transform.forward = direction;
    }
}
