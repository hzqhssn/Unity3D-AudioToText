using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;

public class SpeechRecognition : MonoBehaviour
{

    public Text outPut;
    KeywordRecognizer keywordRecognizer;
    DictationRecognizer dictationRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    void Start()
    {
        Debug.Log("--");
        //Create keywords for keyword recognizer
        keywords.Add("activate", () =>
        {
            Debug.Log("Activate");
        });
        dictationRecognizer = new DictationRecognizer();
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;
        dictationRecognizer.DictationComplete += DictationRecognizer_DictationComplete;

        dictationRecognizer.Start();
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        // if the keyword recognized is in our dictionary, call that Action.
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    private void DictationRecognizer_DictationHypothesis(string text)
    {
        Debug.Log(text+" --");
    }

    private void DictationRecognizer_DictationComplete(DictationCompletionCause cause)
    {
        // do something
    }

    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
    {
        Debug.Log(text +"--"+ confidence);
        outPut.text = text;
    }

    private void OnDisable()
    {
        PhraseRecognitionSystem.Shutdown();
    }
}
