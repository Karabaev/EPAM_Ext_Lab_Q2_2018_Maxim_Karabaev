using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using static System.Console;
namespace Task3.Model
{
    internal class Task3_13 : Task
    {
        private long elapsedTicksToString = 0, elapsedTicksToStringBuilder = 0;
        const int n = 100000;
        const string pathToFile = @"C:\Проекты\EPAM\Task3\Task3\Log.log";
        private string stringLogOut, stringBuilderLogOut, averageStringLogOut, averageStringBuilderogOut;
        uint testCount = 1;
        float elapsedSecondsForAllTests = 0;
        int testIteration = 0;
        public Task3_13()
        {
            Switcher = Switcher.Task3_13;
            Name = Repository.TaskNames[Switcher];
        }
        public override void GetResult()
        {
            WriteLine("Results for {0} tests:", testCount);
            WriteLine(averageStringLogOut);
            WriteLine(averageStringBuilderogOut);
            WriteLine("Total time elapsed: {0} seconds", elapsedSecondsForAllTests);
        }

        public override void MenuItem()
        {
            //Thread stringThread = new Thread(ActionsWithString);
            //stringThread.Start();
            //Thread stringBuilderThread = new Thread(ActionsWithStringBuilder);
            //stringBuilderThread.Start();
            elapsedTicksToString = 0;
            elapsedTicksToStringBuilder = 0;
            elapsedSecondsForAllTests = 0;
            while (true)
            {
                WriteLine("Enter count of tests:");
                if (uint.TryParse(ReadLine(), out testCount) && testCount > 0)
                    break;
                else
                    WriteLine("Value is invalid, enter please integer greater then 0.");
            }
            WriteLog(pathToFile, "-------------Starting tests-------------");
            for (testIteration = 0; testIteration < testCount; testIteration++)
            {
                ActionsWithString();
                ActionsWithStringBuilder();
                WriteLine("Test {0} completed.", testIteration + 1);
            }
            WriteLog(pathToFile, "--------------Ending tests--------------");
            averageStringLogOut = String.Format("Average time elapsed to string: {0} ticks", (float)elapsedTicksToString / testCount);
            averageStringBuilderogOut = String.Format("Average time elapsed to string builder: {0} ticks", (float)elapsedTicksToStringBuilder / testCount);
            WriteLog(pathToFile, averageStringLogOut);
            WriteLog(pathToFile, averageStringBuilderogOut);
        }

        private void ActionsWithString()
        {
            Stopwatch sw = new Stopwatch();
            string str = String.Empty;
            sw.Start();
            for (int i = 0; i < n; i++)
                str += "*";
            sw.Stop();
            elapsedTicksToString += sw.ElapsedTicks;
            elapsedSecondsForAllTests += sw.ElapsedMilliseconds / 1000.0f;
            stringLogOut = String.Format("Test {0}: Elapsed time to string: {1} ticks", testIteration + 1, sw.ElapsedTicks);
            WriteLog(pathToFile, stringLogOut);
        }
        private void ActionsWithStringBuilder()
        {
            Stopwatch sw = new Stopwatch();
            StringBuilder strBuild = new StringBuilder();
            sw.Start();
            for (int i = 0; i < n; i++)
                strBuild.Append("*");
            sw.Stop();
            elapsedTicksToStringBuilder += sw.ElapsedTicks;
            elapsedSecondsForAllTests += sw.ElapsedMilliseconds / 1000.0f;
            stringBuilderLogOut = String.Format("Test {0}: Elapsed time to string builder: {1} ticks", testIteration + 1, sw.ElapsedTicks);
            WriteLog(pathToFile, stringBuilderLogOut);
        } 
    }
}
