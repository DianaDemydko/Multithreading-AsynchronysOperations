using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Multi_Threading_Camp
{
    class Program
    {
        private const string URL = "https://docs.microsoft.com/en-us/dotnet/csharp/csharp";
        static async Task Main(string[] args)
        {
            #region Threads Example

            //Thread someAnotherThread = new Thread(new ThreadStart(LongPerformMethodThread));

            //Console.WriteLine(someAnotherThread.ThreadState);

            //someAnotherThread.Start();

            //Console.WriteLine(someAnotherThread.ThreadState);

            //ShortPerformMethod();

            //Console.WriteLine(someAnotherThread.IsAlive);

            #endregion


            #region Task Using

            //var task = Task.Run(() => Console.WriteLine("Async task"));

            //var nextTask = task.ContinueWith((prevTask) =>
            //{
            //    Console.WriteLine("Continuation");
            //});

            //nextTask.ContinueWith((prevTask) =>
            //{
            //    Console.WriteLine("Last Continuation");
            //});

            //Console.ReadLine();

            #endregion


            #region Cancellation of Tasks

            //var cts = new CancellationTokenSource();

            //var token = cts.Token;

            //token.Register(() => Console.WriteLine("Token works"));

            ////Create and run some task
            //var t = Task.Run(async () =>
            //{
            //    //Perform the cancellation
            //    cts.Cancel();
            //    //Pass the cancelled token to the Delay Task, that Token will throw the exeption
            //    await Task.Delay(10000, token);

            //    //Or use ThrowIfCancellationRequested
            //    token.ThrowIfCancellationRequested();

            //}, token);

            //try
            //{
            //    //waiting for the task
            //    await t;
            //}
            ////TaskCanceledException
            //catch (TaskCanceledException e)
            //{
            //    Console.WriteLine(e.Message + " TaskCanceledException");
            //}
            //catch (OperationCanceledException e)
            //{
            //    Console.WriteLine(e.Message + " OperationCanceledException");
            //}

            #endregion



            #region Async/Await Example

            //DoSynchronousWork();

            //var someTask = DoSomethingAsync();

            //DoSynchronousWorkAfterAwait();

            //await someTask;

            //Console.WriteLine("Finish");

            //Console.ReadLine();

            #endregion
        }

        public static void LongPerformMethodThread()
        {
            Console.WriteLine("Long Perform Started");

            Thread.Sleep(5000);

            Console.WriteLine("Long Perform Finished");
        }

        public static void LongPerformMethodTask()
        {
            Console.WriteLine("Long Perform Started");

            Task.Delay(5000);

            Console.WriteLine("Long Perform Finished");
        }

        public static void ShortPerformMethod()
        {
            Console.WriteLine("Short Perform Method");
        }

        public static void DoSynchronousWork()
        {
            // You can do whatever work is needed here
            Console.WriteLine("1. Doing some work synchronously");
        }

        static async Task DoSomethingAsync() //A Task return type will eventually yield a void
        {
            Console.WriteLine("2. Async task has started...");
            await GetStringAsync(); // we are awaiting the Async Method GetStringAsync
        }

        static async Task GetStringAsync()
        {
            using (var httpClient = new HttpClient())
            {
                Console.WriteLine("3. Awaiting the result of GetStringAsync of Http Client...");
                string result = await httpClient.GetStringAsync(URL); //execution pauses here while awaiting GetStringAsync to complete

                //From this line and below, the execution will resume once the above awaitable is done
                //using await keyword, it will do the magic of unwrapping the Task<string> into string (result variable)
                Console.WriteLine("4. The awaited task has completed. Let's get the content length...");
                Console.WriteLine($"5. The length of http Get for {URL} {result.Length} character");
            }
        }

        static void DoSynchronousWorkAfterAwait()
        {
            //This is the work we can do while waiting for the awaited Async Task to complete
            Console.WriteLine("7. While waiting for the async task to finish, we can do some unrelated work...");
            for (var i = 0; i <= 5; i++)
            {
                for (var j = i; j <= 5; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }

        }
    }
}
