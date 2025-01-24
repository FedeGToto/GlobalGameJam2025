using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueSystemUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    [SerializeField] private Image charArtwork;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Dialogue currentDialogue;
    private int dialogueIndex;

    private void Start()
    {
        GameManager.Instance.Player.Inventory.OnBondAdded += BondAdded;
    }

    private void BondAdded(CharacterSO chara)
    {
        canvas.SetActive(true);
        currentDialogue = chara.Dialogue;
        ShowDialogue(chara);
    }

    public void ShowDialogue(CharacterSO chara)
    {
        dialogueIndex = -1;

        charArtwork.sprite = chara.Artwork;
        nameText.text = chara.GetName();

        AdvanceDialogue();

        canvas.SetActive(true);
    }

    public void AdvanceDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex >= currentDialogue.GetDialoguesCount())
        {
            EndDialogue();
            return;
        }

        dialogueText.text = currentDialogue.GetLine(dialogueIndex);
    }

    public void EndDialogue()
    {
        canvas.SetActive(false);
        GameManager.Instance.Level.CanSpawn = true;
    }
}
