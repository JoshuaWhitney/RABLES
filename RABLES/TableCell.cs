using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RABLES
{
    public class TableCell
    {
        private List<double> optionScores = new List<double>();
        private int timesPlayed = 0;
        private int optionLength;
        public TableCell()
        {
            optionScores.Add(0);
            optionScores.Add(0);
            optionScores.Add(0);
            optionScores.Add(0);
            optionScores.Add(0);
        }

        public TableCell(int bestOption, int allowSplit)
        {
            optionLength = allowSplit;
            for(int i = 0; i < allowSplit; i++)
            {
                if (i == bestOption)
                    optionScores.Add(0.5);
                else
                    optionScores.Add(0);
            }
        }

        public int getBest(bool canDouble = true, bool canSplit = true, bool canSurrender= true)
        {
            double bestValue = -999999999;
            int bestIndex = -1;
            for(int i = 0; i < 2; i++)
            {
                if(optionScores[i] > bestValue)
                {
                    bestValue = optionScores[i];
                    bestIndex = i;
                }
            }
            if (canDouble)
            {
                if (optionScores[2] > bestValue)
                {
                    bestValue = optionScores[2];
                    bestIndex = 2;
                }
            }

            if (canSurrender)
            {
                if (optionScores[3] > bestValue)
                {
                    bestValue = optionScores[3];
                    bestIndex = 3;
                }
            }

            if (canSplit && optionLength == 5)
            {
                if (optionScores[4] > bestValue)
                {
                    bestValue = optionScores[4];
                    bestIndex = 4;
                }
            }
            return bestIndex;
        }

        public void addEntry(int option, double updateVal)
        {
            Console.WriteLine("Current value: " + optionScores[option]);
            Console.WriteLine("updateVal: " + updateVal);
            Console.WriteLine("Times played: " + timesPlayed);
            Console.WriteLine("Calced value: " + ((optionScores[option] * (double)timesPlayed) + updateVal) / (double)(timesPlayed + 1));

            optionScores[option] = ((optionScores[option] * (double)timesPlayed) + updateVal) / (double)(timesPlayed + 1);
            //    += updateVal / (timesPlayed + 1);
        }

        public void incTimes()
        {
            timesPlayed++;
        }

        public List<double> getScores()
        {
            return optionScores;
        }

        public void Rewrite (string[] options)
        {
            timesPlayed = int.Parse(options[2]);
            optionScores[0] = double.Parse(options[3]);
            optionScores[1] = double.Parse(options[4]);
            optionScores[2] = double.Parse(options[5]);
            optionScores[3] = double.Parse(options[6]);
            if (optionLength == 5)
                optionScores[4] = double.Parse(options[7]);
        }

        public int getTimes()
        {
            return timesPlayed;
        }
    }
}