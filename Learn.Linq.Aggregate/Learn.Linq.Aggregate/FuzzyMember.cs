namespace Learn.Linq.Aggregate
{
    using System;

    public class FuzzyMember   
    {
        private long id;
        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int age;
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        private MemberEnum memberType;
        public MemberEnum MemberType 
        {
            get { return memberType; }
            set { memberType = value; }
        }
    }
}
