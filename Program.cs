using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using BankPlus.entity;
using BankPlus.model;
using BankPlus.view;

namespace BankPlus
{
    class Program
    {
        public static YYaccountnumber currentLoggedInYyAccount;

        static void Main(string[] args)
        {
         ApplicationView view = new ApplicationView();
            while (true)
            {
                if (Program.currentLoggedInYyAccount != null)
                {
                    view.GenerateCustomerMenu();
                }
                else
                {
                    view.GenerateDefaultMenu();
                }
            }
            
            // UserName();
            //AddProduct();
//            Console.OutputEncoding = System.Text.Encoding.UTF8;
//            Console.InputEncoding = System.Text.Encoding.UTF8;
//            YYaccountnumber acc =  new YYaccountnumber();
//            acc.Accountnumber = "123123123";
//            acc.Username = "phamhieu";

//            acc.Password = " 123";
//            acc.EncryptPassword();
//            acc.Balance = 2000;
//            acc.Address = "haNoi";
//            acc.IdentityCard = "3120310";
//            acc.Email = "phamhieu1999@gmail.com";
//            acc.PhoneNumber = "123123123424";
//            acc.Fullname = "phamvan hieu";
//            acc.Dob = "2008- 06-10 10:50:25";
//            acc.Gender = 1;
//            model.save(acc);
            // hieu();
//            YYaccountnumber yaccountnumber = new YYaccountnumber()
//            {
//                Username = "phamhieu1999",
//                Balance = -1000
//            };
//            var errors = ValidateAccount(yaccountnumber);
//            if (errors.Count == 0)
//            {
//                Console.WriteLine("luu vao db");
//            }
//            else
//            {
//                Console.WriteLine("vui long fix cac loi duoi day.");
//                foreach (var error in errors.Values)
//                {
//                    Console.WriteLine(error);
//                }
//            }
        }

//        private static void UserName()
//        {
//            Console.OutputEncoding = System.Text.Encoding.UTF8;
//            Console.InputEncoding = System.Text.Encoding.UTF8;
//            
//           
//           
//            Console.WriteLine("Nhập username: ");
//            string username = Console.ReadLine();
//            YYaccountnumber account = model.getUsername(username);
//            if (account == null)
//            {
//                Console.WriteLine("Tài khoản không tồn tại");
//                return;
//            }
//            Console.WriteLine(account.Accountnumber);
//            Console.WriteLine(account.Username);
//            Console.WriteLine(account.Password);
//            Console.WriteLine(account.Balance);
//            Console.WriteLine(account.IdentityCard);
//            Console.WriteLine(account.Fullname);
//            Console.WriteLine(account.Email);
//            Console.WriteLine(account.PhoneNumber);
//            Console.WriteLine(account.Address);
//            Console.WriteLine(account.Dob);
//            Console.WriteLine(account.Gender);
//            Console.WriteLine(account.CreatedAt);
//            Console.WriteLine(account.UpdateAt);
//            Console.WriteLine(account.Status);
//        }
//
//        private static void AddProduct()
//        {
//            var account = new YYaccountnumber();
//            Console.WriteLine("Please enter accountnumber:");
//            account.Accountnumber = Console.ReadLine();
//            Console.WriteLine("Please enter userName:");
//            account.Username = Console.ReadLine();
//            Console.WriteLine("Please enter password:");
//            account.Password = Console.ReadLine();
//            Console.WriteLine("Please enter identityCard:");
//            account.IdentityCard = Console.ReadLine();
//            Console.WriteLine("Please enter fullname:");
//            account.Fullname = Console.ReadLine();
//            Console.WriteLine("Please enter email:");
//            account.Email = Console.ReadLine();
//            Console.WriteLine("Please enter phoneNumber ");
//            account.PhoneNumber = Console.ReadLine();
//            model.save(account);
//        }


//        private static void hieu()
//        {
//            Console.WriteLine("Enter account infor.");
//            Console.WriteLine("Username:");
//            var username = Console.ReadLine();
//            Console.WriteLine("Password:");
//            var password = Console.ReadLine();
//            YYaccountnumber yaccountnumber = model.getUsername(username);
//            if (yaccountnumber == null)
//            {
//                Console.WriteLine("Invalid account infonation");
//            }
//
//            if (!yaccountnumber.CheckEncryptedPassword(password))
//            {
//                Console.WriteLine("Invalid account infonation");
//                return;
//            }
//
//            {
//            }
//        }
//
//        private static Dictionary<string, string> ValidateAccount(YYaccountnumber acc)
//        {
//            var errors = new Dictionary<string, string>();
//            if (string.IsNullOrEmpty(acc.Username))
//            {
//                errors.Add("username", "tai khoanr khong hop le");
//            }
//            else if (acc.Username.Length < 6)
//            {
//                errors.Add("username", "Tai khoan qua ngan, vui long nhap nhieu hon 6 ki tu.");
//            }
//
//            if (acc.Balance < 0)
//            {
//                errors.Add("balance", "so du tai khoan hop le.");
//            }
//
//            return errors;
//        }
    }
}