using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public UnityAction OnLevelEnded;

    private List<CharacterSO> nextBonds;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
            EndLevel();
    }

    public void EndLevel()
    {
        nextBonds = GameManager.Instance.Characters.GetPossibleUpgrades();

        OnLevelEnded?.Invoke();
    }

    public List<CharacterSO> GetNextBonds() => nextBonds;
}
