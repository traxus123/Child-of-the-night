using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialoguePanel;
    public Text nomDialogue;
    public Text phraseDialogue;
    public Queue<string> phrases;
    public Queue<string> noms;


    // Start is called before the first frame update
    void Start()
    {
        DialoguePanel.SetActive(false);
    }

    public void startDialogue(Dialogue dialogue)
    {
        DialoguePanel.SetActive(true);
        noms = new Queue<string>();
        phrases = new Queue<string>();
        Debug.Log(dialogue.id);

        foreach (string nom in dialogue.noms)
        {
            noms.Enqueue(nom);
        }
        foreach (string phrase in dialogue.phrases)
        {
            phrases.Enqueue(phrase);

        }
        nomDialogue.text = noms.Dequeue();
        phraseDialogue.text = phrases.Dequeue();
    }

    public void displayNextSentence()
    {
        if(phrases.Count == 0)
        {
            endDialogue();
            return;
        }
        string nom = noms.Dequeue();
        string phrase = phrases.Dequeue();
        nomDialogue.text = nom;
        phraseDialogue.text = phrase;
        Debug.Log("test" + phrase);
    }
    void endDialogue()
    {
        DialoguePanel.SetActive(false);
        FindObjectOfType<PlayerController>().inDialogue = false;
        Debug.Log("Dialogue end");
    }
}

