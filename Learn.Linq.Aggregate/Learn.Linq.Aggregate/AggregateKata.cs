using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Learn.Linq.Aggregate
{
    [TestClass]
    public class AggregateKata
    {
      [TestMethod]
        public void must_group_and_display_correct_seed_value()
        {
            var fuzzyList = new List<FuzzyMember>() 
            { 
               new FuzzyMember { Id = 0, Age = 4, MemberType = MemberEnum.Cat, Name = "Silver"},

               new FuzzyMember { Id = 1, Age = 3, MemberType = MemberEnum.Cat, Name = "Lucky"},

               new FuzzyMember { Id = 2, Age = 5, MemberType = MemberEnum.Snake, Name = "Jin"},

               new FuzzyMember { Id = 3, Age = 13, MemberType = MemberEnum.Dog, Name = "Luke"},

               new FuzzyMember { Id = 4, Age = 5, MemberType = MemberEnum.Cat, Name = "Tracy"},
            };

            var member =  fuzzyList.Aggregate(new FuzzyMember(), (a, b) =>
            {
                a.Name += b.Name;
                return a;
            });
        }
    }
}
