using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EprinAppClient
{
    public class Person
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public override string ToString()
        {
            return $"{Id}: {FirstName} {LastName}";
        }
    }
}
