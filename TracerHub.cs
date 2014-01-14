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

    [HubName("Tracer")]
    public class TracerHub : Hub
    {
        public void TraceEvent(TraceEvent trace)
        {
            Clients.OthersInGroup(Context.QueryString["groupName"]).TraceEvent(trace);
        }

        public void SetTracingLevel(string source, SourceLevels level)
        {
            Clients.OthersInGroup(Context.QueryString["groupName"]).SetTracingLevel(source, level);
        }

        public override Task OnConnected()
        {
            var groupName = Context.QueryString["groupName"];
            if (string.IsNullOrEmpty(groupName))
                throw new HubException("Query string value 'groupName' must be specified.");

            Groups.Add(Context.ConnectionId, groupName);

            return base.OnConnected();
        }
    }
}