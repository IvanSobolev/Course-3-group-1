namespace mySolution;

class Program
{
    static void Main(string[] args)
    {
        var dbRepository = new DbRepository("Data Source=phone.db;Version=3;");
        bool isOpen = true;
        
        dbRepository.CreateTable();

        while (isOpen)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Добавить контакт");
            Console.WriteLine("2. Удалить контакт по id");
            Console.WriteLine("3. Поиск контакта по имени");
            Console.WriteLine("4. Поиск контакта по id");
            Console.WriteLine("5. Вывести всех контактов");
            Console.WriteLine("6. Выход");
            
            var choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    Console.Write("Введите имя: ");
                    var nameToAdd = Console.ReadLine();
                    Console.Write("Введите номер телефона: ");
                    var phoneToAdd = Console.ReadLine();
                    Contact contact = new Contact(0, nameToAdd, phoneToAdd);
                    dbRepository.AddContact(contact);
                    Console.WriteLine("Контакт добавлен.");
                    break;
                case "2":
                    Console.Write("Введите ID контакта для удаления: ");
                    if (int.TryParse(Console.ReadLine(), out var idToDelete))
                    {
                        dbRepository.DeleteContactById(idToDelete);
                        Console.WriteLine("Контакт удален.");
                    }
                    else
                    {
                        Console.WriteLine("Неверный ID.");
                    }
                    break;
                case "3":
                    Console.Write("Введите имя для поиска: ");
                    var nameToSearch = Console.ReadLine();
                    Contact find = dbRepository.GetContactByName(nameToSearch);
                    Console.WriteLine(find.ToString());
                    break;
                case "4":
                    Console.Write("Введите id для поиска: ");
                    if (int.TryParse(Console.ReadLine(), out  var idToSearch))
                    {
                        Contact find1 = dbRepository.GetContactById(idToSearch);
                        Console.WriteLine(find1.ToString());
                    }
                    else
                    {
                        Console.WriteLine("Неверный ID.");
                    }
                    break;
                case "5":
                    foreach (var c in dbRepository.GetAllContacts())
                    {
                        Console.WriteLine(c.ToString());
                    }
                    break;
                case "6":
                    isOpen = false;
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }
}