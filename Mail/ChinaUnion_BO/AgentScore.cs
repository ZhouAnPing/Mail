using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_BO
{
    public class AgentScore
    {
        public String agentNo;
        public String agentName;
        public String branchNo;
        public String branchName;
        public String score;
        public String standardScore;
        public String dateTime;

        public AgentScore(){}

        public AgentScore(String str)
        {
            String[] array = str.Split(';');

            
            dateTime = array[0];
            agentNo = array[1];
            agentName = array[2];
            branchNo = array[3];
            branchName = array[4];
            score = array[5];
            standardScore = array[5];

            Console.Write( "__" + dateTime);
            Console.Write("__" + agentNo);
            Console.Write("__" + agentName);
            Console.Write("__" + branchNo);
            Console.Write("__" + branchName);
            Console.Write("__" + score);
            Console.Write("__" + standardScore);
        }
        
    }
}
