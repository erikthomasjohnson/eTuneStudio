using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eTuneStudio.eTuneConsole
{
    class Harmony
    {
        List<double> seed;
        int index;
        int next;
        int channel;
        int key;
        int nNote;
        int lNote;
        int llNote;
        int octave;
        int[] mode;
        int[] duration;
        int[] modeP;
        int[] triad;
        int[] tD;
        List<int> sixteenthRthm;
        List<int> eighthRthm;
        List<int> quarterRthm;
        List<int> halfRthm;
        List<int> wholeRthm;
        List<int[]> harmonicProgression;
        List<List<int>> rhythm;
        List<List<List<int>>> intro;
        List<List<List<int>>> verse;
        List<List<List<int>>> build;
        List<List<List<int>>> refrain;
        List<List<List<int>>> bridge;
        List<List<List<int>>> outro;
        public Harmony(int channel, int key, int[] mode, List<int[]> harmonicProgression, List<List<int>> rhythm)
        {
            index = 0;
            next = 0;
            nNote = 0;
            lNote = 0;
            llNote = 0;
            octave = 36;
            duration = new int[] { 3, 6, 12, 24, 48 };
            sixteenthRthm = new List<int>() { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };
            eighthRthm = new List<int>() { 6, 6, 6, 6, 6, 6, 6, 6 };
            quarterRthm = new List<int>() { 12, 12, 12, 12 };
            halfRthm = new List<int>() { 24, 24 };
            wholeRthm = new List<int>() { 48 };
            this.channel = channel;
            this.key = key;
            this.mode = mode;
            this.harmonicProgression = harmonicProgression;
            this.rhythm = rhythm;
            modeP = new int[] { 0, 1, 2, 3, 4, 5, 6 };
            triad = new int[] { 0, 2, 4 };
            tD = new int[] { 0, 4 };
        }
        private void GetSeed()
        {
            Random random = new Random();
            seed = new List<double>();
            double range = 10000000;
            int rng = 10000000;
            for (int i = 0; i < range; i++)
            {
                seed.Add(random.Next(rng) / range);
            }
        }
        private int Rnd(int multiplier)
        {
            return Convert.ToInt32(Math.Floor(seed[index++] * multiplier));
        }
        private int ChordArp(int harmonicInstance, int direction)
        {
            int[] chord = new int[] { 0, 2, 4 };
            if (direction > 0)
            {
                if (next < 0) { next = 0; }
                if (next > 2) { next = 0; }
                return mode[harmonicInstance + chord[next++]];
            }
            if (direction <= 0)
            {
                if (next < 0) { next = 2; }
                if (next > 2) { next = 2; }
                return mode[harmonicInstance + chord[next--]];
            }
            else { return mode[harmonicInstance + chord[next++]]; }
        }
        private List<int> GetRhythm()
        {
            List<int> quarterRhythm = new List<int>();
            List<int> halfRhythm = new List<int>();
            List<int> wholeRhythm = new List<int>();
            int qNum = Rnd(4);
            if (qNum < 2)
            {
                while (quarterRhythm.Sum() != 12)
                {
                    int num = Rnd(3);
                    if (num < 2 || quarterRhythm.Sum() > 6)
                    {
                        quarterRhythm.Add(6);
                    }
                    else
                    {
                        quarterRhythm.Add(6);
                    }
                }
            }
            else
            {
                quarterRhythm.Add(12);
            }
            int hNum = Rnd(4);
            if (hNum < 2)
            {
                while (halfRhythm.Sum() != 24)
                {
                    int num = Rnd(3);
                    if (num < 2)
                    {
                        for (int i = 0; i < quarterRhythm.Count; i++)
                        {
                            halfRhythm.Add(quarterRhythm[i]);
                        }
                    }
                    else
                    {
                        halfRhythm.Add(12);
                    }
                }
            }
            else
            {
                halfRhythm.Add(24);
            }
            int wNum = Rnd(2);
            if (wNum < 2)
            {
                while (wholeRhythm.Sum() != 48)
                {
                    int num = Rnd(3);
                    if (num < 2)
                    {
                        for (int i = 0; i < halfRhythm.Count; i++)
                        {
                            wholeRhythm.Add(halfRhythm[i]);
                        }
                    }
                    else
                    {
                        wholeRhythm.Add(24);
                    }
                }
            }
            else
            {
                wholeRhythm.Add(48);
            }
            return wholeRhythm;
        }
        private List<List<int>> Measure(int c, int n, int d)
        {
            List<List<int>> measure = new List<List<int>>();
            int loop;
            if (d == 3) { loop = 16; }
            else if (d == 4) { loop = 12; }
            else if (d == 6) { loop = 8; }
            else if (d == 12) { loop = 4; }
            else if (d == 24) { loop = 2; }
            else if (d == 48) { loop = 1; }
            else { loop = 0; }
            for (int i = 0; i < loop; i++)
            {
                measure.Add(new List<int> { 0, c, n, d });
            }
            return measure;
        }
        private List<List<int>> GetSection(int harmonicInstance, List<int> rthm)
        {
            List<List<int>> phrase = new List<List<int>>();
            if (channel == 0) { phrase = GetBass(harmonicInstance, wholeRthm); }
            if (channel == 1) { phrase = GetTenor(harmonicInstance, wholeRthm); }
            if (channel == 2) { phrase = GetAlto(harmonicInstance, wholeRthm); }
            if (channel == 3) { phrase = GetSoprano(harmonicInstance, wholeRthm); }
            return phrase;
        }
        private List<List<int>> GetBass(int harmonicInstance, List<int> rthm)
        {
            List<List<int>> phrase = new List<List<int>>();
            nNote = (octave + key) + mode[harmonicInstance];
            for (int i = 0; i < rthm.Count; i++)
            {
                phrase.Add(new List<int> { channel, channel, nNote, rthm[i] });
            }
            return phrase;
        }
        private List<List<int>> GetTenor(int harmonicInstance, List<int> rthm)
        {
            List<List<int>> phrase = new List<List<int>>();
            int root = (octave + key) + mode[harmonicInstance];
            int third = (octave + key) + mode[harmonicInstance + 2];
            int fifth = (octave + key) + mode[harmonicInstance + 4];
            int nextNote = fifth;
            if (nNote <= 0) { nNote = lNote; }
            if (nNote <= 0) { nNote = llNote; }
            if (Math.Abs(nNote - nextNote) > Math.Abs(nNote - third) && nNote <= 0) { nextNote = third; }
            if (Math.Abs(nNote - nextNote) > Math.Abs(nNote - fifth) && nNote <= 0) { nextNote = fifth; }
            if (lNote > 0) { llNote = lNote; }
            if (nNote > 0) { lNote = nNote; }
            for (int i = 0; i < rthm.Count; i++)
            {
                int num = Rnd(100);
                if (num == 0) { nNote = -1; }
                else { nNote = nextNote; }
                //else { nNote = (octave + key) + ChordArp(harmonicInstance, 1); }
                phrase.Add(new List<int> { channel, channel, nNote, rthm[i] });
            }
            return phrase;
        }
        private List<List<int>> GetAlto(int harmonicInstance, List<int> rthm)
        {
            List<List<int>> phrase = new List<List<int>>();
            int root = (octave + key + 0) + mode[harmonicInstance];
            int third = (octave + key + 0) + mode[harmonicInstance + 2];
            int fifth = (octave + key + 0) + mode[harmonicInstance + 4];
            int nextNote = third;
            if (nNote <= 0) { nNote = lNote; }
            if (nNote <= 0) { nNote = llNote; }
            if (Math.Abs(nNote - nextNote) > Math.Abs(nNote - third) && nNote <= 0) { nextNote = third; }
            if (Math.Abs(nNote - nextNote) > Math.Abs(nNote - fifth) && nNote <= 0) { nextNote = fifth; }
            if (lNote > 0) { llNote = lNote; }
            if (nNote > 0) { lNote = nNote; }
            for (int i = 0; i < rthm.Count; i++)
            {
                int num = Rnd(100);
                if (num == 0) { nNote = -1; }
                else { nNote = nextNote; }
                //else { nNote = (octave + key + 12) + ChordArp(harmonicInstance, 0); }
                phrase.Add(new List<int> { channel, channel, nNote, rthm[i] });
            }
            return phrase;
        }
        private List<List<int>> GetSoprano(int harmonicInstance, List<int> rthm)
        {
            List<List<int>> phrase = new List<List<int>>();
            int root = (octave + key + 12) + mode[harmonicInstance];
            int third = (octave + key + 12) + mode[harmonicInstance + 2];
            int fifth = (octave + key + 12) + mode[harmonicInstance + 4];
            int nextNote = root;
            if (nNote <= 0) { nNote = lNote; }
            if (nNote <= 0) { nNote = llNote; }
            if (Math.Abs(nNote - nextNote) > Math.Abs(nNote - third) && nNote <= 0) { nextNote = third; }
            if (Math.Abs(nNote - nextNote) > Math.Abs(nNote - fifth) && nNote <= 0) { nextNote = fifth; }
            if (lNote > 0) { llNote = lNote; }
            if (nNote > 0) { lNote = nNote; }
            for (int i = 0; i < rthm.Count; i++)
            {
                int num = Rnd(100);
                if (num == 0) { nNote = -1; }
                else { nNote = nextNote; }
                phrase.Add(new List<int> { channel, channel, nNote, rthm[i] });
            }
            return phrase;
        }
        private List<List<int>> GetMelody(int[] harmonicProgression)
        {
            List<int[]> guide01 = new List<int[]>() { tD, modeP, modeP, modeP, modeP, modeP, modeP, modeP, tD, modeP, modeP, modeP, modeP, modeP, modeP, modeP };
            List<int[]> guide02 = new List<int[]>() { tD, modeP, triad, modeP, triad, modeP, triad, modeP, tD, modeP, triad, modeP, triad, modeP, triad, modeP };
            int[] melody = new int[16];
            int[] arc = new int[16];
            int[] arcDirection = new int[16];
            int arcGravity = 0;
            for (int i = 0; i < melody.Length; i++)
            {
                int harmonicInstance = 0; /*harmonicProgression[i / 4];*/
                int r = Rnd(2);
                if (r == 0)
                {
                    int[] n = new int[3];
                    for (int ii = 0; ii < n.Length; ii++)
                    {
                        n[ii] = octave + key + 12 + mode[harmonicInstance + guide01[i][Rnd(guide01[i].Length)]];
                        if (ii > 0) { if (n[ii] < n[ii - 1]) { int highN = n[ii - 1]; n[ii - 1] = n[ii]; n[ii] = highN; } }
                    }
                    if (i > 0) { if (Math.Abs(n[0] - melody[i - 1]) <= Math.Abs(n[1] - melody[i - 1])) { melody[i] = n[0]; } else { melody[i] = n[1]; } }
                    else { melody[i] = n[0]; }
                }
                else
                {
                    int[] n = new int[3];
                    for (int ii = 0; ii < n.Length; ii++)
                    {
                        n[ii] = octave + key + 12 + mode[harmonicInstance + guide02[i][Rnd(guide02[i].Length)]];
                        if (ii > 0) { if (n[ii] < n[ii - 1]) { int highN = n[ii - 1]; n[ii - 1] = n[ii]; n[ii] = highN; } }
                    }
                    if (i > 0) { if (Math.Abs(n[0] - melody[i - 1]) <= Math.Abs(n[1] - melody[i - 1])) { melody[i] = n[0]; } else { melody[i] = n[1]; } }
                    else { melody[i] = n[0]; }
                }
                if (i > 0)
                {
                    arcGravity += arc[i] = melody[i] - melody[i - 1];
                    if (arc[i] > arc[i - 1]) { arcDirection[i] = 1; } else if (arc[i] < arc[i - 1]) { arcDirection[i] = -1; } else { arcDirection[i] = 0; }
                    if (i == melody.Length - 1)
                    {
                        int i01 = 0; int c01 = 100; for (int ii = 0; ii < triad.Length; ii++) { if (melody[i] != triad[ii]) { i01++; if (Math.Abs(melody[i] - triad[ii]) < c01) { c01 = triad[ii]; } } }
                        if (i01 == triad.Length) { melody[i] = c01; }
                    }
                }
            }
            List<List<int>> phrase = new List<List<int>>();
            List<int> mRhythm = GetRhythm();
            List<int> rMelody = new List<int>();
            for (int i = 0; i < mRhythm.Count; i++)
            {
                if (i == 0) { rMelody.Add(melody[0]); }
                else
                {
                    rMelody.Add(melody[mRhythm[i] / 6]);
                }
            }
            for (int i = 0; i < mRhythm.Count; i++)
            {
                phrase.Add(new List<int> { channel, channel, rMelody[i], mRhythm[i] });
            }
            return phrase;
        }
        public List<List<List<List<int>>>> Improvise()
        {
            GetSeed();
            Intro();
            Verse();
            Build();
            Refrain();
            Bridge();
            Outro();
            return new List<List<List<List<int>>>>() { verse, refrain, verse, refrain };
        }
        public void Intro()
        {
            intro = new List<List<List<int>>>();
            List<int> rhythm01 = new List<int>();
            List<int> rhythm02 = new List<int>();
            List<int> rhythm03 = new List<int>();
            List<int> rhythm04 = new List<int>();
            if (Rnd(2) == 0) { rhythm01 = rhythm[Rnd(6)]; } else { rhythm01 = rhythm[0]; }
            if (Rnd(2) == 0) { rhythm02 = rhythm[Rnd(6)]; } else { rhythm02 = rhythm[0]; }
            if (Rnd(2) == 0) { rhythm03 = rhythm[Rnd(6)]; } else { rhythm03 = rhythm[0]; }
            if (Rnd(2) == 0) { rhythm04 = rhythm[Rnd(6)]; } else { rhythm04 = rhythm[0]; }
            intro.Add(GetSection(harmonicProgression[0][0], rhythm01));
            intro.Add(GetSection(harmonicProgression[0][0], rhythm02));
        }
        public void Verse()
        {
            verse = new List<List<List<int>>>();
            List<int> rhythm01 = new List<int>();
            List<int> rhythm02 = new List<int>();
            List<int> rhythm03 = new List<int>();
            List<int> rhythm04 = new List<int>();
            if (Rnd(2) == 0) { rhythm01 = rhythm[Rnd(6)]; } else { rhythm01 = rhythm[1]; }
            if (Rnd(2) == 0) { rhythm02 = rhythm[Rnd(6)]; } else { rhythm02 = rhythm[1]; }
            if (Rnd(2) == 0) { rhythm03 = rhythm[Rnd(6)]; } else { rhythm03 = rhythm[1]; }
            if (Rnd(2) == 0) { rhythm04 = rhythm[Rnd(6)]; } else { rhythm04 = rhythm[1]; }
            if (channel == 3) { verse.Add(GetMelody(harmonicProgression[1])); verse.Add(GetMelody(harmonicProgression[1])); }
            else
            {
                verse.Add(GetSection(harmonicProgression[1][0], rhythm01));
                verse.Add(GetSection(harmonicProgression[1][1], rhythm02));
                verse.Add(GetSection(harmonicProgression[1][2], rhythm03));
                verse.Add(GetSection(harmonicProgression[1][3], rhythm04));
                verse.Add(GetSection(harmonicProgression[1][0], rhythm01));
                verse.Add(GetSection(harmonicProgression[1][1], rhythm02));
                verse.Add(GetSection(harmonicProgression[1][2], rhythm03));
                verse.Add(GetSection(harmonicProgression[1][3], rhythm04));
            }
        }
        public void Build()
        {
            build = new List<List<List<int>>>();
            List<int> rhythm01 = new List<int>();
            List<int> rhythm02 = new List<int>();
            List<int> rhythm03 = new List<int>();
            List<int> rhythm04 = new List<int>();
            if (Rnd(2) == 0) { rhythm01 = rhythm[Rnd(6)]; } else { rhythm01 = rhythm[2]; }
            if (Rnd(2) == 0) { rhythm02 = rhythm[Rnd(6)]; } else { rhythm02 = rhythm[2]; }
            if (Rnd(2) == 0) { rhythm03 = rhythm[Rnd(6)]; } else { rhythm03 = rhythm[2]; }
            if (Rnd(2) == 0) { rhythm04 = rhythm[Rnd(6)]; } else { rhythm04 = rhythm[2]; }
            build.Add(GetSection(harmonicProgression[2][0], rhythm01));
            build.Add(GetSection(harmonicProgression[2][1], rhythm02));
        }
        public void Refrain()
        {
            refrain = new List<List<List<int>>>();
            List<int> rhythm01 = new List<int>();
            List<int> rhythm02 = new List<int>();
            List<int> rhythm03 = new List<int>();
            List<int> rhythm04 = new List<int>();
            if (Rnd(2) == 0) { rhythm01 = rhythm[Rnd(6)]; } else { rhythm01 = rhythm[3]; }
            if (Rnd(2) == 0) { rhythm02 = rhythm[Rnd(6)]; } else { rhythm02 = rhythm[3]; }
            if (Rnd(2) == 0) { rhythm03 = rhythm[Rnd(6)]; } else { rhythm03 = rhythm[3]; }
            if (Rnd(2) == 0) { rhythm04 = rhythm[Rnd(6)]; } else { rhythm04 = rhythm[3]; }
            if (channel == 3) { refrain.Add(GetMelody(harmonicProgression[3])); refrain.Add(GetMelody(harmonicProgression[3])); }
            else
            {
                refrain.Add(GetSection(harmonicProgression[3][0], rhythm01));
                refrain.Add(GetSection(harmonicProgression[3][1], rhythm02));
                refrain.Add(GetSection(harmonicProgression[3][2], rhythm03));
                refrain.Add(GetSection(harmonicProgression[3][3], rhythm04));
                refrain.Add(GetSection(harmonicProgression[3][0], rhythm01));
                refrain.Add(GetSection(harmonicProgression[3][1], rhythm02));
                refrain.Add(GetSection(harmonicProgression[3][2], rhythm03));
                refrain.Add(GetSection(harmonicProgression[3][3], rhythm04));
            }
        }
        public void Bridge()
        {
            bridge = new List<List<List<int>>>();
            List<int> rhythm01 = new List<int>();
            List<int> rhythm02 = new List<int>();
            List<int> rhythm03 = new List<int>();
            List<int> rhythm04 = new List<int>();
            if (Rnd(2) == 0) { rhythm01 = rhythm[Rnd(6)]; } else { rhythm01 = rhythm[4]; }
            if (Rnd(2) == 0) { rhythm02 = rhythm[Rnd(6)]; } else { rhythm02 = rhythm[4]; }
            if (Rnd(2) == 0) { rhythm03 = rhythm[Rnd(6)]; } else { rhythm03 = rhythm[4]; }
            if (Rnd(2) == 0) { rhythm04 = rhythm[Rnd(6)]; } else { rhythm04 = rhythm[4]; }
            bridge.Add(GetSection(harmonicProgression[4][0], rhythm01));
            bridge.Add(GetSection(harmonicProgression[4][1], rhythm02));
        }
        public void Outro()
        {
            outro = new List<List<List<int>>>();
            List<int> rhythm01 = new List<int>();
            List<int> rhythm02 = new List<int>();
            List<int> rhythm03 = new List<int>();
            List<int> rhythm04 = new List<int>();
            if (Rnd(2) == 0) { rhythm01 = rhythm[Rnd(6)]; } else { rhythm01 = rhythm[5]; }
            if (Rnd(2) == 0) { rhythm02 = rhythm[Rnd(6)]; } else { rhythm02 = rhythm[5]; }
            if (Rnd(2) == 0) { rhythm03 = rhythm[Rnd(6)]; } else { rhythm03 = rhythm[5]; }
            if (Rnd(2) == 0) { rhythm04 = rhythm[Rnd(6)]; } else { rhythm04 = rhythm[5]; }
            outro.Add(GetSection(harmonicProgression[5][0], rhythm01));
            outro.Add(GetSection(harmonicProgression[5][0], rhythm02));
        }
    }
}