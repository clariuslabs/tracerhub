/*
   Copyright 2014 Clarius Consulting

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0
*/

[assembly: Microsoft.Owin.OwinStartup(typeof(TracerHub.Startup))]

namespace TracerHub
{
    using Owin;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}