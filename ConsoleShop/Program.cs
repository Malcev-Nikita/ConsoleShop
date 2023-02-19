using ConsoleShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleShop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Choose();
        }

        static void Choose()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("0: Выход 1: Клиенты 2: Продукты 3: Заказы");
                switch (Console.ReadKey().Key) 
                {
                    case ConsoleKey.D1:
                        Clients();
                        break;

                    case ConsoleKey.D2:
                        Products();
                        break;

                    case ConsoleKey.D3:
                        break;

                    default:
                        break;
                }
            } while (true);
        }

        static void Products()
        {
            Console.Clear();
            Console.WriteLine("0: Выход 1: Посмотреть ассортимент 2: Посмотреть по категориям");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    OutputProductOne();
                    break; 

                case ConsoleKey.D2:
                    OutputProductTwo();
                    break;

                default: 
                    break;
            }
        }

        static void Clients()
        {
            var res = Constant.context.Client;

            int page = 0;
            int pageSize = 3;
            double pageCount = Math.Ceiling(res.Count() / 3.0);
            int i = 1;

            do
            {
                Console.Clear();
                Console.WriteLine($"- - - - - - - - - - Клиенты - Страница №{page + 1}  - - - - - - - - - -");
                var itemPerPage = res.Skip(page * pageSize).Take(pageSize);

                foreach (var item in itemPerPage)
                {
                    Console.WriteLine($"{i}: {item.Name} {item.Surname}");
                    i++;
                }

                Console.WriteLine("\n<- Назад - - 0: Выход - - Вперёд ->");

                var key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow when page > 0:
                        i = 1;
                        page--;
                        break;

                    case ConsoleKey.RightArrow when page < pageCount - 1:
                        page++;
                        break;

                    case ConsoleKey.D0: goto end_switch;
                    default:
                        if (key == ConsoleKey.D1 || key == ConsoleKey.D2 || key == ConsoleKey.D3 || key == ConsoleKey.D4 ||
                            key == ConsoleKey.D5 || key == ConsoleKey.D6 || key == ConsoleKey.D7 || key == ConsoleKey.D8 ||
                            key == ConsoleKey.D9) OutputClient(int.Parse(key.ToString().Remove(0, 1)));

                        i -= itemPerPage.Count();
                        continue;
                }

            } while (true);

        end_switch:;
        }

        static void OutputClient(int id)
        {
            var res = Constant.context.Order.Where(x => x.ClientId == id).ToList();

            int page = 0;
            int pageSize = 3;
            double pageCount = Math.Ceiling(res.Count() / 3.0);
            int i = 1;

            do
            {
                Console.Clear();
                Console.WriteLine($"- - - - - - - - - - Заказы клента - Страница №{page + 1}  - - - - - - - - - -");
                var itemPerPage = res.Skip(page * pageSize).Take(pageSize);

                foreach (var item in itemPerPage)
                {
                    Console.WriteLine($"{item.Id}: {item.Cost}; {item.Status}");
                    i++;
                }

                Console.WriteLine("\n<- Назад - - 0: Выход - - Вперёд ->");

                var key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow when page > 0:
                        i = 1;
                        page--;
                        break;

                    case ConsoleKey.RightArrow when page < pageCount - 1:
                        page++;
                        break;

                    case ConsoleKey.D0: goto end_switch;

                    default:
                        i -= itemPerPage.Count();
                        continue;
                }

            } while (true);

            end_switch:;
        }

        static void OutputProductOne()
        {
            var res = Constant.context.Product;

            int page = 0;
            int pageSize = 3;
            double pageCount = Math.Ceiling(res.Count() / 3.0);
            int i = 1;

            do
            {
                Console.Clear();
                Console.WriteLine($"- - - - - - - - - - Продукты - Страница №{page + 1}  - - - - - - - - - -");
                var itemPerPage = res.Skip(page * pageSize).Take(pageSize);

                foreach (var item in itemPerPage)
                {
                    Console.WriteLine($"{i}: {item.Name}; {item.Category}");
                    i++;
                }

                Console.WriteLine("\n<- Назад - - 0: Выход - - Вперёд ->");

                var key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow when page > 0:
                        i = 1;
                        page--;
                        break;

                    case ConsoleKey.RightArrow when page < pageCount - 1:
                        page++;
                        break;

                    case ConsoleKey.D0: goto end_switch;
                    default:
                        if (key == ConsoleKey.D1 || key == ConsoleKey.D2 || key == ConsoleKey.D3 || key == ConsoleKey.D4 ||
                            key == ConsoleKey.D5 || key == ConsoleKey.D6 || key == ConsoleKey.D7 || key == ConsoleKey.D8 ||
                            key == ConsoleKey.D9) OutputOneProduct(int.Parse(key.ToString().Remove(0, 1)));

                        i -= itemPerPage.Count();
                        continue;
                }

            } while (true);

            end_switch:;
        }

        static void OutputOneProduct(int id)
        {
            var res = Constant.context.Product.FirstOrDefault(x => x.Id == id);

            Console.Clear();
            Console.WriteLine("0: Выход 1: Рудактировать 2: Удалить");

            Console.WriteLine($"\n{res.Id}: Наименование - {res.Name}; Описание - {res.Description}; Категория - {res.Category}; Стоимость - {res.Cost}; Количество - {res.Count}");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    break;

                case ConsoleKey.D2:
                    DeleteProduct(res);
                    break;

                default: break;
            }
        }

        static void DeleteProduct(Product res)
        {
            if (Constant.context.Order.FirstOrDefault(x => x.Id == res.Id) != null)
            {
                Console.WriteLine("\nПродукт нельзя удалить т.к. он есть в заказах");
                Console.WriteLine("\nНажмите любую кнопку для продолжения");
                Console.ReadKey();
            }
            else
            {
                Constant.context.Remove(res);
                Constant.context.SaveChanges();
            }
        }

        static void OutputProductTwo()
        {
            var res = Constant.context.Product.GroupBy(x => x.Category).Select(x => x.Key).ToList();

            int page = 0;
            int pageSize = 3;
            double pageCount = Math.Ceiling(res.Count() / 3.0);
            int i = 1;

            do
            {
                Console.Clear();
                Console.WriteLine($"- - - - - - - - - - Продукты - Страница №{page + 1} из {pageCount}  - - - - - - - - - -");
                var itemPerPage = res.Skip(page * pageSize).Take(pageSize);

                foreach (var item in itemPerPage)
                {
                    Console.WriteLine($"{i}: {item}");
                    i++;
                }

                Console.WriteLine("\n<- Назад - - 0: Выход - - Вперёд ->");

                var key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow when page > 0:
                        i = 1;
                        page--;
                        break;

                    case ConsoleKey.RightArrow when page < pageCount - 1:
                        page++;
                        break;

                    case ConsoleKey.D0: goto end_switch;

                    default:
                        if (key == ConsoleKey.D1 || key == ConsoleKey.D2 ||
                            key == ConsoleKey.D3 || key == ConsoleKey.D4 ||
                            key == ConsoleKey.D5 || key == ConsoleKey.D6 ||
                            key == ConsoleKey.D7 || key == ConsoleKey.D8 ||
                            key == ConsoleKey.D9) OutputProductPerCategory(res[int.Parse(key.ToString().Remove(0, 1)) - 1]);


                        i -= itemPerPage.Count();
                        continue;

                }

            } while (true);

            end_switch:;
        }

        static void OutputProductPerCategory(string categoty)
        {
            var res = Constant.context.Product.Where(x => x.Category == categoty);

            int page = 0;
            int pageSize = 3;
            double pageCount = Math.Ceiling(res.Count() / 3.0);
            int i = 1;

            do
            {
                Console.Clear();
                Console.WriteLine($"- - - - - - - - - - Продукты - Страница №{page + 1} из {pageCount}  - - - - - - - - - -");
                var itemPerPage = res.Skip(page * pageSize).Take(pageSize);

                foreach (var item in res)
                {
                    Console.WriteLine($"{i}: {item.Name}; {item.Category}");
                    i++;
                }

                Console.WriteLine("\n<- Назад - - 0: Выход - - Вперёд ->");

                var key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow when page > 0:
                        i = 1;
                        page--;
                        break;

                    case ConsoleKey.RightArrow when page < pageCount - 1:
                        page++;
                        break;

                    case ConsoleKey.D0: goto end_switch;
                    default:
                        i -= itemPerPage.Count();
                        continue;
                }
            } while (true);

            end_switch:;
        }
    }
}
