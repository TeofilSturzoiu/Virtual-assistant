using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Diagnostics;
//using Google.API.Search;

namespace Jarvis_v1._0._0
{
    public partial class Jarvis : Form
    {
        SpeechRecognitionEngine Speechreco = new SpeechRecognitionEngine();
        SpeechSynthesizer jarvis = new SpeechSynthesizer();
        DateTime dateTime = DateTime.UtcNow.Date;

        public Jarvis()
        {
            Speechreco.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Speechreco_SpeechRecognized);
            LoadGrammar();
            Speechreco.SetInputToDefaultAudioDevice();
            Speechreco.RecognizeAsync(RecognizeMode.Multiple);       //Sa revin aici pentru comenzi luate gresit.
            jarvis.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Teen);
            //jarvis.SelectVoice("Microsoft Server Speech Text to Speech Voice (en-US, Helen)");
            InitializeComponent();
        }

        private void Speechreco_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            
            richTextBox1.Text = e.Result.Text;
            string speech = e.Result.Text;
            //jarvis.Speak("Au trecut 15 minute.");
            switch(speech)
            {
                case "Jarvis, hello":
                case "Jarvis, hi":
                    jarvis.Speak("Hello Sir.");
                    break;
                case "Jarvis how are you?":
                    jarvis.Speak("I am fine Sir.");
                    break;
                case "Jarvis what is the date?":
                    jarvis.Speak("Today is" + dateTime);
                    break;
                case "Jarvis Open browser for me":
                    jarvis.Speak("Wait a second");
                    System.Diagnostics.Process.Start("http://google.com");
                    break;
                case "Jarvis End process":
                    jarvis.Speak("See you soon Sir");
                    Application.Exit();
                    break;
                case "What is your name?":
                    jarvis.Speak("My name is Jarvis, and I am your servant");
                    break;
                case "Jarvis Who created you?":
                    jarvis.Speak("Teo created me.");
                    break;
                case "Jarvis Play a movie for me":
                    jarvis.Speak("Give me a second");
                    System.Diagnostics.Process.Start(@"D:\FILME\All.Marvel.Movies.Related.To.Infinity.Stones.In.Chronological.Order.HDRO-NoGrp\17.Black.Panther.2018.BluRay.720p.DTS.x264-MTeam\Black.Panther.2018.BluRay.720p.DTS.x264-MTeam.mkv");
                    break;
                case "Jarvis what day is it":
                    string day;
                    day = "Today is, " + System.DateTime.Now.ToString("dddd");
                    jarvis.SpeakAsync(day);
                    break;
                case "what time is it":
                    System.DateTime now = System.DateTime.Now;
                    string time = now.GetDateTimeFormats('t')[0];
                    jarvis.Speak(time);
                    break;
                case "who are you":
                    jarvis.Speak("i am your personal assistant");
                    jarvis.Speak("i can read email, weather report, i can search web for you, anything that you need a personal assistant to do, you can ask me a question and I will reply to you");
                    break;
                case "test":
                    string urladdress = e.Result.Text;
                    webBrowser1.Navigate("www.google.com/search" + urladdress);
                    jarvis.Speak("done");
                    break;
            }
        }

        private void LoadGrammar()
        {
            Choices texts = new Choices();
            string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\commands.txt");
            texts.Add(lines);
            Grammar wordlist = new Grammar(new GrammarBuilder(texts));
            Speechreco.LoadGrammar(wordlist);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
