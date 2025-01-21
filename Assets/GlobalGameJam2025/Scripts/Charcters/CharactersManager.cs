using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{
    [SerializeField] private int charactersPerLevelUp;
    [Space]
    [SerializeField] private int maxCharacters;

    [SerializeField] private List<CharacterSO> characters;

    public List<CharacterSO> GetPossibleUpgrades()
    {
        List<CharacterSO> possibileUpgradeCharacters = new List<CharacterSO>();

        var playerBonds = GameManager.Instance.Player.Inventory.GetBonds();

        CheckAndAddCharacters(ref possibileUpgradeCharacters, playerBonds, maxCharacters);


        foreach (var character in characters)
        {
            if (possibileUpgradeCharacters.Find(x => x.ID == character.ID))
                continue;

            possibileUpgradeCharacters.Add(character);
        }

        RemoveMaxLevelCharacters(ref possibileUpgradeCharacters);
        return ExtractCharacters(ref possibileUpgradeCharacters);
    }

    private List<CharacterSO> ExtractCharacters(ref List<CharacterSO> possibleUpgradeCharacters)
    {
        List<CharacterSO> result = new List<CharacterSO>();
        int iterations = possibleUpgradeCharacters.Count >= charactersPerLevelUp ? charactersPerLevelUp : possibleUpgradeCharacters.Count;

        for (int i = 0; i < iterations; i++)
        {
            int randomId = Random.Range(0, possibleUpgradeCharacters.Count);
            result.Add(possibleUpgradeCharacters[randomId]);
            possibleUpgradeCharacters.RemoveAt(randomId);
        }

        return result;
    }

    public CharacterSO GetCharacterByID(string id) => characters.ToList().Find(x => x.ID == id);

    private void CheckAndAddCharacters(ref List<CharacterSO> possibleCharacters, IEnumerable<CharacterSO> characters, int maxCapacity)
    {
        if (characters.Count() <= maxCapacity)
        {
            possibleCharacters.AddRange(characters);
        }
    }

    private void RemoveMaxLevelCharacters(ref List<CharacterSO> possibleCharacters)
    {
        int maxLevel = GameManager.Instance.Player.Inventory.MaxLevel;
        possibleCharacters.RemoveAll(x => x.Level == maxLevel);
    }
}
