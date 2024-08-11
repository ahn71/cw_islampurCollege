using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Classes
{
    public class projectTask
    {
        string value1;
        string value2;
        string value3;

        public string Value3
        {
            get { return value3; }
        }
        public string Value1
        {
            get { return value1; }
        }     
        public string Value2
        {
            get { return value2; }
        }

        public projectTask(string val1, string val2, string val3)
        {
            this.value1 = val1;
            this.value2 = val2;
            this.value3 = val3;
        }

        public projectTask(string val1, string val2)
        {
            this.value1 = val1;
            this.value2 = val2;
        }

        public projectTask(string val1)
        {
            this.value1 = val1;
        }

        public projectTask()
        {

        }

        public double calculation(double val1, double val2)
        {
            double sum = val1 + val2;
            return sum;
        }

        public static void MethodA() //Function Overloading
        {

        }

        public static void MethodA(string a) //Function Overloading
        {

        }
    }
}