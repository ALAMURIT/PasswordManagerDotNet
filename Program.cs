using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace PasswordManagerDotNet
{
    class Program
    {
        Random random = new Random();
        static void Main(string[] args)
        {
            //Console.WriteLine($"Hello World!, this is password generator\npassword is {PasswordGenerator(8)}");
            //Console.WriteLine($"Hi!, this is password generator\nSite address: ");
            //string _userAddress = Console.ReadLine();
            //Console.WriteLine($"Hi!, this is password generator\nEnter user name: ");
            //string _userName = Console.ReadLine();
            //Console.WriteLine($"Hi!, this is password generator\nEnter strength: ");
            //int _strength = Convert.ToInt32(Console.ReadLine());
            //string _generatedPassword = PasswordGenerator(_strength);
            //Console.WriteLine($"Hi!, this is password generator\nYour password is: {_generatedPassword}\nDo you want to save? (Y/N)");
            //if (Console.ReadLine() == "Y")
            //{
            //    WriteToDb(_userAddress, _userName, _generatedPassword);
            //}
            //else
            //{
            //    Console.WriteLine("Insertion Discarded!");
            //}
            Console.WriteLine("Hi, this is PasswordGenerator\nSelect operation\nRegister|Retrive|Clear|Info: (reg|ret|clr|inf)");

            //string _prompt = "reg";
            switch (Console.ReadLine())
            {
                case "reg":
                    GenerateUser();
                    break;
                case "ret":
                    Console.WriteLine("Enter password: ");
                    string _retrivalPassword = Console.ReadLine();
                    if (AuthoriseUser(_retrivalPassword))
                    {
                        RetrieveCredentials();
                    }
                    else
                    {
                        Console.WriteLine("Invalid password!");
                    }
                    break;
                case "clr":
                    ClearTable();
                    break;
                case "inf":
                    Console.WriteLine("By @|@murit follow at https://alamurit.github.io/");

                    break;
                default:
                    Console.WriteLine("Done");
                    break;
            }
            //if (Console.ReadLine() == "reg")
            //{
            //    GenerateUser();
            //}
            //if (Console.ReadLine() == "ret")
            //{
            //    Console.WriteLine("Enter password: ");
            //    string _retrivalPassword = Console.ReadLine();
            //    if (AuthoriseUser(_retrivalPassword))
            //    {
            //        RetrieveCredentials();
            //    }
            //    else
            //    {
            //        Console.WriteLine("Invalid password!");
            //    }
            //}
            //if (Console.ReadLine() == "clr")
            //{
            //    ClearTable();
            //}
            //else
            //{
            //    Console.WriteLine("Invalid response!");
            //}
            EnterToClose();
        }
        static void EnterToClose()
        {
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }
        static void GenerateUser()
        {
            //Console.WriteLine($"Hello World!, this is password generator\npassword is {PasswordGenerator(8)}");
            Console.WriteLine($"Hi!, this is password generator\nSite address: ");
            string _userAddress = Console.ReadLine();
            Console.WriteLine($"Hi!, this is password generator\nEnter user name: ");
            string _userName = Console.ReadLine();
            Console.WriteLine($"Hi!, this is password generator\nEnter strength: ");
            int _strength = Convert.ToInt32(Console.ReadLine());
            string _generatedPassword = PasswordGenerator(_strength);
            Console.WriteLine($"Hi!, this is password generator\nYour password is: {_generatedPassword}\nDo you want to save? (Y/N)");
            if (Console.ReadLine() == "Y")
            {
                WriteToDb(_userAddress, _userName, _generatedPassword);
                Console.WriteLine("Preview? (Y/N)");
                if (Console.ReadLine() == "Y")
                {
                    RetrieveCredentials();
                }
            }
            else
            {
                Console.WriteLine("Insertion Discarded!");
            }
        }
        static void WriteToDb(string _url, string _userName, string _userPassword)
        {
            //SQLite connection string
            string _connectionString = "Data Source=PasswordDb.db;Mode=ReadWrite";
            //Initiate SQLite connection
            using(var _connection=new SqliteConnection(_connectionString))
            {
                _connection.Open();
                //Insert values into PasswordDb
                //PasswordTable[siteAddress|sitePassword|siteUserName]
                string _insertQuery = "INSERT INTO PasswordTable (siteAddress, sitePassword, siteUserName) VALUES (@siteAddress, @sitePassword, @siteUserName);";
                using(var _insertCmd = new SqliteCommand(_insertQuery, _connection))
                {
                    _insertCmd.Parameters.AddWithValue("@siteAddress", _url);
                    _insertCmd.Parameters.AddWithValue("@sitePassword", _userPassword);
                    _insertCmd.Parameters.AddWithValue("@siteUserName", _userName);
                    //execute insert
                    _insertCmd.ExecuteNonQuery();
                    //Debug
                    Console.WriteLine("insertion completed!");
                }
            }
        }
        static void RetrieveCredentials()
        {
            string _connectionString = "Data Source=PasswordDb.db;Mode=ReadOnly";
            using(var _connection=new SqliteConnection(_connectionString))
            {
                _connection.Open();
                string _selectQuery = "SELECT siteAddress, sitePassword, siteUserName FROM PasswordTable";
                using(var _selectCmd=new SqliteCommand(_selectQuery, _connection))
                {
                    using(var _reader = _selectCmd.ExecuteReader())
                    {
                        while (_reader.Read())
                        {
                            Console.WriteLine($"url {_reader.GetString(0)}, user {_reader.GetString(2)}, psw {_reader.GetString(1)}");
                        }
                    }
                }
            }
        }
        static bool AuthoriseUser(string _dbPassword)
        {
            string _connectionString = "Data Source=PasswordDb.db;Mode=ReadOnly";
            using (var _connection = new SqliteConnection(_connectionString))
            {
                _connection.Open();
                string _selectQuery = "SELECT siteAddress, sitePassword, siteUserName FROM PasswordTable LIMIT 1";
                using (var _selectCmd = new SqliteCommand(_selectQuery, _connection))
                {
                    using (var _reader = _selectCmd.ExecuteReader())
                    {
                        //Console.WriteLine($"url {_reader.GetString(0)}, user {_reader.GetString(2)}, psw {_reader.GetString(1)}");
                        while (_reader.Read())
                        {
                            string _pswd = _reader.GetString(1).ToString();
                            
                            if (_pswd == _dbPassword)
                            {
                                return (true);
                            }
                            else
                            {
                                //Console.WriteLine($"{_reader.GetString(1)}, {_pswd}");
                                return (false);
                            }
                            //Console.WriteLine($"url {_reader.GetString(0)}, user {_reader.GetString(2)}, psw {_reader.GetString(1)}");
                        }
                    }
                }
            }
            //using(var _connection = new SqliteConnection(_connectionString))
            //{
            //    _connection.Open();
            //    string _selectQuery = "SELECT siteAddress, sitePassword, siteUserName FROM PasswordTable LIMIT 1";
            //    using (var _selectCmd = new SqliteCommand(_selectQuery, _connection))
            //    {
            //        using (var _reader = _selectCmd.ExecuteReader())
            //        {
            //            while (_reader.Read())
            //            {
            //                Console.WriteLine($"url {_reader.GetString(0)}, user {_reader.GetString(2)}, psw {_reader.GetString(1)}");
            //            }
            //            //while (_reader.Read())
            //            //{
            //            //    Console.WriteLine($"url {_reader.GetString(0)}, user {_reader.GetString(2)}, psw {_reader.GetString(1)}");
            //            //}
                        //if (_reader.GetString(0) == _dbPassword)
                        //{
                        //    return (true);
                        //}
                        //else
                        //{
                        //    return (false);
                        //}
            //        }
            //    }
            //}
            return (true);
        }
        static void ClearTable()
        {
            //SQLite connection string
            string _connectionString = "Data Source=PasswordDb.db;Mode=ReadWrite";
            //Initiate SQLite connection
            using (var _connection = new SqliteConnection(_connectionString))
            {
                _connection.Open();
                //PasswordTable[siteAddress|sitePassword|siteUserName]
                string _deleteQuery = "DELETE FROM PasswordTable";
                using (var _deleteCmd = new SqliteCommand(_deleteQuery, _connection))
                {
                    // Execute the delete command
                    int rowsAffected = _deleteCmd.ExecuteNonQuery();

                    // Output the number of rows deleted
                    Console.WriteLine($"{rowsAffected} rows were deleted from the Events table.");
                }
            }
        }
        static string RandomSmall()
        {
            Random random = new Random();
            string[] smallLetters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            return (smallLetters[random.Next(smallLetters.Length)]);
        }
        static string RandomCapital()
        {
            Random random = new Random();
            string[] capitalLetters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            return (capitalLetters[random.Next(capitalLetters.Length)]);
        }
        static string RandomSpecial()
        {
            Random random = new Random();
            string[] specialCharacters = new string[] { "!", "@", "#", "$", "%", "^", "&", "*" };
            return (specialCharacters[random.Next(specialCharacters.Length)]);
        }
        static string RandomNumbers()
        {
            Random random = new Random();
            string[] numeralCharacters = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            return (numeralCharacters[random.Next(numeralCharacters.Length)]);
        }
        static string RandomCharacterGenerator(int randomKey)
        {
            switch (randomKey)
            {
                case 0:
                    return (RandomSmall());
                    //break;
                case 1:
                    return (RandomCapital());
                    //break;
                case 2:
                    return (RandomSpecial());
                    //break;
                default:
                    return (RandomNumbers());
                    //break;
            }
        }
        static string PasswordGenerator(int passwordStrength)
        {
            Random random = new Random();
            int i = 0;
            string password = "";
            while (i < passwordStrength)
            {
                password += RandomCharacterGenerator(random.Next(4));
                i++;
            }
            return (password);
        }
    }
}
