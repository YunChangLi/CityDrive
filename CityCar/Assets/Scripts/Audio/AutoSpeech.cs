using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SpeechLib;
using UnityEngine;

public class AutoSpeech:MonoBehaviour
{


    private CancellationTokenSource m_cancel;

    public void StartTips(string s)
    {
        var voice = new SpVoice();
        StopTip();
        m_cancel=new CancellationTokenSource();
        m_cancel.Token.Register(() =>
        {
            // voice.Speak(string.Empty,SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
            voice.Pause();
        });
        Task.Run(() => { voice.Speak( s);}, m_cancel.Token);
        
    }

    public void CheckoutTips(string s)
    {
        var voice = new SpVoice();
        StopTip();
        m_cancel = new CancellationTokenSource();
        m_cancel.Token.Register(() =>
        {
            // voice.Speak(string.Empty,SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
            voice.Pause();
        });
        voice.Rate =1;
        voice.Volume = 100;
        Task.Run(() => { voice.Speak(s); }, m_cancel.Token);
    }

    public void StopTip()
    {
        m_cancel?.Cancel();
    }
    
}
