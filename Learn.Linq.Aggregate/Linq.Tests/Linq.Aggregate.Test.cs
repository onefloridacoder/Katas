namespace Linq.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Learn.Linq.Aggregate;

    [TestClass]
    public class LinqTests
    {
        IList<FuzzyMember> FuzzyList {get; set;}
        int NameListLength { get; set; }
        
        [TestInitialize]
        public void BuildTestData()
        {
            var fuzzyList = new List<FuzzyMember>() 
            { 
               new FuzzyMember { Id = 0, Age = 4, MemberType = MemberEnum.Cat, Name = "Silver"},

               new FuzzyMember { Id = 1, Age = 3, MemberType = MemberEnum.Cat, Name = "Lucky"},

               new FuzzyMember { Id = 2, Age = 5, MemberType = MemberEnum.Snake, Name = "Jin"},

               new FuzzyMember { Id = 3, Age = 13, MemberType = MemberEnum.Dog, Name = "Luke"},

               new FuzzyMember { Id = 4, Age = 5, MemberType = MemberEnum.Cat, Name = "Tracy"},
            };

            this.FuzzyList = fuzzyList;

            var names = this.FuzzyList.Aggregate(new FuzzyMember(), (a, b) =>
                {
                    a.Name += b.Name;
                    return a;
                });

            this.NameListLength = names.Name.Length;
        }
        
        [TestMethod]
        public void must_group_and_display_correct_seed_value()
        {
            var member = this.FuzzyList.Aggregate(new FuzzyMember(), (a, b) =>
            {
                a.Name += b.Name;
                return a;
            });

            Assert.AreNotEqual(this.FuzzyList[0].Name, member.Name); // "Silver" != "SilverLuckyJinLukeTracy"
        }

        [TestMethod]
        public void must_group_and_append_to_string_builder()
        {
            var memberString = this.FuzzyList.Aggregate(new StringBuilder(), (a, b) => a.Append(b.Name));

            var expected = this.NameListLength;
            var actual = memberString.Length;
            
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void must_sum_ages()
        {
            var actual = this.FuzzyList.Aggregate(0, (seed, b) => seed + b.Age);
            var expected = 30;

            Assert.AreEqual(expected, actual);
        }
    }
}
