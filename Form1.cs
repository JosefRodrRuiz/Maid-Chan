using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Diagnostics;
using System.IO;
using Syn.Speech


namespace Voice_Assistant
{
    public partial class Form1 : Form
    {



        SpeechSynthesizer s = new SpeechSynthesizer();
        Choices list = new Choices();

        public Boolean wake = true;
        private Label label1;
        private Label label2;
        private Timer timer1;
        private IContainer components;
        private Timer timer2;
        public Boolean search = false;


        public Form1()
        {

            SpeechRecognitionEngine rec = new SpeechRecognitionEngine();


            //list.Add(new string[] { "" });
            list.Add(File.ReadAllLines(@"C:\cmds.txt")); //CHANGE FILE LOCATION TO THE SAME IN YOUR SYSTEM

            Grammar gr = new Grammar(new GrammarBuilder(list));


            try
            {
                rec.RequestRecognizerUpdate();
                rec.LoadGrammar(gr);
                rec.SpeechRecognized += rec_SpeechRecognized;
                rec.SetInputToDefaultAudioDevice();
                rec.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch { return; }


            s.SelectVoiceByHints(VoiceGender.Female);

            s.Speak("Voice Assistant");


            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void say(string h)
        {
            s.Speak(h);
        }


        private void rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string r = e.Result.Text;


            //                                                         SEARCH COMMAND




            if (search)
            {
                Process.Start("https://duckduckgo.com" + r);

                search = false;
            }


            //                                                          WAKE COMMANDS

            if (r == "wake") wake = true;
            if (r == "sleep") wake = false;





            if (wake == true && search == false)
            {

                if (r == "search for")
                {
                    search = true;
                }

                //                                                         COMMANDS






                //HERE ARE SOME EXAMPLES

                if (r == "open spotify")
                {
                    Process.Start(@"C:\Users\PC\AppData\Roaming\Spotify\Spotify.exe");
                }
                if (r == "open duckduckgo")
                {
                    Process.Start("https://duckduckgo.com");
                }
                if (r == "time")
                {
                    s.SpeakAsync("it's");
                    string time = DateTime.Now.ToShortTimeString();
                    label1.Text = time;
                    s.SpeakAsync(time);
                };
                if (r == "what date is it today")
                {
                    s.SpeakAsync("today is");
                    string date = DateTime.Now.ToLongDateString();
                    label2.Text = date;
                    s.SpeakAsync(date);
                }
            }


        }


    //                                                                     TIMER

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToLongTimeString();
            label1.Text = time;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToLongDateString();
            label2.Text = date;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
