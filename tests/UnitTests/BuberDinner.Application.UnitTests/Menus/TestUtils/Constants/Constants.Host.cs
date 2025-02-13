using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuberDinner.Domain.Host.ValueObjects;

namespace BuberDinner.Application.UnitTests.Menus.TestUtils.Constants
{
    public static partial class Constants
    {
        public static class Host
        {
            public static readonly HostId Id=HostId.Create("Host id");
        }
    }
}
