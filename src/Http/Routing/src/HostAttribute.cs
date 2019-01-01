// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AspNetCore.Routing
{
    public class HostAttribute : Attribute, IHostMetadata
    {
        public IReadOnlyList<string> Hosts { get; }

        public HostAttribute(params string[] hosts)
        {
            if (hosts == null)
            {
                throw new ArgumentNullException(nameof(hosts));
            }

            Hosts = hosts.ToArray();
        }
    }
}
