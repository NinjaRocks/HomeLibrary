using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using MicroFx;
using MicroFx.Data.Migration;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace HomeLibrary.API
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly IDisposable app = null;

        private readonly MicroFx.MicroService microService = null;
       
        
        // The name of your queue
        const string QueueName = "homelibraryapp";

        // QueueClient is thread-safe. Recommended that you cache 
        // rather than recreating it on every request
        //QueueClient Client;
        ManualResetEvent CompletedEvent = new ManualResetEvent(false);

        public WorkerRole()
        {
            microService = new MicroFx.MicroService(new ServiceSettings())
                .WithStartupTask(() => new DbMigrator(new DbConnectionProvider(), new ScriptDirectoryProvider()));
        }

        public override void Run()
        {
            Trace.WriteLine("Starting processing of messages");
            while (true)
            {
                Thread.Sleep(10000);
                Trace.TraceInformation("Working", "Information");
            }


            // Initiates the message pump and callback is invoked for each message that is received, calling close on the client will stop the pump.
            //Client.OnMessage((receivedMessage) =>
            //    {
            //        try
            //        {
            //            // Process the message
            //           // Trace.WriteLine("Processing Service Bus message: " + receivedMessage.SequenceNumber.ToString());
            //        }
            //        catch
            //        {
            //            // Handle any message processing specific exceptions here
            //        }
            //    });

            //CompletedEvent.WaitOne();
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // New code:
            var endpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["Endpoint"];
            var baseUri = $"{endpoint.Protocol}://{endpoint.IPEndpoint}";

            Trace.TraceInformation($"Starting OWIN at {baseUri}", "Information");

           microService.Start<Startup>(baseUri);

            //app = WebApp.Start<Startup>(new StartOptions(url: baseUri));

            //// Create the queue if it does not exist already
            //var connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            //var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            //if (!namespaceManager.QueueExists(QueueName))
            //    namespaceManager.CreateQueue(QueueName);

            //// Initialize the connection to Service Bus Queue
            //Client = QueueClient.CreateFromConnectionString(connectionString, QueueName);


            return base.OnStart();
        }

        public override void OnStop()
        {
            app?.Dispose();

            microService.Stop();
        
            // Close the connection to Service Bus Queue
            // Client.Close();
            CompletedEvent.Set();
            base.OnStop();
        }
    }
}
