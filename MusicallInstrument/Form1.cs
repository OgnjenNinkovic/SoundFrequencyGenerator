using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicallInstrument
{
  

    public partial class Form1 : Form
    {
       
        SignalGenerator sine = new SignalGenerator()
        {
            Type = SignalGeneratorType.Sin,
            Gain = 0.2,
        };


       
        WaveOutEvent player = new WaveOutEvent();


       
        private System.Drawing.Point CusrsorPositionOnMouseDown;

       
        private bool ButtonIsDown = false;

        public Form1()
        {
            InitializeComponent();

          
            player.Init(sine);

           
           
            trackFrequency.ValueChanged += (s, e) => sine.Frequency = trackFrequency.Value;
           
            trackFrequency.Value = 600;


           
            trackVolume.ValueChanged += (s, e) => player.Volume = trackVolume.Value / 100F;
            trackVolume.Value = 50;


          


        }

       
        private void TheMouseDown(object sender, MouseEventArgs e)
        {
           
            player.Play();

           
            CusrsorPositionOnMouseDown = e.Location;


           
            ButtonIsDown = true;
        }

        private void TheMouseUp(object sender, MouseEventArgs e)
        {
           
            player.Stop();

           
            ButtonIsDown = false;
        }




        
        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            
            var dx = e.X - CusrsorPositionOnMouseDown.X;


           
            var vol = player.Volume + (dx / 1000F);




          
            var dy = CusrsorPositionOnMouseDown.Y - e.Y;

           
            var freq = sine.Frequency + dy;

            if (ButtonIsDown)
            {
                
                player.Volume = (vol > 0) ? (vol<1)?vol:1 : 0;
                sine.Frequency = (freq > 100)?(freq<1000)?freq:1000:100;



              
                trackFrequency.Value = (int)Math.Round(sine.Frequency);
                trackVolume.Value = (int)Math.Round(player.Volume * 100);
            }


           
            Text = $"Musical instrument! ({dx},{dy}) ({vol}, {freq})";
            
        }
    }
}
