using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_Algorithm
{
      public class Questions
    {

        public Question[] Generator(int Number)
        {

            Random random = new Random(Guid.NewGuid().GetHashCode());
           
            Question[] question = new Question[Number];
            for (int i = 0; i < Number; i++)
            {
                question[i] = new Question();
                int x = Number.ToString().Length;
                question[i].ID = i.ToString().PadLeft(x, '0');
            
                 int y =  random.Next(0, 4);  //5种题型
                 question[i].Type = y;

                if (y == 0)
                    question[i].Score = 3;
                if (y == 1)
                    question[i].Score = 5;
                if (y == 2)
                    question[i].Score = 2;
                if (y == 3)
                    question[i].Score = 8;
                if (y == 4)
                    question[i].Score = 6;  //每种题型的分值
                double z = random.NextDouble() * 10;
                question[i].Difficulty = z;    //难度系数 1-10

                int k = random.Next(1, 5); //知识点的总数为30

                List<int> vs = new List<int>();
               
                while (vs.Count != k)  //覆盖的知识点
                {
                    int k1 = random.Next(1, 100);
                    if (!vs.Contains(k1))
                        vs.Add(k1);

                }



                question[i].Knowledge = vs.ToArray();



            }

            return question;




        }
            

    }
    public class Question
    {
        public string ID { get; set; }
        public double Difficulty { get; set; }
        public int Score { get; set; }
        public int[] Knowledge { get; set; }

        public int Type { get; set; }

       
    }
}
