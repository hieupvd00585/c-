using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using BankPlus.model;
using BankPlus.utility;

namespace BankPlus.entity
{
    public class YYaccountnumber
    {
        private string _accountnumber;
        private string _username;
        private string _password;
        private string _salt;
        private decimal _balance;
        private string _identityCard;
        private string _fullname;
        private string _cpassword;
        private string _phoneNumber;
        private string _address;
        private string _dob;
        private int _gender;// 1. 
        private string _createdAt;
        private string _updateAt;
        private int _status;
       
        private string email;

        public YYaccountnumber()
        {
            GenerteSale();
            GenerateAccountNumber();
        }

        public YYaccountnumber(string accountnumber, string username, string password, decimal balance, string identityCard, string fullname, string email, string phoneNumber, string address, int gender)
        {
            _accountnumber = accountnumber;
            _username = username;
            _password = password;
            _balance = balance;
            _identityCard = identityCard;
            _fullname = fullname;
            this.email = email;
            _phoneNumber = phoneNumber;
            _address = address;
            _gender = gender;
        }
        // Tham số là chuỗi password chưa mã hoá mà người dùng nhập vào.
        public bool CheckEncryptedPassword(string password) 
        {         
            // Tiến hành mã hoá password người dùng nhập vào kèm theo muối được lấy từ db.
            // Trả về một chuỗi password đã mã hoá.            
            var checkPassword = Hash.EncryptedString(password, _salt);
            // So sánh hai chuỗi password đã mã hoá. Nếu trùng nhau thì trả về true.
            // Nếu không trùng nhau thì trả về false.
            return (checkPassword == _password);
        }        
        public void EncryptPassword()
        {
            if (string.IsNullOrEmpty(_password))
            {
                throw new ArgumentNullException("Password is null or empyt.");
            }
            _password = Hash.EncryptedString(_password, _salt);
        }
        private void GenerateAccountNumber()
        {
            _accountnumber = Guid.NewGuid().ToString();// unique
        }

        private void GenerteSale()
        {
            _salt = Guid.NewGuid().ToString().Substring(0, 7);
        }

        public string Salt
        {
            get => _salt;
            set => _salt = value;
        }
        public string Cpassword
        {
            get => _cpassword;
            set => _cpassword = value;
        }
      
        public string Accountnumber
        {
            get => _accountnumber;
            set => _accountnumber = value;
        }

        public string Username
        {
            get => _username;
            set => _username = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public decimal Balance
        {
            get => _balance;
            set => _balance = value;
        }

        public string IdentityCard
        {
            get => _identityCard;
            set => _identityCard = value;
        }

        public string Fullname
        {
            get => _fullname;
            set => _fullname = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public string Address
        {
            get => _address;
            set => _address = value;
        }

        public string Dob
        {
            get => _dob;
            set => _dob = value;
        }

        public int Gender
        {
            get => _gender;
            set => _gender = value;
        }

        public string CreatedAt
        {
            get => _createdAt;
            set => _createdAt = value;
        }

        public string UpdateAt
        {
            get => _updateAt;
            set => _updateAt = value;
        }

        public int Status
        {
            get => _status;
            set => _status = value;
        }

        public Dictionary<string, string> CheckValidate()
        {
            AccountModel model = new AccountModel();
            Dictionary<string, string> errors = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(this._username))
            {
                errors.Add("username", "Username can not be null or empty.");
            } else if (this._username.Length < 6)
            {
                errors.Add("username", "Username is too short. At least 6 characters.");
            }else if (model.CheckExistUsername(this._username))
            {
                // Check trùng username.
                errors.Add("username", "Username is exist. Please try another one.");
            }
            if (_cpassword != _password)
            {
                errors.Add("password", "Confirm password does not match.");
            }

            // if else if else ...
            return errors;
        }
    }
}