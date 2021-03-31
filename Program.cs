using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Diagnostics;

namespace GA_Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            
           
            Questions  QU = new Questions();
            Question[] questions = QU.Generator(10000);
            List<Question[]> population = new List<Question[]>();
            List<double> score = new List<double>();
            List<string> Know = new List<string>();
            while (Know.Count <= 8)
            {

                Random random = new Random(Guid.NewGuid().GetHashCode());
               string x =  random.Next(1, 100).ToString();
                if (!Know.Contains(x))
                    Know.Add(x);

            }
            PrintAndSee();
         



            while (true)
            {
                double maxscore = 0.0;
                Question[] bestindividul;
                int i = 0;
                population = FirstGeneration(questions, 600);

                for ( i = 0; i < 5000; i++)
                {
                    score = fitnessfunction(population, 6.5, Know);
                    bestindividul = BestIndividul(population, score, out maxscore);

                    population = Select(population, score);
                    population = Crossover(population);
                    population = Mutation(population, 0.03);
                    score = fitnessfunction(population, 6.5, Know);
                    bestindividul = BestIndividul(population, score, out maxscore);

                    Console.WriteLine("Generation:" + (i + 1).ToString() + "      " + "Polulation:" + population.Count + "       " + "Maxscore:" + maxscore);
                    if (maxscore > 0.9)
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("=================================================================================================");
                        Console.WriteLine("The best generation is founded,the score is " + maxscore + "  the generation is " + (i + 1).ToString());
                        Console.WriteLine("=================================================================================================");
                        break;

                    }
                }
                if (i == 4999)
                    Console.WriteLine("Don't find the best individul !! so plsase do begin to run new search");
                else break;
               


            }

            Console.ReadKey();


























            void PrintAndSee()
            {
                string know;
                string path = Directory.GetCurrentDirectory();
                if (File.Exists(path + "\\题库.txt"))
                    File.Delete(path + "\\题库.txt");


                FileStream stream = new FileStream(path + "\\题库.txt", FileMode.Create, FileAccess.ReadWrite);
                StreamWriter writer = new StreamWriter(stream);


                foreach (var item in questions)
                {
                    know = "";

                    foreach (var item2 in item.Knowledge)
                    {

                        know = know + item2.ToString() + ",";

                    }
                    writer.Write(item.ID + "       " + item.Type + "       " + know + "      " + item.Difficulty.ToString("f1") + "       " + item.Score);
                    writer.WriteLine();


                }

                writer.Close();
                stream.Close();
                Console.WriteLine("题库已经随机生成，点击任意键继续");
                Process.Start(path + "\\题库.txt");
                Console.ReadKey();




            }
             List<Question[]> FirstGeneration(Question[] questions2, int number)  //question2 是原始题库 number 为第一代染色体的数量
            {

                List<Question[]> first = new List<Question[]>();

              
               while (first.Count != number)
                {

                    List<Question> selectionA = new List<Question>();
                  
                    List<Question> selectionB = new List<Question>();
                    Question[] first1 = null;
                    Random random = new Random(Guid.NewGuid().GetHashCode());  
                    int x;
                    List<int> randdom = new List<int>();
                    
                     while (selectionA.Count != 20)
                        {
                            
                            x = random.Next(0, questions2.Length);
                        randdom.Add(x);
                        if (questions2[x].Type == 0 | questions2[x].Type == 1 | questions2[x].Type == 2)   //选择客观题 基因数 为20
                            {
                            if (!selectionA.Contains(questions2[x]))
                            {
                                Question qs = new Question();
                                qs.Difficulty = questions2[x].Difficulty;
                                qs.ID = questions2[x].ID;
                                qs.Knowledge = questions2[x].Knowledge;
                                qs.Score = questions2[x].Score;
                                qs.Type = questions2[x].Type;
                                
                                selectionA.Add(qs);
                             
                            }
                            }
                        }

                  

                    while (selectionB.Count != 5)
                        {
                           x = random.Next(0, questions2.Length);
                            Question qs1 = new Question();
                        if (questions2[x].Type == 3 | questions2[x].Type == 4)   //选择主观题 基因数为5
                        {
                            if (!selectionB.Contains(questions2[x]))
                            {
                                qs1.Difficulty = questions2[x].Difficulty;
                                qs1.ID = questions2[x].ID;
                                qs1.Knowledge = questions2[x].Knowledge;
                                qs1.Score = questions2[x].Score;
                                qs1.Type = questions2[x].Type;
                                selectionB.Add(qs1);
                            }
                        }

                        }


                  

                   selectionA.AddRange(selectionB);

                  




                         first1  =   selectionA.ToArray();
                    Question[] RQ = new Question[first1.Length];
                    for (int i = 0; i < first1.Length; i++)
                    {

                        Question qs2 = new Question();
                       RQ[i] = qs2;
                       RQ[i].Difficulty = first1[i].Difficulty;
                       RQ[i].ID = first1[i].ID;
                       RQ[i].Knowledge = first1[i].Knowledge;
                       RQ[i].Score = first1[i].Score;
                       RQ[i].Type = first1[i].Type;


                    }

                    first.Add(RQ);
                    


                }
                
                return first;




            }
             List<Question[]>Select(List<Question[]> generation, List<double> fitnessfuction)
            {

                double sum = 0.0;
                List<double> score0 = new List<double>();
                int k = 0;
                List<Question[]> result = new List<Question[]>();
                foreach (var item in fitnessfuction)
                {

                    sum = sum + item;
                   
                }

                foreach (var item2 in fitnessfuction)
                {

                    score0.Add(item2 / sum);

                }
                
                foreach (var item in score0)
                {
                    k++;
                    Random random = new Random(Guid.NewGuid().GetHashCode());
                    double j = random.NextDouble();
                    if (item <= j)
                        result.Add(generation[k - 1]);


                }


                return result;




            }
             List<Question[]>Crossover(List<Question[]> selection)
            {
                
                List<Question[]> result = new List<Question[]>();
                Random random = new Random(Guid.NewGuid().GetHashCode());
                int length = 0;
                if (selection.Count < 200)
                {
                    if(selection.Count < 50)
                       length= random.Next(0, selection.Count );
                   else length = random.Next(0, selection.Count / 4);
                    for (int i = 0; i < length; i++)
                    {
                        int n = random.Next(0, selection.Count);
                        result.Add(selection[n]);
                    }
                }
                
                while (selection.Count >1)
                {
                  
                    int k;
                    int x;
                    int y;
                    while (true)
                    {
                         x = random.Next(0, selection.Count);
                        y = random.Next(0, selection.Count);
                         k = random.Next(0, selection[x].Length - 2);
                        if (y != x)
                            break;
                    }
                    
                    for (int i = k; i < k + 2; i++)
                    {

                        string  ID = null;
                        ID = selection[y][i].ID;
                        double Difficulty = selection[y][i].Difficulty;
                        var Knowledge = selection[y][i].Knowledge;
                        var Score = selection[y][i].Score;
                        var type = selection[y][i].Type;
                        if (selection[y][i].ID != selection[x][i].ID)
                        {
                            selection[y][i].Difficulty = selection[x][i].Difficulty;
                            selection[y][i].ID = selection[x][i].ID;
                            selection[y][i].Knowledge = selection[x][i].Knowledge;
                            selection[y][i].Score = selection[x][i].Score;
                            selection[y][i].Type = selection[y][i].Type;

                        }
                        if (selection[x][i].ID != ID)
                        {
                            selection[x][i].Difficulty = Difficulty;
                            selection[x][i].ID = ID;
                            selection[x][i].Knowledge = Knowledge;
                            selection[x][i].Score = Score;
                            selection[x][i].Type =type;

                        }

                    }
                    result.Add(selection[x]);
                    result.Add(selection[y]);
                   
                    selection.RemoveAt(x);
                    if(y<x)
                    selection.RemoveAt(y);
                    if (y > x)
                    selection.RemoveAt(y - 1);
                    if (selection.Count == 1)
                        result.Add(selection[0]);
                   


                }
               
                return result;








            }
             List<Question[]> Mutation(List<Question[]> generation, double rate)
            {
                while (true)
                {
                   
                        Random random = new Random(Guid.NewGuid().GetHashCode());
                        double x = random.NextDouble();
                        if (x <= rate)
                        {
                        while (true)
                        {
                            int y = random.Next(0, generation.Count);
                            int z = random.Next(0, generation[y].Length);

                            Questions questions2 = new Questions();
                            Question[] questions1 = questions2.Generator(2);
                            if (questions1[0].Type == generation[y][z].Type)

                            {
                                generation[y][z] = questions1[0];
                                break;
                            }
                        }

                        }
                  
                    break;
                }

                return generation;


            }
             List<double> fitnessfunction(List<Question[]> generation, double DIFF,List<string> KNOW)  //DIFF 预设难度系数   KNOW 预设知识点
            {

                List<double> vs = new List<double>();
              
                foreach (Question[] item in generation)
                {

                    double difficulty = 0.0;
                    List<string> knowledge = new List<string>();
                    int k = 0;
                    double result = 0.0;
                    foreach (var item2 in item)
                    {

                        difficulty = difficulty + item2.Difficulty;
                        foreach (var item3 in item2.Knowledge)
                        {

                            if (!knowledge.Contains(item3.ToString()))
                                knowledge.Add(item3.ToString());


                        }

                    }
                    foreach (var item4 in KNOW)
                    {

                        if (knowledge.Contains(item4))
                            k++;
                         

                    }
                    difficulty = difficulty / item.Length;
                    result =( 1 - (1 - (k / KNOW.Count))*0.5 - Math.Abs(difficulty - DIFF) * 0.5);

                    vs.Add(result);

                }

                return vs;

            }
             Question[] BestIndividul(List<Question[]> generation,List<double> score1, out double maxscore0)
            {

                double max = score1.Max();
                int x = 0;
                int n = 0;
                foreach (var item in score1)
                {

                    if (item == max)
                        x = n;
                    n++;
                }

                maxscore0 = max;
                return generation[x];


            }

        }
    }


    







}
