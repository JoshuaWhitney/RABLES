using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RABLES
{
    public class AI
    {
        //Access with Player sum, then Dealer card
        int tableRef;
        //Hard Table
        List<List<TableCell>> HardTable = new List<List<TableCell>>();
        
        //Soft Table
        List<List<TableCell>> SoftTable = new List<List<TableCell>>();

        //Split Table
        List<List<TableCell>> SplitTable = new List<List<TableCell>>();


        public AI()
        {
            for (int i = 0; i <= 22; i++)
            {
                HardTable.Add(new List<TableCell>());
                for(int j = 0; j < 12; j++)
                {
                    HardTable[i].Add(new TableCell(1, 4));
                }
            }
            
            for (int i = 0; i <= 21; i++)
            {
                SplitTable.Add(new List<TableCell>());
                for (int j = 0; j < 12; j++)
                {
                    SplitTable[i].Add(new TableCell(4, 5));
                }
            }

            for (int i = 0; i <= 21; i++)
            {
                SoftTable.Add(new List<TableCell>());
                for (int j = 0; j < 12; j++)
                {
                    SoftTable[i].Add(new TableCell(0, 4));
                }
            }

            /*for (int j = 0; j <= 21; j++)
            {
                for (int i = 0; i <= 11; i++)
            }*/
        }

        public int Solve(List<Card> hand, int dealer, bool canDouble, bool canSplit, bool canSurrender, bool matchingCards)
        {
            int t;

            Console.WriteLine("Player has ");
            foreach (Card card in hand)
            {
                Console.Write(card.face + ", ");
            }
            Console.WriteLine("Dealer shows " + dealer);
            Console.WriteLine("Can double: " + canDouble);
            
            if (matchingCards && canSplit)
            {
                t = SplitTable[hand[0].value][dealer].getBest(canDouble, canSplit, canSurrender);
            }
            else if (hand.Find(card => card.face == 'A') != null)
            {
                t = SoftTable[hand.Sum(item => item.value)][dealer].getBest(canDouble, canSplit, canSurrender);
            }
            else
            {
                t = HardTable[hand.Sum(item => item.value)][dealer].getBest(canDouble, canSplit, canSurrender);
            }
                
            return t;
            
        }//Output: Hit 0, Stand 1, Double 2, Surrender 3, Split 4, invalid -1\

        public void update(List<Card> playerHand, int dealerCard, double hitUpdate, double standUpdate, double doubleUpdate, double surrUpdate, double splitUpdate)
        {
            Card softAce = playerHand.Find(card => card.face == 'A');

            if (playerHand.Count == 2 && playerHand[0].value == playerHand[1].value)
            {
                int tem = playerHand[0].value;
                if (tem == 1) tem += 10;
                if (dealerCard == 1) dealerCard += 10;
                SplitTable[tem][dealerCard].addEntry(0, hitUpdate);
                SplitTable[tem][dealerCard].addEntry(1, standUpdate);
                SplitTable[tem][dealerCard].addEntry(2, doubleUpdate);
                SplitTable[tem][dealerCard].addEntry(3, surrUpdate);
                SplitTable[tem][dealerCard].addEntry(4, splitUpdate);
                SplitTable[tem][dealerCard].incTimes();
            }
            else if (softAce != null && (softAce.value == 11 || playerHand.Sum(item => item.value) < 12))
            {
                int tem = playerHand.Sum(item => item.value);
                if (dealerCard == 1) dealerCard += 10;
                if (tem < 12) tem += 10;
                    SoftTable[tem][dealerCard].addEntry(0, hitUpdate);
                    SoftTable[tem][dealerCard].addEntry(1, standUpdate);
                    SoftTable[tem][dealerCard].addEntry(2, doubleUpdate);
                    SoftTable[tem][dealerCard].addEntry(3, surrUpdate);
                    SoftTable[tem][dealerCard].incTimes();
            }
            else
            {
                int tem = playerHand.Sum(item => item.value);
                if (dealerCard == 1) dealerCard += 10;
                HardTable[tem][dealerCard].addEntry(0, hitUpdate);
                HardTable[tem][dealerCard].addEntry(1, standUpdate);
                HardTable[tem][dealerCard].addEntry(2, doubleUpdate);
                HardTable[tem][dealerCard].addEntry(3, surrUpdate);
                HardTable[tem][dealerCard].incTimes();
            }
        }

        public void Print()
        {
            Console.WriteLine("\n\n\n");
            Console.WriteLine("Hard Table:");

            Console.Write("     ");
            for (int j = 2; j < 10; j++)
            {
                Console.Write(j + " | ");
            }
            Console.Write("T | ");
            Console.WriteLine("A | ");
            Console.WriteLine("---------------------------------");

            for (int j = 2; j < 22; j++)
            {
                if (j < 10) Console.Write(" ");
                Console.Write(j + " | ");
                for (int i = 2; i < 12; i++)
                {
                    Console.Write(HardTable[j][i].getBest() + " | ");
                }
                Console.Write("\n");
            }

            Console.WriteLine("Soft Table:");
            Console.Write("     ");
            for (int j = 2; j < 10; j++)
            {
                Console.Write(j + " | ");
            }
            Console.Write("T | ");
            Console.WriteLine("A | ");
            Console.WriteLine("---------------------------------");

            for (int j = 12; j < 22; j++)
            {
                if (j < 10) Console.Write(" ");
                Console.Write(j + " | ");
                for (int i = 2; i < 12; i++)
                {
                    Console.Write(SoftTable[j][i].getBest() + " | ");
                }
                Console.Write("\n");
            }

            Console.WriteLine("Split Table:");
            Console.Write("     ");
            for (int j = 2; j < 10; j++)
            {
                Console.Write(j + " | ");
            }
            Console.Write("T | ");
            Console.WriteLine("A | ");
            Console.WriteLine("---------------------------------");
            for (int j = 2; j < 12; j++)
            {
                if (j < 10) Console.Write(" ");
                Console.Write(j + " | ");
                for (int i = 2; i < 12; i++)
                {
                    Console.Write(SplitTable[j][i].getBest() + " | ");
                }
                Console.Write("\n");
            }

        }

        public void Save()
        {
            StreamWriter file = new StreamWriter("AIv2.txt");
            for (int j = 2; j < 22; j++)
            {
                //if (j < 10) file.Write( ");
                //file.WriteLine(j + " | ");
                for (int i = 2; i < 12; i++)
                {
                    file.Write(j + " " + i + " " + HardTable[j][i].getTimes() + " ");
                    foreach (double item in HardTable[j][i].getScores()){
                        file.Write(item + " ");
                    }
                    file.Write("\n");
                }
                //file.Write("\n");
            }

            for (int j = 12; j < 22; j++)
            {
                //if (j < 10) file.Write(" ");
                //file.WriteLine(j + " | ");
                for (int i = 2;  i < 12; i++)
                {
                    file.Write(j + " " + i + " " + SoftTable[j][i].getTimes() + " ");
                    foreach (double item in SoftTable[j][i].getScores())
                    {
                        file.Write(item + " ");
                    }
                    file.Write("\n");
                }
                //file.Write("\n");
            }

            for (int j = 2; j < 12; j++)
            {
                //if (j < 10) file.Write(" ");
                //file.WriteLine(j + " | ");
                for (int i = 2; i < 12; i++)
                {
                    file.Write(j + " " + i + " " + HardTable[j][i].getTimes() + " ");
                    foreach (double item in SplitTable[j][i].getScores())
                    {
                        file.Write(item + " ");
                    }
                    file.Write("\n");
                }
                //file.Write("\n");
            }
            file.Close();
        }

        public void Load()
        {
            StreamReader file = new StreamReader("AIv2.txt");
            //for each
            //for ()!file.EndOfStream
            for(int i = 0; i < 200; i++){
                string inText = file.ReadLine();
                string[] line = inText.Split(' ');
                //foreach (string entry in line) Console.Write(entry + " ");
                
                HardTable[int.Parse(line[0])][int.Parse(line[1])].Rewrite(line);
                //Console.WriteLine();
            }

            //Console.Read();

            for (int i = 0; i < 100; i++)
            {
                string inText = file.ReadLine();
                string[] line = inText.Split(' ');
                //foreach (string entry in line) Console.Write(entry + " ");

                SoftTable[int.Parse(line[0])][int.Parse(line[1])].Rewrite(line);
                //Console.WriteLine();
            }

            //Console.Read();

            for (int i = 0; i < 100; i++)
            {
                string inText = file.ReadLine();
                string[] line = inText.Split(' ');
                //foreach (string entry in line) Console.Write(entry + " ");

                SplitTable[int.Parse(line[0])][int.Parse(line[1])].Rewrite(line);
                //Console.WriteLine();
            }
            
            file.Close();
        }
    }
}