using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int MaxLevel => maxCharacterLevel;

    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private int maxCharacterLevel = 3;

    public UnityAction OnBondAdded;

    List<CharacterSO> bonds = new();

    public void AddCharacter(string characterId)
    {
        CharacterSO characterReference = GameManager.Instance.Characters.GetCharacterByID(characterId);
        AddCharacter(characterReference);
    }

    public void AddCharacter(CharacterSO augmentId)
    {
        if (bonds == null)
        {
            Debug.LogError("Cannot add any bond: the dictionary is not initialized");
            return;
        }

        CharacterSO bond = bonds.Find(x => x.ID == augmentId.ID);

        if (bond)
        {
            if (bond.Level >= maxCharacterLevel)
                return;

            bond.LevelUp();
        }
        else
        {
            bond = Instantiate(augmentId);
            bonds.Add(bond);
            bond.SetOwner(playerManager);
            bond.LevelUp();
        }

        OnBondAdded?.Invoke();
    }

    public IEnumerable<CharacterSO> GetBonds()
    {
        return bonds;
    }
}
