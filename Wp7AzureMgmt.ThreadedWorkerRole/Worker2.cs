﻿//-----------------------------------------------------------------------
// <copyright file="Worker2.cs" company="Berry International">
//     Copyright (c) 2012 Berry International. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ThreadedWorkerRole
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.ServiceRuntime;
    
    /// <summary>
    /// Worker2 contains the entire cycle of the worker thread
    /// </summary>
    public class Worker2 : WorkerEntryPoint
    {
       /// <summary>
       /// Run is the function of an working cycle
       /// </summary>
        public override void Run()
        {
            Trace.TraceInformation("Worker2:Run begin", "Information");

            try
            {
                while (true)
                {
                    // CHANGE SLEEP TIME
                    Thread.Sleep(this.Seconds45);

                    //// ADD CODE HERE

                    string traceInformation = DateTime.UtcNow.ToString() + " Worker2:Run loop thread=" + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
                    Trace.TraceInformation(traceInformation, "Information");
                }
            }
            catch (SystemException se)
            {
                Trace.TraceError("Worker2:Run SystemException", se.ToString());
                throw se;
            }
            catch (Exception ex)
            {
                Trace.TraceError("Worker2:Run Exception", ex.ToString());
            }

            Trace.TraceInformation("Worker2:Run end", "Information");
        }
    }
}