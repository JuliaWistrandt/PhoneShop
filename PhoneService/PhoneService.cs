using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhoneShop.Models;


namespace PhoneShop.Service
{
    public partial class PhoneService                                      
    {
        private int _nextId;

        private readonly string pathToFile = @".\wwwroot\db\phones.json"; 
        List<Phone> phones= new List<Phone>(); 

        public string GenerateId()
        {
            //return Guid.NewGuid().ToString(); - an alternative method for generating a random id  
            _nextId++;
            return $"TEL" + _nextId; 
        }
        public void AddPhoneToList(Phone phone)
        {
            DeserializeList();
            phone.Id = GenerateId(); 
            phones.Add(phone); 
            SerializeList(); 
        }

        public void RemovePhone(string id)
        {
            DeserializeList();
            var phone = phones.Find((phone) => phone.Id == id);
            phones.Remove(phone);
            SerializeList();
        }

        public List<Phone> ShowPhones()
        {
            DeserializeList(); 
            return phones; 
        }

        public Phone ShowPhoneById(string id)
        {
            DeserializeList();
            return phones.Find((phone) => phone.Id == id);
        }

        private void SerializeList() 
        {
            string json = JsonConvert.SerializeObject(phones); 
            File.WriteAllText(pathToFile, json);
        }

        private void DeserializeList() 
        {
            string json = ReadFileToString();
            phones = JsonConvert.DeserializeObject<List<Phone>>(json) ?? new List<Phone>();
        }

        private string ReadFileToString()                                    
        {
            if (!File.Exists(pathToFile)) 
            {
                File.WriteAllText(pathToFile, "");                                      
            }
            return File.ReadAllText(pathToFile);                                       
        }

    }
}
