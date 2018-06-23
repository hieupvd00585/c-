using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using BankPlus.entity;
using BankPlus.model;
using BankPlus.utility;

namespace BankPlus.controller
{
    public class Controller
    {
        private static AccountModel model = new AccountModel();
        public bool Register()
        {
            YYaccountnumber  account = GetAccountInformation();
            Dictionary<string, string> errors =  account.CheckValidate();
            if (errors.Count > 0)
            {
                Console.WriteLine("Please fix errros below and try again.");
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }
                return false;
            }
            else
            {
                // Lưu vào database.
                account.EncryptPassword();
                model.Save(account);
                return true;
            }
        }
        public bool Login()
        {
            Console.WriteLine("----------------LOGIN INFORMATION----------------");
            Console.WriteLine("Username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Password: ");
            var password = Console.ReadLine();
            YYaccountnumber existingAccount = model.getUsername(username);
            if (existingAccount == null)
            {                
                return false;
            }

            if (!existingAccount.CheckEncryptedPassword(password))
            {                
                return false;
            }

            Program.currentLoggedInYyAccount = existingAccount;
            return true;    
        }
        private YYaccountnumber GetAccountInformation()
        {
            Console.WriteLine("----------------REGISTER INFORMATION----------------");
            Console.WriteLine("Username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Password: ");
            var password = Console.ReadLine();
            Console.WriteLine("Confirm Password: ");
            var cpassword = Console.ReadLine();
            Console.WriteLine("Balance: ");
            var balance = Utility.GetDecimalNumber();
            Console.WriteLine("Identity Card: ");
            var identityCard = Console.ReadLine();
            Console.WriteLine("Full Name: ");
            var fullName = Console.ReadLine();
            Console.WriteLine("Birthday: ");
            var birthday = Console.ReadLine();
            Console.WriteLine("Gender (1. Male |2. Female| 3.Others): ");
            var gender = Utility.GetInt32Number();
            Console.WriteLine("Email: ");
            var email = Console.ReadLine();
            Console.WriteLine("Phone Number: ");
            var phoneNumber = Console.ReadLine();
            Console.WriteLine("Address: ");
            var address = Console.ReadLine();
            var acc = new YYaccountnumber()
            {
                Username = username,
                Password = password,
                Cpassword = cpassword,
                IdentityCard = identityCard,
                Gender = gender,
                Balance = balance,
                Address = address,
                Dob = birthday,
                Fullname = fullName,
                Email = email,
                PhoneNumber = phoneNumber
            };
            return acc;
        }

        public void ShowAccountInformation()
        {
           var currentAccount = model.getUsername(Program.currentLoggedInYyAccount.Username);
            if (currentAccount  == null)
            {
                Program.currentLoggedInYyAccount = null;
                Console.WriteLine("Sai thông tin tài khoản phải bị khóa.");
                return;
            }
            Console.WriteLine("Số tài khoản :");
            Console.WriteLine(Program.currentLoggedInYyAccount.Accountnumber);
            Console.WriteLine("Số dư hiện tại (vnd):");
            Console.WriteLine(Program.currentLoggedInYyAccount.Balance);
        }
        // Tiến hành chuyển khoản , mặc định là trong ngân hàng.
        //1. Yêu cầu nhập số tài khoản và hiển thị tên người cần chuyển
        //1.1 Xác minh thông tin tài khoản và hiển thị tên người cần chuyển
        //2 Nhập số tiền cần chuyển
        // 2.1 kiểm tra số dư tài khoản 
        //3. Nhập nội dung chuyên tiền.
        //4. Thực hiện chuyển tiền.
        // 4.1 Mở transaction.
        // 4.2 Trừ tiền người gủi 
        /*4.2.1 Lấy thông tin tài khoản tiền mooitj làn nữa.Đảm bảo thông tin là mới nhất.
         * 4.2.2 Kiểm tra lại một lần nữa số dư xem có đủ tiền để chuyển không.
         * 
         */
        // 4.3 Lưu lịch sử giao dịch
        // 4.x .Đóng , comit transaction.

       
        public void Transfer()
        {
            Console.WriteLine("------------------Transerf Information-------------------");
            var accountNumber = "3bbfb97d-6296-45fd-8835-8b7815f5d6dc";
            var account = model.GetByAccountNumber(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Invalid account info ");
                return;
            }

            Console.WriteLine("You are doing transaction with account :" +account.Fullname);
            Console.WriteLine("enter amout to transfer");
            var amount = Utility.GetDecimalNumber();
            if (amount > account.Balance)
            
            {
                Console.WriteLine("Amout not enough to perfom transaction:");
                return;
            }

            amount += account.Balance;

            Console.WriteLine("Please enter message conten:");
            var content = Console.ReadLine();
            Console.WriteLine("Are you sure you want to make a transaction with your account ? (y/n)");
            var choice = Console.ReadLine();

            if (choice.Equals("n"))
            {
                return;
            }

            var historyTransaction = new YYtransaction()
            {
                Id = Guid.NewGuid().ToString(),
                Type = 4,
                Content = content,
                Amount = amount,
                SenderAccountnumber = Program.currentLoggedInYyAccount.Accountnumber,
                ReceiverAccountnumber= account.Accountnumber,
                Status = 2,
            };
            if (model.TransferAmount(Program.currentLoggedInYyAccount, historyTransaction))
                       {
                           Console.WriteLine("Transaction success!");
                       }
                       else
                       {
                           Console.WriteLine("Transaction fails, please try again!");
                       }
           
           
                       Program.currentLoggedInYyAccount = model.getUsername(Program.currentLoggedInYyAccount.Username);
                       Console.WriteLine("Current balance: " + Program.currentLoggedInYyAccount.Balance);
                       Console.WriteLine("Press enter to continue!");
                       Console.ReadLine();
        }

    }
}