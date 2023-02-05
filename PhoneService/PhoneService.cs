using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhoneShop.Models;


namespace PhoneShop.Service
{
    public partial class PhoneService   // здесь лежит вся бизнес логика, модели в папке models должны оставаться нетронутыми,
                                        // здесь мы пишем все что с моделями делать хотим 
    {
        private readonly string pathToFile = @".\wwwroot\db\phones.json"; // адрес где лежит наш файл в формате json, в будущем это база данных
        List<Phone> phones= new List<Phone>(); // лист куда мы будем записывать, удалять или показывать наши телефоны

        public string GenerateId() // генерируем рандомное id  
        {
            return Guid.NewGuid().ToString();
        
        }
        public void AddPhoneToList(Phone phone)
        {
            DeserializeList();// выгрузим из базы данных свеженький лист со всеми телефонами
            phone.Id = GenerateId(); // присвоим новенькому телефону id
            phones.Add(phone); //добавим в этот свеженький лист новый телефон
            SerializeList(); // загрузим обновленный лист обратно в базу данных
        }

        public void DeletePhoneById(string id)
        {
            DeserializeList();
            var phone = phones.FirstOrDefault(x => x.Id == id);
            phones.Remove(phone);
            SerializeList();
        }

        public List<Phone> ShowPhones()
        {
            DeserializeList(); // выгрузим из базы данных свеженький лист со всеми телефонами
            return phones; // вернем этот лист тому методу, кто его попросит дальше
        }

        private void SerializeList() // json язык сервера, этот метод позволяет загрузить лист(базу данныых) с телефонами с сервера
        {
            string json = JsonConvert.SerializeObject(phones); 
            File.WriteAllText(pathToFile, json);
        }

        private void DeserializeList() // а этот метод позволяет загрузить лист(базу данныых) с телефонами обратно на сервер
        {
            string json = ReadFileToString();
            phones = JsonConvert.DeserializeObject<List<Phone>>(json) ?? new List<Phone>();
        }

        private string ReadFileToString() // этот метод превращает данные с сервера из формата json (которые c# не может считать корретно)
                                          // в формат строки (а вот это с# может)
        {
            if (!File.Exists(pathToFile)) // если такой файл НЕ существует (например, путь "../document/repos/database.json"), погугли File.Exists
            {
                File.WriteAllText(pathToFile, ""); //создание нового файла, запись указанной строки в файл,
                                                   //а затем закрытие файла(путь pathToFile, содержимое "") гугли  File.WriteAllText
            }

            return File.ReadAllText(pathToFile); // если он существует - прочти весь текст в искомом файле и верни его в строковом формате
                                                 // тому методу, кто его попросит дальше 
        }

    }
}
