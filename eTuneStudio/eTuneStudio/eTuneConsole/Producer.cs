using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace eTuneStudio.eTuneConsole
{
    public class Producer
    {
        static string[] m_InstrumentNames =
        {
            "Acoustic Grand Piano",
            "Bright Acoustic Piano",
            "Electric Grand Piano",
            "Honky-tonk Piano",
            "Electric Piano 1",
            "Electric Piano 2",
            "Harpsichord",
            "Clavinet",
            "Celesta",
            "Glockenspiel",
            "Music Box",
            "Vibraphone",
            "Marimba",
            "Xylophone",
            "Tubular Bells",
            "Dulcimer",
            "Drawbar Organ",
            "Percussive Organ",
            "Rock Organ",
            "Church Organ",
            "Reed Organ",
            "Accordion",
            "Harmonica",
            "Tango Accordion",
            "Acoustic Guitar (nylon)",
            "Acoustic Guitar (steel)",
            "Electric Guitar (jazz)",
            "Electric Guitar (clean)",
            "Electric Guitar (muted)",
            "Overdriven Guitar",
            "Distortion Guitar",
            "Guitar harmonics",
            "Acoustic Bass",
            "Electric Bass (finger)",
            "Electric Bass (pick)",
            "Fretless Bass",
            "Slap Bass 1",
            "Slap Bass 2",
            "Synth Bass 1",
            "Synth Bass 2",
            "Violin",
            "Viola",
            "Cello",
            "Contrabass",
            "Tremolo Strings",
            "Pizzicato Strings",
            "Orchestral Harp",
            "Timpani",
            "String Ensemble 1",
            "String Ensemble 2",
            "Synth Strings 1",
            "Synth Strings 2",
            "Choir Aahs",
            "Voice Oohs",
            "Synth Choir",
            "Orchestra Hit",
            "Trumpet",
            "Trombone",
            "Tuba",
            "Muted Trumpet",
            "French Horn",
            "Brass Section",
            "Synth Brass 1",
            "Synth Brass 2",
            "Soprano Sax",
            "Alto Sax",
            "Tenor Sax",
            "Baritone Sax",
            "Oboe",
            "English Horn",
            "Bassoon",
            "Clarinet",
            "Piccolo",
            "Flute",
            "Recorder",
            "Pan Flute",
            "Blown Bottle",
            "Shakuhachi",
            "Whistle",
            "Ocarina",
            "Lead 1 (square)",
            "Lead 2 (sawtooth)",
            "Lead 3 (calliope)",
            "Lead 4 (chiff)",
            "Lead 5 (charang)",
            "Lead 6 (voice)",
            "Lead 7 (fifths)",
            "Lead 8 (bass + lead)",
            "Pad 1 (new age)",
            "Pad 2 (warm)",
            "Pad 3 (polysynth)",
            "Pad 4 (choir)",
            "Pad 5 (bowed)",
            "Pad 6 (metallic)",
            "Pad 7 (halo)",
            "Pad 8 (sweep)",
            "FX 1 (rain)",
            "FX 2 (soundtrack)",
            "FX 3 (crystal)",
            "FX 4 (atmosphere)",
            "FX 5 (brightness)",
            "FX 6 (goblins)",
            "FX 7 (echoes)",
            "FX 8 (sci-fi)",
            "Sitar",
            "Banjo",
            "Shamisen",
            "Koto",
            "Kalimba",
            "Bag pipe",
            "Fiddle",
            "Shanai",
            "Tinkle Bell",
            "Agogo",
            "Steel Drums",
            "Woodblock",
            "Taiko Drum",
            "Melodic Tom",
            "Synth Drum",
            "Reverse Cymbal",
            "Guitar Fret Noise",
            "Breath Noise",
            "Seashore",
            "Bird Tweet",
            "Telephone Ring",
            "Helicopter",
            "Applause",
            "Gunshot"
        };
        MIDISong song;
        Composer composition;
        public void CreateMidi()
        {
            song = new MIDISong();
            composition = new Composer();
            List<List<List<List<List<int>>>>> music = new List<List<List<List<List<int>>>>>();
            music = composition.Compose();
            for (int i = 0; i < music.Count; i++)
            {
                if (music[i][0][0][0][1] == 0) { song.AddTrack("Bass"); song.SetTimeSignature(0, 4, 4); song.SetTempo(0, 120); song.SetChannelInstrument(0, 0, 52); }
                if (music[i][0][0][0][1] == 1) { song.AddTrack("Tenor"); song.SetTimeSignature(0, 4, 4); song.SetTempo(0, 120); song.SetChannelInstrument(1, 1, 52); }
                if (music[i][0][0][0][1] == 2) { song.AddTrack("Alto"); song.SetTimeSignature(0, 4, 4); song.SetTempo(0, 120); song.SetChannelInstrument(2, 2, 52); }
                if (music[i][0][0][0][1] == 3) { song.AddTrack("Soprano"); song.SetTimeSignature(0, 4, 4); song.SetTempo(0, 120); song.SetChannelInstrument(3, 3, 52); }
                for (int ii = 0; ii < music[i].Count; ii++)
                {
                    for (int iii = 0; iii < music[i][ii].Count; iii++)
                    {
                        for (int iv = 0; iv < music[i][ii][iii].Count; iv++)
                        {
                            song.AddNote(music[i][ii][iii][iv][0], music[i][ii][iii][iv][1], music[i][ii][iii][iv][2], music[i][ii][iii][iv][3]);
                        }
                    }
                }
            }

            try
            {
                MemoryStream ms = new MemoryStream();
                song.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);
                byte[] src = ms.GetBuffer();
                byte[] dst = new byte[src.Length];
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] = src[i];
                }
                ms.Close();

                string strTempFileName = Path.ChangeExtension(Path.GetTempFileName(), ".mid");
                FileStream objWriter = File.Create(strTempFileName);
                objWriter.Write(dst, 0, dst.Length);
                objWriter.Close();
                objWriter.Dispose();
                objWriter = null;

                Console.WriteLine(strTempFileName);
            }
            catch { }

        }
    }
}