using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[Serializable]
public class Dialogue 
{
    [SerializeField] private LocalizedString[] lines;

    public string GetLine(int lineIndex)
    {
        return lines[lineIndex].GetLocalizedString();
    }

    public int GetDialoguesCount() => lines.Length;
}
