using System;
using System.Transactions;
using BankPlus.entity;
using MySql.Data.MySqlClient;

namespace BankPlus.model
{
    public class AccountModel
    {
        public Boolean Save (YYaccountnumber account)
        {
            DbConnection.Instance().OpenConnection();
            var sqlQuery =
                "insert into accounts (accountnumber,username,password,balance,identityCard,fullname,email,phoneNumber,address,dob,gender,createdAt,updateAt,status) " +
                "values (@accountnumber,@username,@password,@balance,@identityCard,@fullname,@email,@phoneNumber,@address,@dob,@gender,@createdAt,@updateAt,@status)";
            var cmd = new MySqlCommand(sqlQuery, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@accountnumber", account.Accountnumber);
            cmd.Parameters.AddWithValue("@username", account.Username);
            cmd.Parameters.AddWithValue("@password", account.Password);
            cmd.Parameters.AddWithValue("@balance", account.Balance);
            cmd.Parameters.AddWithValue("@identityCard", account.IdentityCard);
            cmd.Parameters.AddWithValue("@fullname", account.Fullname);
            cmd.Parameters.AddWithValue("@email", account.Email);
            cmd.Parameters.AddWithValue("@phoneNumber", account.PhoneNumber);
            cmd.Parameters.AddWithValue("@address", account.Address);
            cmd.Parameters.AddWithValue("@dob", account.Dob);
            cmd.Parameters.AddWithValue("@gender", account.Gender);
            cmd.Parameters.AddWithValue("@createdAt", account.CreatedAt);
            cmd.Parameters.AddWithValue("@updateAt", account.UpdateAt);
            cmd.Parameters.AddWithValue("@status", account.Status);
            cmd.Parameters.AddWithValue("@salt", account.Salt);
            cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
            return true;
        }
        public Boolean CheckExistUsername(string username)
        {
            DbConnection.Instance().OpenConnection();
            var queryString = "select * from `accounts` where username = @username";
            var cmd = new MySqlCommand(queryString, DbConnection.Instance().Connection);            
            cmd.Parameters.AddWithValue("@username", username);            
            var reader = cmd.ExecuteReader();
            var isExist = reader.Read();
            DbConnection.Instance().CloseConnection();
            return isExist;            
        }

        public YYaccountnumber getUsername(string username)
        {
            DbConnection.Instance().OpenConnection();
            var queryString = "select * from accounts where username = @username";
            var cmd = new MySqlCommand(queryString, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@username", username);
            var reader = cmd.ExecuteReader();
            YYaccountnumber account = null;
            if (reader.Read())
            {
                string accountnumber = reader.GetString("accountnumber");
                string username1 = reader.GetString("username");
                string password = reader.GetString("password");
                decimal balance = reader.GetDecimal("balance");
                string identityCard = reader.GetString("identityCard");
                string fullName = reader.GetString("fullName");
                string email = reader.GetString("email");
                string phoneNumber = reader.GetString("phoneNumber");
                string address = reader.GetString("address");
                int gender = reader.GetInt32("gender");
                account = new YYaccountnumber(accountnumber, username1, password, balance, identityCard, fullName,
                    email, phoneNumber, address, gender);
            }

            DbConnection.Instance().CloseConnection();
            return account;
        }

        public YYaccountnumber GetByAccountNumber(string accountNumber)
        {
            YYaccountnumber account = null;
            DbConnection.Instance().OpenConnection();
            var queryString = "select * from `accounts` where username = @accountnumber and status = 1";
            var cmd = new MySqlCommand(queryString, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@accountnumber", accountNumber);
            var reader = cmd.ExecuteReader();
            var isExist = reader.Read();
            if (isExist)
            {
                account = new YYaccountnumber()
                {
                    Accountnumber = reader.GetString("accountNumber"),
                    Username = reader.GetString("username"),
                    Password = reader.GetString("password"),
                    Salt = reader.GetString("salt"),
                    Fullname = reader.GetString("fullName"),
                    Balance = reader.GetInt32("balance")
                };
            }

            DbConnection.Instance().CloseConnection();
            return account;
        }
        

        public bool TransferAmount(YYaccountnumber account, YYtransaction historyTransaction)
        {
            DbConnection.Instance().OpenConnection();
            var transaction = DbConnection.Instance().Connection.BeginTransaction();

            try
            {
                // Kiểm tra số tài khoản mới nhất
                var queryBalance = "select `balance` from `account` where username = @username and status = 1";
                MySqlCommand queryBalanceCommand = new MySqlCommand(queryBalance, DbConnection.Instance().Connection);
                queryBalanceCommand.Parameters.AddWithValue("@username", account.Username);
                var balanceReader = queryBalanceCommand.ExecuteReader();
                // Không tìm thấy tài khoản tương ứng, throw lỗi.
                if (!balanceReader.Read())
                {
                    throw new TransactionException ("Invalid username");
                }

                var currentBalance = balanceReader.GetDecimal("balance");
                currentBalance -= historyTransaction.Amount;
                balanceReader.Close();

                // Update số dư vào database.
                var updateAccountResult = 0;
                var queryUpdateAccountBalance =
                    "update `accounts` set balance = @balance where username = @username and status = 1";
                var cmdUpdateAccountBalance =
                    new MySqlCommand(queryUpdateAccountBalance, DbConnection.Instance().Connection);
                cmdUpdateAccountBalance.Parameters.AddWithValue("@username", account.Username);
                cmdUpdateAccountBalance.Parameters.AddWithValue("@balance", currentBalance);
                updateAccountResult = cmdUpdateAccountBalance.ExecuteNonQuery();

                // Lưu thông tin transaction vào bảng transaction.
                var insertTransactionResult = 0;
                var queryInsertTransaction = "insert into `transaction` " +
                                             "(id, fromAccountNumber, amount, content, toAccountNumber, type, status) " +
                                             "values (@id, @fromAccountNumber, @amount, @content, @toAccountNumber, @type, @status)";
                var cmdInsertTransaction =
                    new MySqlCommand(queryInsertTransaction, DbConnection.Instance().Connection);
                cmdInsertTransaction.Parameters.AddWithValue("@id", historyTransaction.Id);
                cmdInsertTransaction.Parameters.AddWithValue("@fromAccountNumber",
                    historyTransaction.SenderAccountnumber);
                cmdInsertTransaction.Parameters.AddWithValue("@amount", historyTransaction.Amount);
                cmdInsertTransaction.Parameters.AddWithValue("@content", historyTransaction.Content);
                cmdInsertTransaction.Parameters.AddWithValue("@toAccountNumber",
                    historyTransaction.ReceiverAccountnumber);
                cmdInsertTransaction.Parameters.AddWithValue("@type", historyTransaction.Type);
                cmdInsertTransaction.Parameters.AddWithValue("@status", historyTransaction.Status);
                insertTransactionResult = cmdInsertTransaction.ExecuteNonQuery();

                // Kiểm tra lại câu lệnh
                if (updateAccountResult == 1 && insertTransactionResult == 1)
                {
                    transaction.Commit();
                    return true;
                }
            }
            catch (TransactionException e)
            {
                transaction.Rollback();
                return false;
            }

            DbConnection.Instance().CloseConnection();
            return false;
        }
    }
}