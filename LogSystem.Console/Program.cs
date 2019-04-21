using LogSystem.Models;
using StorageSystem.Services.Abstract;
using System;

namespace LogSystem.Console
{
    public class Program
    {
        private static Service _service;

        private const int CERTIFICATE_NUMBER_SIZE = 9;

        public static void Main(string[] args)
        {
            string userChoice;
            bool isParsed;
            int choice;

            System.Console.WriteLine("Выберите действие:");
            System.Console.WriteLine("1) Добавить информацию о новом посетителе");
            System.Console.WriteLine("2) Изменить информацию о посетителе");
            System.Console.WriteLine("3) Удалить информацию о посетителе");
            System.Console.WriteLine("4) Вывести всех посетителей");

            userChoice = System.Console.ReadLine();
            isParsed = int.TryParse(userChoice, out choice);

            while (!isParsed)
            {
                System.Console.WriteLine("Введите ИМЕННО НОМЕР ДЕЙСТВИЯ!");
                userChoice = System.Console.ReadLine();
                isParsed = int.TryParse(userChoice, out choice);
            }

            switch (choice)
            {
                case 1:
                    Insert();
                    break;
                case 2:
                    SelectAll();
                    Update();
                    break;
                case 3:
                    SelectAll();
                    Delete();
                    break;
                case 4:
                    SelectAll();
                    break;
                default:
                    System.Console.WriteLine("Такой опции не существует!");
                    break;
            }

            System.Console.ReadLine();
        }

        private static void Delete()
        {
            string userChoice;
            int choice;
            bool isParsed;

            _service = new Service();
            var visitors = _service.Select<Visitor>();
            _service.Dispose();

            if (visitors.Count == 0)
            {
                System.Console.WriteLine("Удалить информацию о посетителе не удастся!");
                return;
            }

            System.Console.WriteLine("Введите номер посетителя:");
            userChoice = System.Console.ReadLine();
            isParsed = int.TryParse(userChoice, out choice);

            while (!isParsed || choice > visitors.Count || choice <= 0)
            {
                System.Console.WriteLine("Введите ИМЕННО НОМЕР СУЩЕСТВУЮЩЕГО ПОСЕТИТЕЛЯ:");
                userChoice = System.Console.ReadLine();
                isParsed = int.TryParse(userChoice, out choice);
            }

            var visitor = visitors[choice - 1];

            _service = new Service();
            _service.Delete(visitor);
            _service.Dispose();

            System.Console.WriteLine("Информация о посетителе успешно удалена!");
        }

        private static void Update()
        {
            string userChoice;
            int choice;
            bool isParsed;

            _service = new Service();
            var visitors = _service.Select<Visitor>();
            _service.Dispose();

            if (visitors.Count == 0)
            {
                System.Console.WriteLine("Обновить информацию о посетителе не удастся!");
                return;
            }

            System.Console.WriteLine("Введите номер посетителя:");
            userChoice = System.Console.ReadLine();
            isParsed = int.TryParse(userChoice, out choice);

            while (!isParsed || choice > visitors.Count || choice <= 0)
            {
                System.Console.WriteLine("Введите ИМЕННО НОМЕР СУЩЕСТВУЮЩЕГО ПОСЕТИТЕЛЯ:");
                userChoice = System.Console.ReadLine();
                isParsed = int.TryParse(userChoice, out choice);
            }

            var visitor = visitors[choice - 1];

            System.Console.WriteLine("Что хотите изменить?");
            System.Console.WriteLine("1) Имя посетителя");
            System.Console.WriteLine("2) Номер удостоверения посетителя");
            System.Console.WriteLine("3) Время входа");
            System.Console.WriteLine("4) Время выхода");
            System.Console.WriteLine("5) Цель посещения");

            userChoice = System.Console.ReadLine();
            isParsed = int.TryParse(userChoice, out choice);

            while (!isParsed)
            {
                System.Console.WriteLine("Введите ИМЕННО НОМЕР ХАРАКТЕРИСТИКИ, которая должна быть ИЗМЕНЕНА:");
                userChoice = System.Console.ReadLine();
                isParsed = int.TryParse(userChoice, out choice);
            }

            string changedData;
            long certificateNumber;
            DateTime dateTime;

            switch (choice)
            {
                case 1:
                    System.Console.WriteLine("Введите новое имя:");
                    changedData = System.Console.ReadLine();
                    visitor.Fullname = changedData;
                    break;
                case 2:
                    {
                        System.Console.WriteLine("Введите новый номер удостоверения: Например: 123456789");
                        changedData = System.Console.ReadLine();

                        isParsed = long.TryParse(changedData, out certificateNumber);

                        while (!isParsed || certificateNumber.ToString().Length != CERTIFICATE_NUMBER_SIZE)
                        {
                            System.Console.WriteLine("Неверный ввод! Введите еще раз: Например: 123456789");
                            changedData = System.Console.ReadLine();
                            isParsed = long.TryParse(changedData, out certificateNumber);
                        }

                        visitor.CertificateNumber = certificateNumber;
                    }
                    break;
                case 3:
                    {
                        System.Console.WriteLine("Введите новое время входа: Например: 2009-05-08 14:40:52");
                        changedData = System.Console.ReadLine();

                        isParsed = DateTime.TryParse(changedData, out dateTime);

                        while (!isParsed)
                        {
                            System.Console.WriteLine("Неверный ввод! Введите еще раз: Например: 2009-05-08 14:40:52");
                            changedData = System.Console.ReadLine();
                            isParsed = DateTime.TryParse(changedData, out dateTime);
                        }

                        visitor.EnterTime = dateTime;
                    }
                    break;
                case 4:
                    {
                        System.Console.WriteLine("Введите новое время выхода: Например: 2009-05-08 14:40:52");
                        changedData = System.Console.ReadLine();

                        isParsed = DateTime.TryParse(changedData, out dateTime);

                        while (!isParsed)
                        {
                            System.Console.WriteLine("Неверный ввод! Введите еще раз: Например: 2009-05-08 14:40:52");
                            changedData = System.Console.ReadLine();
                            isParsed = DateTime.TryParse(changedData, out dateTime);
                        }

                        visitor.QuitTime = dateTime;
                    }
                    break;
                case 5:
                    System.Console.WriteLine("Введите новую цель посещения:");
                    changedData = System.Console.ReadLine();
                    visitor.EnterPurpose = changedData;
                    break;
                default:
                    System.Console.WriteLine("Такой опции не существует!");
                    return;
            }

            _service = new Service();
            _service.Update(visitor);
            _service.Dispose();

            System.Console.WriteLine("Информация о посетителе успешно обновлена!");
        }

        public static void SelectAll()
        {
            System.Console.WriteLine("Вывод посетителей:");

            _service = new Service();
            var visitors = _service.Select<Visitor>();
            _service.Dispose();

            if (visitors.Count == 0)
            {
                System.Console.WriteLine("Информации о посетителях нет!");
                return;
            }

            int i = 0;

            System.Console.WriteLine(string.Format("{0}{1,-20} {2,-20} {3,-20} {4,-20} {5,-50}", "№)", "Имя", "Номер удостоверения", "Время входа", "Время выхода", "Цель посещения"));
            foreach (var visitor in visitors)
            {
                System.Console.WriteLine(string.Format("{0}){1,-20} {2,-20} {3,-20} {4,-20} {5,-50}", ++i, visitor.Fullname, visitor.CertificateNumber, visitor.EnterTime, visitor.QuitTime, visitor.EnterPurpose));
            }
        }

        public static void Insert()
        {
            string visitorInfo;
            long certificateNumber;
            DateTime dateTime;
            bool isParsed;

            var visitor = new Visitor();

            System.Console.WriteLine("Введите имя посетителя:");
            visitorInfo = System.Console.ReadLine();
            visitor.Fullname = visitorInfo;

            System.Console.WriteLine("Введите время входа: Например: 2009-05-08 14:40:52");
            visitorInfo = System.Console.ReadLine();

            isParsed = DateTime.TryParse(visitorInfo, out dateTime);

            while (!isParsed)
            {
                System.Console.WriteLine("Неверный ввод! Введите еще раз: Например: 2009-05-08 14:40:52");
                visitorInfo = System.Console.ReadLine();
                isParsed = DateTime.TryParse(visitorInfo, out dateTime);
            }

            visitor.EnterTime = dateTime;

            System.Console.WriteLine("Введите время выхода: Например: 2009-05-08 14:40:52");
            visitorInfo = System.Console.ReadLine();

            isParsed = DateTime.TryParse(visitorInfo, out dateTime);

            while (!isParsed)
            {
                System.Console.WriteLine("Неверный ввод! Введите еще раз: Например: 2009-05-08 14:40:52");
                visitorInfo = System.Console.ReadLine();
                isParsed = DateTime.TryParse(visitorInfo, out dateTime);
            }

            visitor.QuitTime = dateTime;

            System.Console.WriteLine("Введите номер удостоверения посетителя: Например: 123456789");
            visitorInfo = System.Console.ReadLine();

            isParsed = long.TryParse(visitorInfo, out certificateNumber);

            while (!isParsed || certificateNumber.ToString().Length != CERTIFICATE_NUMBER_SIZE)
            {
                System.Console.WriteLine("Неверный ввод! Введите еще раз: Например: 123456789");
                visitorInfo = System.Console.ReadLine();
                isParsed = long.TryParse(visitorInfo, out certificateNumber);
            }

            visitor.CertificateNumber = certificateNumber;

            System.Console.WriteLine("Введите цель посещения:");
            visitorInfo = System.Console.ReadLine();
            visitor.EnterPurpose = visitorInfo;

            _service = new Service();
            _service.Insert(visitor);
            _service.Dispose();

            System.Console.WriteLine("Информация о посетителе успешно добавлена!");
        }


    }
}
