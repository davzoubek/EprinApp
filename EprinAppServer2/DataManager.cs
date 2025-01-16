using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace EprinAppServer2
{
    public class DataManager
    {
        private readonly string _filePath;
        private readonly List<Person> _people;
        private int _nextId;

        public DataManager(string filePath)
        {
            _filePath = filePath;
            _people = LoadPeople();
            _nextId = _people.Count > 0 ? _people[^1].Id + 1 : 1;
        }

        public List<Person> GetAllPeople() => new List<Person>(_people);

        public Person AddPerson(string firstName, string lastName)
        {
            var person = new Person { Id = _nextId++, FirstName = firstName, LastName = lastName };
            _people.Add(person);
            SavePeople();
            return person;
        }

        public bool UpdatePerson(int id, string firstName, string lastName)
        {
            var person = _people.Find(p => p.Id == id);
            if(person == null) return false;

            person.FirstName = firstName;
            person.LastName = lastName;
            SavePeople();
            return true;
        }

        public bool DeletePerson(int id)
        {
            var person = _people.Find(p => p.Id == id);
            if (person == null) return false;

            _people.Remove(person);
            SavePeople();
            return true;
        }

        private List<Person> LoadPeople()
        {
            if(!File.Exists(_filePath)) return new List<Person>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Person>>(json) ?? new List<Person>();
        }

        private void SavePeople()
        {
            var json = JsonSerializer.Serialize(_people);
            File.WriteAllText(_filePath, json);
        }
    }
}
