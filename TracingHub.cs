/*
   Copyright 2014 Clarius Consulting SA

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0
*/

namespace TracerHub
{
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
	using TracerHub.Diagnostics;
	using TracerHub.Properties;

    [HubName("Tracer")]
    public class TracingHub : Hub
    {
        private static ITracer tracer = Tracer.Get(typeof(TracingHub).Namespace);

		/// <summary>
		/// The group that the hub uses to trace its own internal messages.
		/// </summary>
		public const string SelfGroup = "tracerhub";

        public void TraceEvent(TraceEvent trace)
        {
            Clients.OthersInGroup(Context.QueryString["groupName"]).TraceEvent(trace);

			if (ShouldTrace(Context))
				// For security reasons, we never broadcast via the hub the actual message payloads.
				tracer.Verbose(Strings.Trace.TraceEvent(trace.Source, trace.EventType));
        }

        public void SetTracingLevel(string source, SourceLevels level)
        {
            Clients.OthersInGroup(Context.QueryString["groupName"]).SetTracingLevel(source, level);

			if (ShouldTrace(Context))
				tracer.Verbose(Strings.Trace.SetTraceLevel(source, level));
		}

        public override Task OnConnected()
        {
            var groupName = Context.QueryString["groupName"];
            if (string.IsNullOrEmpty(groupName))
            {
                tracer.Warn(Strings.Trace.NoGroupName(Context.Request.GetHttpContext().Request.UserHostAddress));
                throw new HubException(Strings.TracerHub.GroupNameRequired);
            }

            Groups.Add(Context.ConnectionId, groupName);
			if (ShouldTrace(Context))
				tracer.Info(Strings.Trace.Connected(Context.Request.GetHttpContext().Request.UserHostAddress, groupName));

            return base.OnConnected();
        }

		public override Task OnDisconnected()
		{
			if (ShouldTrace(Context))
				tracer.Info(Strings.Trace.Disconnected(Context.Request.GetHttpContext().Request.UserHostAddress, Context.QueryString["groupName"]));
			
			return base.OnDisconnected();
		}

		public override Task OnReconnected()
		{
			if (ShouldTrace(Context))
				tracer.Info(Strings.Trace.Reconnected(Context.Request.GetHttpContext().Request.UserHostAddress, Context.QueryString["groupName"]));
			
			return base.OnReconnected();
		}

		private static bool ShouldTrace(HubCallerContext context)
		{
			return !SelfGroup.Equals(context.QueryString["groupName"], StringComparison.InvariantCultureIgnoreCase);
		}
    }
}