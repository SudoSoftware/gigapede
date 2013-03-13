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
    class SoundtrackManager
    {
        Soundtrack current_soundtrack;

        public SoundtrackManager()
        {
            current_soundtrack = null;
        }

        public virtual Soundtrack PlaySoundtrack(Soundtrack track)
        {
            if (current_soundtrack != null)
                current_soundtrack.Stop();

            Soundtrack old = current_soundtrack;
            current_soundtrack = track;
            current_soundtrack.Play();

            return old;
        }

        public virtual void PlaySound(SoundEffect sound)
        {
            sound.Play();
        }

        public virtual void Update()
        {
            if (current_soundtrack != null)
                current_soundtrack.Update();
        }
    }
}
