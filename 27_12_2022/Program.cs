using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace _27_12_2022_2_
{
    [Serializable]
    class Question
    {
        public string Text_of_question { get; private set; }
        public List<string> Answers { get; private set; } = new List<string>();
        public List<int> Right_answers { get; private set; } = new List<int>();
        public List<int> Checked_answers { get; private set; } = new List<int>();
        public bool Question_solved { get; private set; } = false;
        public bool Solved_right { get; private set; } = false;
        public Question()
        {
            Console.WriteLine(" Введите текст вопроса:");
            Text_of_question = Console.ReadLine();
            for (int i = 0; ; i++)
            {
                Console.WriteLine($"Введите {i + 1} ответ (Enter для завершения):");
                string bufer = Console.ReadLine();
                if (bufer != "") Answers.Add(bufer);
                else break;
            }
            bool right_answers_checked = false;
            int selected_answer = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Отметьте правильные ответы с помощью Space, затем нажмите Enter для завершения:\n");
                Console.WriteLine(Text_of_question);
                Console.WriteLine();
                for (int i = 0; i < Answers.Count; i++)
                {
                    if (Right_answers.Contains(i)) Console.BackgroundColor = ConsoleColor.DarkGreen;
                    if (selected_answer == i) Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(Answers[i]);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected_answer > 0) selected_answer--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected_answer < Answers.Count - 1) selected_answer++;
                        break;
                    case ConsoleKey.Spacebar:
                        {
                            if (Right_answers.Contains(selected_answer)) Right_answers.Remove(selected_answer);
                            else Right_answers.Add(selected_answer);
                            Right_answers.Sort();
                        }
                        break;
                    case ConsoleKey.Enter:
                        right_answers_checked = true;
                        break;
                }
            } while (!right_answers_checked);
        }
        public Question(string text_of_question, List<string> answers, List<int> right_answers)
        {
            Text_of_question = text_of_question;
            Answers = answers;
            Right_answers = right_answers;
        }
        public void Edit_question()
        {
            bool editing_complete = false;
            int selected_answer = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Выберите пункт для редактирования с помощью Tab, удалите вариант ответа с помощью Backspace, добавьте ответ с помощью + или отметьте правильные ответы с помощью Space, затем нажмите Enter для завершения:\n");
                if (selected_answer == -1) Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(Text_of_question);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
                for (int i = 0; i < Answers.Count; i++)
                {
                    if (Right_answers.Contains(i)) Console.BackgroundColor = ConsoleColor.DarkGreen;
                    if (selected_answer == i) Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(Answers[i]);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected_answer > -1) selected_answer--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected_answer < Answers.Count - 1) selected_answer++;
                        break;
                    case ConsoleKey.Spacebar:
                        {
                            if (selected_answer > -1)
                            {
                                if (Right_answers.Contains(selected_answer)) Right_answers.Remove(selected_answer);
                                else Right_answers.Add(selected_answer);
                                Right_answers.Sort();
                            }
                        }
                        break;
                    case ConsoleKey.Tab:
                        {
                            Console.WriteLine("Введите новый текст: ");
                            string bufer = Console.ReadLine();
                            if (selected_answer == -1) Text_of_question = bufer;
                            else Answers[selected_answer] = bufer;
                        }
                        break;
                    case ConsoleKey.Backspace:
                        {
                            if (selected_answer > -1)
                            {
                                Answers.RemoveAt(selected_answer);
                                Right_answers.Remove(selected_answer);
                                Right_answers.Sort();
                            }
                        }
                        break;
                    case ConsoleKey.OemPlus:
                        {
                            Console.WriteLine("Введите дополнительный ответ: ");
                            Answers.Add(Console.ReadLine());
                        }
                        break;
                    case ConsoleKey.Add:
                        {
                            Console.WriteLine("Введите дополнительный ответ: ");
                            Answers.Add(Console.ReadLine());
                        }
                        break;
                    case ConsoleKey.Enter:
                        editing_complete = true;
                        break;
                }
            } while (!editing_complete);
        }
        public void Solve_question()
        {
            Solved_right = false;
            Checked_answers.Clear();
            bool right_answers_checked = false;
            int selected_answer = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Отметьте правильные ответы с помощью Space, затем нажмите Enter для завершения:\n");
                Console.WriteLine(Text_of_question);
                Console.WriteLine();
                for (int i = 0; i < Answers.Count; i++)
                {
                    if (Checked_answers.Contains(i)) Console.BackgroundColor = ConsoleColor.DarkGreen;
                    if (selected_answer == i) Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(Answers[i]);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected_answer > 0) selected_answer--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected_answer < Answers.Count - 1) selected_answer++;
                        break;
                    case ConsoleKey.Spacebar:
                        {
                            if (Checked_answers.Contains(selected_answer)) Checked_answers.Remove(selected_answer);
                            else Checked_answers.Add(selected_answer);
                            Checked_answers.Sort();
                        }
                        break;
                    case ConsoleKey.Enter:
                        right_answers_checked = true;
                        break;
                }
            } while (!right_answers_checked);
            Question_solved = true;
            Solved_right = true;
            foreach(var answer in Right_answers)
            {
                if(!Checked_answers.Contains(answer))
                {
                    Solved_right = false;
                    break;
                }
            }
            if(Solved_right)
            {
                foreach(var answer in Checked_answers)
                {
                    if(!Right_answers.Contains(answer))
                    {
                        Solved_right = false;
                        break;
                    }
                }
            }
        }
        public void Print_question()
        {
            Console.WriteLine(Text_of_question);
            Console.WriteLine();
            if (Question_solved)
            {
                for (int i = 0; i < Answers.Count; i++)
                {
                    if (Checked_answers.Contains(i) && Right_answers.Contains(i)) Console.BackgroundColor = ConsoleColor.DarkGreen;
                    else if (Checked_answers.Contains(i)) Console.BackgroundColor = ConsoleColor.DarkRed;
                    else if (Right_answers.Contains(i)) Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(Answers[i]);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
            else
            {
                for (int i = 0; i < Answers.Count; i++)
                {
                    Console.WriteLine(Answers[i]);
                }
            }
        }
    }

    [Serializable]
    class Test
    {
        public int Id { get; private set; }
        public static int Last_id { get; private set; } = 0;
        public string Name { get; private set; }
        public string Theme { get; private set; }
        public List<Question> Questions { get; private set; } = new List<Question>();
        public int Right_solved_questions { get; private set; }
        public bool Test_solved { get; private set; } = false;
        public Test()
        {
            Console.Clear();
            Id = ++Last_id;
            Console.WriteLine("Введите тему теста:");
            Theme = Console.ReadLine();
            Console.WriteLine("Введите название теста:");
            Name = Console.ReadLine();
            int number_of_questions = 0;
            bool questions_created = false;
            do
            {
                Console.Clear();
                Console.Write(++number_of_questions);
                Questions.Add(new Question());
                Console.Clear();
                ConsoleKeyInfo key;
                bool yes_no = true;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Продолжить?");
                    Console.WriteLine();
                    if (!questions_created) Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Да");
                    Console.BackgroundColor = ConsoleColor.Black;
                    if (questions_created) Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нет");
                    Console.BackgroundColor = ConsoleColor.Black;
                    key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            questions_created = !questions_created;
                            break;
                        case ConsoleKey.DownArrow:
                            questions_created = !questions_created;
                            break;
                        case ConsoleKey.Enter:
                            yes_no = false;
                            break;
                    }
                } while (yes_no);
            } while (!questions_created);
            Console.WriteLine("Тест создан");
            Console.ReadKey();
        }
        public Test(string name, List<string> themes, List<Question> questions)
        {
            Id = ++Last_id;
            Name = name;
            for(int i = 0; i < themes.Count; i++)
            {
                Theme += themes[i];
                if (i < themes.Count - 1) Theme += ", ";
            }
            Questions = questions;
        }
        public Test(int id, string name, string theme, List<Question> questions)
        {
            Id = id;
            Name = name;
            Theme = theme;
            Questions = questions;
        }
        public void Set_id() { Id = ++Last_id; }
        public void Edit_test()
        {
            bool editing_complete = false;
            int selected_question = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Выберите вопрос для редактирования с помощью Enter, удалите вопрос с помощью Backspace или добавьте ответ с помощью +, затем нажмите Escape для завершения:\n");
                if (selected_question == -2) Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(Theme);
                Console.BackgroundColor = ConsoleColor.Black;
                if (selected_question == -1) Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(Name);
                Console.BackgroundColor = ConsoleColor.Black;
                for (int i = 0; i < Questions.Count; i++)
                {
                    if (selected_question == i) Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(Questions[i].Text_of_question);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected_question > -2) selected_question--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected_question < Questions.Count - 1) selected_question++;
                        break;
                    case ConsoleKey.Enter:
                        if (selected_question > -1) Questions[selected_question].Edit_question();
                        else
                        {
                            Console.WriteLine("Введите новый текст: ");
                            string bufer = Console.ReadLine();
                            if (selected_question == -2) Theme = bufer;
                            else Name = bufer;
                        }
                        break;
                    case ConsoleKey.Backspace:
                        if (selected_question > -1) Questions.RemoveAt(selected_question);
                        break;
                    case ConsoleKey.OemPlus:
                        {
                            Console.Clear();
                            Questions.Add(new Question());
                        }
                        break;
                    case ConsoleKey.Add:
                        {
                            Console.Clear();
                            Questions.Add(new Question());
                        }
                        break;
                    case ConsoleKey.Escape:
                        editing_complete = true;
                        break;
                }
            } while (!editing_complete);
        }
        public void Solve_test()
        {
            Right_solved_questions = 0;
            Test_solved = false;
            foreach (var question in Questions)
            {
                question.Solve_question();
                if (question.Solved_right) Right_solved_questions++;
            }
            Test_solved = true;
            bool want_see_answers = true;
            bool yes_no = true;
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();
                Console.WriteLine($"Ваш результат: {Right_solved_questions} из {Questions.Count}");
                Console.WriteLine("Хотите посмотреть ответы?");
                Console.WriteLine();
                if (want_see_answers) Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Да");
                Console.BackgroundColor = ConsoleColor.Black;
                if (!want_see_answers) Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Нет");
                Console.BackgroundColor = ConsoleColor.Black;
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        want_see_answers = !want_see_answers;
                        break;
                    case ConsoleKey.DownArrow:
                        want_see_answers = !want_see_answers;
                        break;
                    case ConsoleKey.Enter:
                        yes_no = false;
                        break;
                }
            } while (yes_no);
            if (want_see_answers)
            {
                Console.Clear();
                Print_test();
            }
        }
        public void Print_test()
        {
            foreach (var question in Questions)
            {
                question.Print_question();
                Console.WriteLine();
            }
        }
    }

    delegate void Data_func(ref Data data);

    [Serializable]
    class Account
    {
        public string Login { get; private set; }
        public string Password { get; private set; }
        public DateTime Birthday { get; private set; }
        public List<Test> Tests { get; private set; }
        bool admin = false;
        public Account(ref Data data)
        {
            do
            {
                bool login_exists = false;
                Console.Clear();
                Console.WriteLine("Регистрация нового пользователя");
                Console.WriteLine();
                Console.WriteLine("Введите логин:");
                string bufer = Console.ReadLine();
                for (int i = 0; i < data.Accounts.Count() - 1; i++)
                {
                    if (data.Accounts[i].Login == bufer)
                    {
                        login_exists = true;
                        Console.WriteLine("Логин занят");
                        Console.ReadKey();
                        break;
                    }
                }
                if (!login_exists) Login = bufer;
            } while (Login.Length == 0);
            Console.WriteLine("Введите пароль:");
            ConsoleKeyInfo key;
            string password = "";
            while (true)
            {
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter) break;
                else if (key.Key == ConsoleKey.Backspace)
                {
                    Console.Write(" \b \b");
                    if (password.Length > 0) password = password.Remove(password.Length - 1);
                }
                else
                {
                    password += key.KeyChar;
                    Console.Write("\b*");
                }
            }
            Password = password;
            Console.WriteLine("Введите дату рождения:");
            Birthday = DateTime.Parse(Console.ReadLine());
            Tests = data.Tests;
            data.Accounts.Add(this);
            data.Save_accounts();
        }
        public Account(string login, string password, bool admin, ref Data data)
        {
            Login = login;
            Password = password;
            Birthday = new DateTime(2000, 1, 1);
            Tests = data.Tests;
            this.admin = admin;
            data.Save_accounts();
        }
        public bool Check_login_password(string login, string password)
        {
            if (Login == login && Password == password) return true;
            else return false;
        }
        public void Main_menu(ref Data data)
        {
            bool variant_selected = false;
            int selected_variant = 0;
            List<object[]> variants = new List<object[]>
            {
                new object[3] { selected_variant++, "Мой аккаунт", (Data_func)My_account_menu },
                new object[3] { selected_variant++, "Мои тесты", (Data_func)My_tests_menu }
            };
            if (admin) variants.Add(new object[3] { selected_variant++, "Администраторское меню тестов", (Data_func)Admin_tests_menu });
            selected_variant = 0;
            do
            {
                Console.Clear();
                for (int i = 0; i < variants.Count; i++)
                {
                    if (selected_variant == i) Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(variants[i][1]);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected_variant > 0) selected_variant--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected_variant < variants.Count - 1) selected_variant++;
                        break;
                    case ConsoleKey.Enter:
                        ((Data_func)variants[selected_variant][2])(ref data);
                        break;
                    case ConsoleKey.Escape:
                        variant_selected = true;
                        break;
                }
            } while (!variant_selected);
        }
        void My_account_menu(ref Data data)
        {
            bool variant_selected = false;
            int selected_variant = 0;
            List<object[]> variants = new List<object[]>
            {
                new object[3] { selected_variant++, "Сменить пароль", (Data_func)Change_password },
                new object[3] { selected_variant++, "Сменить дату рождения", (Data_func)Change_Birthday }
            };
            selected_variant = 0;
            do
            {
                Console.Clear();
                Console.WriteLine($"Логин: {Login}\nПароль: {Password}\nДата рождения: {Birthday}");
                for (int i = 0; i < variants.Count; i++)
                {
                    if (selected_variant == i) Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(variants[i][1]);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected_variant > 0) selected_variant--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected_variant < variants.Count - 1) selected_variant++;
                        break;
                    case ConsoleKey.Enter:
                        ((Data_func)variants[selected_variant][2])(ref data);
                        break;
                    case ConsoleKey.Escape:
                        variant_selected = true;
                        break;
                }
            } while (!variant_selected);
        }
        void Change_password(ref Data data)
        {
            Console.WriteLine("Введите новый пароль:");
            ConsoleKeyInfo key;
            string password = "";
            while (true)
            {
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter) break;
                else if (key.Key == ConsoleKey.Backspace)
                {
                    Console.Write(" \b \b");
                    if (password.Length > 0) password = password.Remove(password.Length - 1);
                }
                else
                {
                    password += key.KeyChar;
                    Console.Write("\b*");
                }
            }
            Password = password;
            data.Save_accounts();
        }
        void Change_Birthday(ref Data data)
        {
            Console.WriteLine("Введите дату рождения:");
            Birthday = DateTime.Parse(Console.ReadLine());
            data.Save_accounts();
        }
        void My_tests_menu(ref Data data)
        {
            bool variant_selected = false;
            int selected_variant = 0;
            List<object[]> variants = new List<object[]>
            {
                new object[3] { selected_variant++, "Проверить обновления", (Data_func)Check_updates },
                new object[3] { selected_variant++, "Показать мои тесты", (Data_func)Show_my_tests }
            };
            selected_variant = 0;
            do
            {
                Console.Clear();
                for (int i = 0; i < variants.Count; i++)
                {
                    if (selected_variant == i) Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(variants[i][1]);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected_variant > 0) selected_variant--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected_variant < variants.Count - 1) selected_variant++;
                        break;
                    case ConsoleKey.Enter:
                        ((Data_func)variants[selected_variant][2])(ref data);
                        break;
                    case ConsoleKey.Escape:
                        variant_selected = true;
                        break;
                }
            } while (!variant_selected);
        }
        void Check_updates(ref Data data)
        {
            int count_of_new_tests = 0;
            for (int i = 0; i < data.Tests.Count; i++)
            {
                bool test_found = false;
                for (int j = 0; j < Tests.Count; j++)
                {
                    if (data.Tests[i].Id == Tests[j].Id)
                    {
                        test_found = true;
                        if (!Tests[j].Test_solved)
                            Tests[j] = new Test(data.Tests[i].Id, data.Tests[i].Name, data.Tests[i].Theme, data.Tests[i].Questions);
                        break;
                    }
                }
                if (!test_found)
                {
                    Tests.Add(data.Tests[i]);
                    count_of_new_tests++;
                }
            }
            Console.WriteLine($"Проверка завершена, добавлено {count_of_new_tests} тестов");
            if (count_of_new_tests > 0) data.Save_accounts();
        }
        void Show_my_tests(ref Data data)
        {
            bool exit_menu = false;
            int selected_test = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Выберите тест с помощью Enter или вернитесь назад с помощью Escape:\n");
                if (Tests.Count > 0)
                {
                    for (int i = 0; i < Tests.Count; i++)
                    {
                        if (selected_test == i) Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"{data.Tests[i].Name} на тему {data.Tests[i].Theme}");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    ConsoleKeyInfo key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (selected_test > 0) selected_test--;
                            break;
                        case ConsoleKey.DownArrow:
                            if (selected_test < Tests.Count - 1) selected_test++;
                            break;
                        case ConsoleKey.Enter:
                            Test_menu(ref data, Tests[selected_test]);
                            break;
                        case ConsoleKey.Escape:
                            exit_menu = true;
                            break;
                    }
                }
                else exit_menu = true;
            } while (!exit_menu);
        }
        void Test_menu(ref Data data, Test test)
        {
            bool exit_menu = false;
            bool selected_variant = true;
            do
            {
                Console.Clear();
                test.Print_test();
                Console.WriteLine();
                if (selected_variant) Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Показать топ-20 пользователей, решивших этот тест");
                Console.BackgroundColor = ConsoleColor.Black;
                if (!selected_variant) Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Решить тест");
                Console.BackgroundColor = ConsoleColor.Black;
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selected_variant = !selected_variant;
                        break;
                    case ConsoleKey.DownArrow:
                        selected_variant = !selected_variant;
                        break;
                    case ConsoleKey.Enter:
                        {
                            if (selected_variant)
                            {
                                Console.Clear();
                                Show_top(ref data, test.Id);
                            }
                            else if (test.Test_solved)
                            {
                                do
                                {
                                    Console.Clear();
                                    Console.WriteLine("Тест уже решён, вы точно хотите перерешать его?");
                                    if (selected_variant) Console.BackgroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Да");
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    if (!selected_variant) Console.BackgroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Нет");
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    key = Console.ReadKey();
                                    switch (key.Key)
                                    {
                                        case ConsoleKey.UpArrow:
                                            selected_variant = !selected_variant;
                                            break;
                                        case ConsoleKey.DownArrow:
                                            selected_variant = !selected_variant;
                                            break;
                                        case ConsoleKey.Enter:
                                            {
                                                if (selected_variant)
                                                {
                                                    test.Solve_test();
                                                    data.Save_accounts();
                                                    exit_menu = true;
                                                }
                                                else exit_menu = true;
                                            }
                                            break;
                                    }
                                } while (!exit_menu);
                                exit_menu = false;
                            }
                            else
                            {
                                test.Solve_test();
                                data.Save_accounts();
                            }
                        }
                        break;
                    case ConsoleKey.Escape:
                        exit_menu = true;
                        break;
                }
            } while (!exit_menu);
        }
        void Show_top(ref Data data, int id)
        {
            SortedDictionary<int, string> top = new SortedDictionary<int, string>();
            for (int i = 0; i < data.Accounts.Count; i++)
            {
                for (int j = 0; j < data.Accounts[i].Tests.Count; j++)
                {
                    if (data.Accounts[i].Tests[j].Id == id && data.Accounts[i].Tests[j].Test_solved)
                    {
                        top.Add(data.Accounts[i].Tests[j].Right_solved_questions, data.Accounts[i].Login);
                        break;
                    }
                }
            }
            top.Reverse();
            int counter = 0;
            foreach (var account in top)
            {
                Console.WriteLine($"{++counter}. {account.Value} - {account.Key}");
                if (counter == 20) break;
            }
            Console.ReadKey();
        }
        void Admin_tests_menu(ref Data data)
        {
            bool exit_menu = false;
            int selected_test = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Выберите тест для редактирования с помощью Enter, удалите тест с помощью Backspace, создайте тест из случайных вопросов с помощью Tab или добавьте тест с помощью +, затем нажмите Escape для завершения:\n");
                for (int i = 0; i < data.Tests.Count; i++)
                {
                    if (selected_test == i) Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{data.Tests[i].Name} на тему {data.Tests[i].Theme}");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected_test > 0) selected_test--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected_test < data.Tests.Count - 1) selected_test++;
                        break;
                    case ConsoleKey.Enter:
                        if (data.Tests.Count > 0)
                        {
                            data.Tests[selected_test].Edit_test();
                            data.Save_tests();
                        }
                        break;
                    case ConsoleKey.Backspace:
                        {
                            if (data.Tests.Count > 0) data.Tests.RemoveAt(selected_test);
                            data.Save_tests();
                        }
                        break;
                    case ConsoleKey.OemPlus:
                        {
                            data.Tests.Add(new Test());
                            data.Save_tests();
                        }
                        break;
                    case ConsoleKey.Add:
                        {
                            data.Tests.Add(new Test());
                            data.Save_tests();
                        }
                        break;
                    case ConsoleKey.Tab:
                        if (data.Tests.Count > 0) Add_random_test(ref data);
                        break;
                    case ConsoleKey.Escape:
                        exit_menu = true;
                        break;
                }
            } while (!exit_menu);
        }
        void Add_random_test(ref Data data)
        {
            Console.WriteLine("Из скольки вопросов создать тест?");
            int number_of_questions = Convert.ToInt32(Console.ReadLine());
            if (number_of_questions < 1) number_of_questions = 20;
            List<string> themes = new List<string>();
            List<Question> questions = new List<Question>();
            for (int i = 0; i < number_of_questions; i++)
            {
                Random rand = new Random();
                Thread.Sleep(1000);
                int number_of_test = rand.Next(data.Tests.Count);
                if (!themes.Contains(data.Tests[number_of_test].Theme)) themes.Add(data.Tests[number_of_test].Theme);
                int number_of_question = rand.Next(data.Tests[number_of_test].Questions.Count);
                questions.Add(new Question(data.Tests[number_of_test].Questions[number_of_question].Text_of_question,
                    data.Tests[number_of_test].Questions[number_of_question].Answers,
                    data.Tests[number_of_test].Questions[number_of_question].Right_answers));
            }
            foreach (var question in questions)
            {
                question.Print_question();
                Console.WriteLine();
            }
            Console.WriteLine("Введите название теста:");
            string name = Console.ReadLine();
            data.Tests.Add(new Test(name, themes, questions));
            data.Save_tests();
        }
    }

    class Data
    {
        public List<Account> Accounts { get; private set; } = new List<Account>();
        public List<Test> Tests { get; private set; } = new List<Test>();
        public Data()
        {
            Load_accounts();
            Load_tests();
        }
        public void Save_accounts()
        {
            DirectoryInfo directory = new DirectoryInfo(Directory.GetCurrentDirectory() + @"\Files\Accounts");
            if (!directory.Exists) directory.Create();
            FileStream stream = new FileStream(Directory.GetCurrentDirectory() + @"\Files\Accounts\Accounts.bin", FileMode.OpenOrCreate);
            BinaryFormatter binary = new BinaryFormatter();
            binary.Serialize(stream, Accounts);
            stream.Close();
        }
        void Load_accounts()
        {
            FileInfo file = new FileInfo(Directory.GetCurrentDirectory() + @"\Files\Accounts\Accounts.bin");
            if (file.Exists)
            {
                FileStream stream = new FileStream(Directory.GetCurrentDirectory() + @"\Files\Accounts\Accounts.bin", FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                Accounts = (List<Account>)binary.Deserialize(stream);
                stream.Close();
            }
        }
        public void Save_tests()
        {
            DirectoryInfo directory = new DirectoryInfo(Directory.GetCurrentDirectory() + @"\Files\Tests");
            if (!directory.Exists) directory.Create();
            FileStream stream = new FileStream(Directory.GetCurrentDirectory() + @"\Files\Tests\Tests.bin", FileMode.OpenOrCreate);
            BinaryFormatter binary = new BinaryFormatter();
            binary.Serialize(stream, Tests);
            stream.Close();
        }
        void Load_tests()
        {
            FileInfo file = new FileInfo(Directory.GetCurrentDirectory() + @"\Files\Tests\Tests.bin");
            if (file.Exists)
            {
                FileStream stream = new FileStream(Directory.GetCurrentDirectory() + @"\Files\Tests\Tests.bin", FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                Tests = (List<Test>)binary.Deserialize(stream);
                foreach(Test test in Tests)
                {
                    test.Set_id();
                }
                stream.Close();
            }
        }
    }

    internal class Program
    {
        static Account Authorization(List<Account> accounts)
        {
            int i;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Авторизация\n");
                Console.WriteLine("Введите логин: ");
                string login = Console.ReadLine();
                Console.WriteLine("Введите пароль: ");
                ConsoleKeyInfo key;
                string password = "";
                while (true)
                {
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Enter) break;
                    if (key.Key == ConsoleKey.Backspace)
                    {
                        Console.Write(" \b \b");
                        if (password.Length > 0) password = password.Remove(password.Length - 1);
                    }
                    else if (key.Key != ConsoleKey.Backspace)
                    {
                        password += key.KeyChar;
                        Console.Write("\b*");
                    }
                }
                bool success = false;
                i = 0;
                foreach (var account in accounts)
                {
                    if (account.Check_login_password(login, password))
                    {
                        success = true;
                        break;
                    }
                    i++;
                }
                if (success == true) break;
                else
                {
                    Console.WriteLine("Неправильный логин или пароль");
                    Console.ReadKey();
                }
            }
            Console.Clear();
            return accounts[i];
        }
        static void Main()
        {
            Data data = new Data();
            ConsoleKeyInfo key;
            if (data.Accounts.Count == 0)
            {
                Console.WriteLine("Создайте администраторскую учётную запись\n");
                Console.WriteLine("Введите логин: ");
                string login = Console.ReadLine();
                Console.WriteLine("Введите пароль: ");
                string password = "";
                while (true)
                {
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Enter) break;
                    password += key.KeyChar;
                    Console.Write("\b*");
                }
                data.Accounts.Add(new Account(login, password, true, ref data));
                data.Save_accounts();
                Console.WriteLine("Учётная запись создана. Для продолжения нажмите любую клавишу...");
                Console.ReadKey();
                Console.Clear();
            }
            bool authorization = true;
            bool yes_no = true;
            do
            {
                Console.Clear();
                if (authorization) Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Авторизоваться");
                Console.BackgroundColor = ConsoleColor.Black;
                if (!authorization) Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Зарегистрироваться");
                Console.BackgroundColor = ConsoleColor.Black;
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        authorization = !authorization;
                        break;
                    case ConsoleKey.DownArrow:
                        authorization = !authorization;
                        break;
                    case ConsoleKey.Enter:
                        yes_no = false;
                        break;
                }
            } while (yes_no);
            if (authorization) Authorization(data.Accounts).Main_menu(ref data);
            else
            {
                data.Accounts.Add(new Account(ref data));
                Authorization(data.Accounts).Main_menu(ref data);
            }
        }
    }
}