﻿using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchard.ContentManagement.Handlers
{
    public class ActivatedContentContext
    {
        public string ContentType { get; set; }
        public ContentItem ContentItem { get; set; }
    }
}
