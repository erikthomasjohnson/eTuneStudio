using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eTuneStudio.eTuneConsole
{
    public class Composer
    {
        List<double> seed;
        int index;
        int key;
        int[] duration;
        int[] mode;
        int[] introHP;
        int[] verseHP;
        int[] buildHP;
        int[] refrainHP;
        int[] bridgeHP;
        int[] outroHP;
        int[] hP;
        List<int[]> harmonicProgression;
        List<int> bassRthm;
        List<int> introRthm;
        List<int> verseRthm;
        List<int> buildRthm;
        List<int> refrainRthm;
        List<int> bridgeRthm;
        List<int> outroRthm;
        List<List<int>> rhythm;
        public Composer()
        {
            index = 0;
            key = 0;
            duration = new int[] { 3, 6, 12, 24, 48 };
            bassRthm = new List<int>() { 48 };
            introRthm = new List<int>();
            verseRthm = new List<int>();
            buildRthm = new List<int>();
            refrainRthm = new List<int>();
            bridgeRthm = new List<int>();
            outroRthm = new List<int>();
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
        private void GetMode()
        {
            int num = Rnd(1);
            int[] ionian = new int[15] { 0, 2, 4, 5, 7, 9, 11, 12, 14, 16, 17, 19, 21, 23, 24 };
            int[] aeolian = new int[15] { 0, 2, 3, 5, 7, 8, 10, 12, 14, 15, 17, 19, 20, 22, 24 };
            int[] dorian = new int[15] { 0, 2, 3, 5, 7, 9, 10, 12, 14, 15, 17, 19, 21, 22, 24 };
            int[] lydian = new int[15] { 0, 2, 4, 6, 7, 9, 11, 12, 14, 16, 18, 19, 21, 23, 24 };
            if (num == 0) { mode = ionian; }
            else if (num == 1) { mode = aeolian; }
            else if (num == 2) { mode = dorian; }
            else if (num == 3) { mode = lydian; }
        }
        public int[] GetHarmonicProgression()
        {
            int num = Rnd(4);
            int[] I_V_vi_IV = new int[4] { 0, 4, 5, 3 };
            int[] V_vi_IV_I = new int[4] { 4, 5, 3, 0 };
            int[] vi_IV_I_V = new int[4] { 5, 3, 0, 4 };
            int[] IV_I_V_iv = new int[4] { 3, 0, 4, 5 };
            int[] I_vi_IV_V = new int[4] { 0, 5, 3, 4 };
            int[] ii_IV_V_V = new int[4] { 1, 3, 4, 4 };
            int[] I_IV_V_IV = new int[4] { 0, 3, 4, 3 };
            int[] V_IV_I_I = new int[4] { 4, 3, 0, 0 };
            int[] ii_I_V_VII = new int[4] { 1, 0, 4, 6 };
            if (num == 0) { return I_V_vi_IV; }
            else if (num == 1) { return V_vi_IV_I; }
            else if (num == 2) { return vi_IV_I_V; }
            else if (num == 3) { return IV_I_V_iv; }
            else if (num == 4) { return I_vi_IV_V; }
            else if (num == 5) { return ii_IV_V_V; }
            else if (num == 6) { return I_IV_V_IV; }
            else if (num == 7) { return V_IV_I_I; }
            else if (num == 8) { return ii_I_V_VII; }
            else { return I_V_vi_IV; }
        }
        private List<int> GetRhythm()
        {
            List<int> quarterRhythm = new List<int>();
            List<int> halfRhythm = new List<int>();
            List<int> wholeRhythm = new List<int>();
            int qNum = Rnd(3);
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
            int hNum = Rnd(3);
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
            int wNum = Rnd(3);
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
        public List<List<List<List<List<int>>>>> Compose()
        {
            GetSeed();
            GetMode();
            key = Rnd(12);
            hP = GetHarmonicProgression();
            introHP = new int[] { hP[Rnd(4)], hP[Rnd(4)] };
            introRthm = GetRhythm();
            verseHP = GetHarmonicProgression();
            verseRthm = GetRhythm();
            buildHP = new int[] { hP[Rnd(4)], hP[Rnd(4)] };
            buildRthm = GetRhythm();
            refrainHP = hP;
            refrainRthm = GetRhythm();
            bridgeHP = new int[] { hP[Rnd(4)], hP[Rnd(4)] };
            bridgeRthm = GetRhythm();
            outroHP = new int[] { hP[Rnd(4)], hP[Rnd(4)] };
            outroRthm = GetRhythm();
            harmonicProgression = new List<int[]> { introHP, verseHP, buildHP, refrainHP, bridgeHP, outroHP };
            rhythm = new List<List<int>>() { introRthm, verseRthm, buildRthm, refrainRthm, bridgeRthm, outroRthm };
            Harmony bass = new Harmony(0, key, mode, harmonicProgression, rhythm);
            Harmony tenor = new Harmony(1, key, mode, harmonicProgression, rhythm);
            Harmony alto = new Harmony(2, key, mode, harmonicProgression, rhythm);
            Harmony soprano = new Harmony(3, key, mode, harmonicProgression, rhythm);
            return new List<List<List<List<List<int>>>>>() { bass.Improvise(), tenor.Improvise(), alto.Improvise(), soprano.Improvise() };
        }
    }
}