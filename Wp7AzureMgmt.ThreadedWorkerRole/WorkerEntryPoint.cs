//-----------------------------------------------------------------------
// <copyright file="WorkerEntryPoint.cs" company="Berry International">
//     Copyright (c) 2012 Berry International. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ThreadedWorkerRole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    
    /// <summary>
    /// Model for Workers
    /// </summary>
    public class WorkerEntryPoint
    {
        /// <summary>
        /// Cycle rate of 30 seconds
        /// </summary>
        public readonly int Seconds30 = 30000;

        /// <summary>
        /// Cycle rate of 45 seconds
        /// </summary>
        public readonly int Seconds45 = 45000;

        /// <summary>
        /// OnStart method for workers
        /// </summary>
        /// <returns>bool for success</returns>
        public virtual bool OnStart()
        {
            return true;
        }

        /// <summary>
        /// Run method
        /// </summary>
        public virtual void Run()
        {
        }

        /// <summary>
        /// OnStop method
        /// </summary>
        public virtual void OnStop()
        {
        }

        /// <summary>
        /// This method prevents unhandled exceptions from being thrown
        /// from the worker thread.
        /// </summary>
        internal void ProtectedRun()
        {
            try
            {
                // Call the Workers Run() method
                this.Run();
            }
            catch (SystemException)
            {
                // Exit Quickly on a System Exception
                throw;
            }
            catch (Exception)
            {
            }
        }
    }
}