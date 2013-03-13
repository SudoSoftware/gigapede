using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace gigapede
{
    class Soundtrack
    {
        private bool stopped;

        private SoundEffect current_sound;
        private DateTime current_start;

        List<SoundEffect> audio_queue;

        public Soundtrack()
        {
            audio_queue = new List<SoundEffect>();
            current_sound = null;

            stopped = true;
        }

        public virtual void AddAudio(SoundEffect audio, int position = -1)
        {
            if (position < 0)
                audio_queue.Add(audio);
            else
                audio_queue.Insert(position, audio);

        }

        public virtual void RemoveAudio(SoundEffect audio)
        {
            if (audio_queue.Contains(audio))
            {
                audio.Dispose();
                audio_queue.Remove(audio);
            }
        }

        public virtual void Play(int start = 0)
        {
            if (audio_queue.Count > 0)
            {
                current_sound = audio_queue[start];
            }

            stopped = false;
        }

        public virtual int GetCurrentIndex()
        {
            if (current_sound != null)
                return audio_queue.IndexOf(current_sound);
            else 
                return -1;
        }

        public virtual void Stop()
        {
            stopped = true;
        }

        public virtual void Update()
        {
            TimeSpan since_start = (DateTime.Now - current_start).Duration();

            if (current_sound != null && since_start > current_sound.Duration && !stopped)
            {
                int index = audio_queue.IndexOf(current_sound);

                if (index == audio_queue.Count)
                    index = 0;

                current_sound = audio_queue[index];

                current_sound.Play();
            }
        }
    }
}
