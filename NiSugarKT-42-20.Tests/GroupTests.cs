using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NiSugarKT_42_20.Models;

namespace NiSugarKT_42_20.Tests
{
    public class GroupTests
    {
        [Fact]
        public void IsValidGroupName_True()
        {
            var GroupName = new Group
            {
                GroupName = "КТ-42-20"
            };

            var result = GroupName.IsValidGroupName();

            Assert.False(result);

        }
    }
}