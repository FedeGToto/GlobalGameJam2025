using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BondsUI : MonoBehaviour
{
    [SerializeField] private CharacterUI[] characters;

    private PlayerManager player;

    private void Start()
    {
        player = GameManager.Instance.Player;

        player.Inventory.OnBondAdded += BondAdded;
        BondAdded();
    }

    private void BondAdded()
    {
        var characterBonds = player.Inventory.GetBonds().ToArray();


        for (int i = 0; i < characterBonds.Length; i++)
        {
            characters[i].UpdateAugment(characterBonds[i]);
        }
    }
}
