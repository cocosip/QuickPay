using DotCommon.Caching;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.Assist.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuickPay.Tests.Assist.Store
{
    public class CacheTableTest : TestBase
    {
        [Fact]
        public void GetSet_Test()
        {

            var tableKey1 = "tb:TestTableClass1";
            var cache1 = Provider.GetService<IDistributedCache<List<TestTableClass1>>>();
            var table1 = new List<TestTableClass1>();
            table1.Add(new TestTableClass1(1, "zhangsan"));
            Assert.NotNull(table1);
            cache1.Set(tableKey1, table1);

            var queryTable1 = cache1.Get(tableKey1);
            var queryTestTableClass1 = queryTable1.FirstOrDefault();
            Assert.Equal(1, queryTestTableClass1.Id);
            Assert.Equal("zhangsan", queryTestTableClass1.Name);

        }
    }

    [Serializable]
    public class TestTableClass1
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TestTableClass1()
        {


        }
        public TestTableClass1(int id, string name)
        {
            this.Id = id;
            this.Name = name;

        }

    }

    [Serializable]
    public class TestTableClass2
    {
        public int Id { get; set; }

        public string PhoneNumber { get; set; }
    }
}
