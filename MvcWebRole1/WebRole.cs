//-----------------------------------------------------------------------
// <copyright file="WebRole.cs" company="Berry International">
//     Copyright (c) 2012 Berry International. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace MvcWebRole1
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Diagnostics;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using ThreadedWorkerRole;
    
    /// <summary>
    /// Manages creation, usage and deletion of workers in threads
    /// </summary>
    public class WebRole : ThreadedRoleEntryPoint
    {
        /// <summary>
        /// Treated as WebRole Start
        ///     If the OnStart method returns false, the instance is immediately stopped.
        ///     If the method returns true, then Windows Azure starts the role by calling
        ///     the Microsoft.WindowsAzure.ServiceRuntime.RoleEntryPoint.Run() method.        
        /// </summary>
        /// <returns>bool success</returns>
        public override bool OnStart()
        {
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            Trace.TraceInformation("WebRole::OnStart ", "Information");

            // Setup Azure Dignostics so tracing is captured in Azure Storage
            this.DiagnosticSetup();

            return base.OnStart();
        }

        /// <summary>
        /// Treated as WorkerRole Run
        /// </summary>
        public override void Run()
        {
            Trace.TraceInformation("WebRole::Run begin", "Information");
            
            List<WorkerEntryPoint> workers = new List<WorkerEntryPoint>();

            // ONLY CHANGE SHOULD BE TO ADD OR REMOVE FROM THE NEXT TWO LINES
            // MORE OR LESS ADDITIONS
            // WITH SAME OR DIFFERENT WORKER CLASSES
            workers.Add(new Worker1());
            workers.Add(new Worker2());

            base.Run(workers.ToArray());

            Trace.TraceInformation("WebRole::Run end", "Information");
        }

        /// <summary>
        /// This sets up Azure Diagnostics to Azure Storage. 
        /// </summary>
        private void DiagnosticSetup()
        {
            // Transfer to Azure Storage on this rage
            TimeSpan transferTime = TimeSpan.FromMinutes(1);

            DiagnosticMonitorConfiguration dmc = DiagnosticMonitor.GetDefaultInitialConfiguration();

            // Transfer logs to storage every cycle
            dmc.Logs.ScheduledTransferPeriod = transferTime;

            // Transfer verbose, critical, etc. logs
            dmc.Logs.ScheduledTransferLogLevelFilter = LogLevel.Verbose;

            System.Diagnostics.Trace.Listeners.Add(new Microsoft.WindowsAzure.Diagnostics.DiagnosticMonitorTraceListener());

            System.Diagnostics.Trace.AutoFlush = true;

#if DEBUG
            var account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            // USE CLOUD STORAGE    
            var account = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString"));
#endif

            // Start up the diagnostic manager with the given configuration
            DiagnosticMonitor.Start(account, dmc);
        }
    }
}
