/*
   Copyright 2014 Clarius Consulting SA

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0
*/

namespace TracerHub
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TraceEvent
    {
        public TraceEventType EventType { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
    }
}
