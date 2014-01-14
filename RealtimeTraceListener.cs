namespace TracerHub.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.AspNet.SignalR.Client;
    using System.Diagnostics;
    using System.IO;

    partial class RealtimeTraceListener
    {
		public RealtimeTraceListener(string groupName, string hubUrl)
			: this(groupName)
		{
			this.hubUrl = hubUrl;
		}

        partial void OnConnecting()
        {
            Proxy.On<string, SourceLevels>("SetTracingLevel", SetTracingLevel);
        }

        private void SetTracingLevel(string source, SourceLevels level)
        {
            Tracer.Manager.SetTracingLevel(source, level);
        }
    }

    partial class Tracer
    {
        public static ITracerManager Manager { get { return manager; } }

        partial class DefaultManager
        {
            public void SetTracingLevel(string sourceName, SourceLevels level)
            {
            }
        }
    }

    partial interface ITracerManager
    {
        void SetTracingLevel(string sourceName, SourceLevels level);
    }
}
