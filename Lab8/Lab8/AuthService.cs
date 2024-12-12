
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab8
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message) { }
    }

    public class AuthService
    {
        Dictionary<string, string> _users = new();
        private readonly HashSet<string> _loggedUsers = new();
        public int GetRegisteredUsersCount() => _users.Count;
        public int GetLoggedUsersCount() => _loggedUsers.Count;
        public bool Register(string username, string password)
        {
            if (!CredentialsValidator.ValidateUsername(username))
            {
                Console.WriteLine("Invalid username");
                return false;
            }
            if (!CredentialsValidator.ValidatePassword(password))
            {
                Console.WriteLine("Invalid password");
                return false;
            }

            if (_users.ContainsKey(username))
            {
                Console.WriteLine("Username already exists");
                return false;
            }

            _users[username] = PasswordHasher.HashPassword(password);
            Console.WriteLine("User registered successfully");
            return true;
        }

        public bool Login(string username, string password)
        {
            if (!_users.ContainsKey(username))
            {
                return false;
            }
            if (_users[username] != password)
            {
                return false;
            }
            if (_loggedUsers.Contains(username))
            {
                return false; 
            }
            _loggedUsers.Add(username);
            return true;
        }


        public User GetUserData(string username)
        {
            if (!_users.ContainsKey(username))
            {
                throw new UserNotFoundException($"User with username '{username}' not found.");
            }

            string passwordHash = _users[username];


            return new User(username, passwordHash);
        }
    }

    public static class CredentialsValidator
    {
        public static bool ValidateUsername(string username)
        {
            string usernamePattern = @"^[a-zA-Z][a-zA-Z0-9_]{7,31}$";
            return Regex.IsMatch(username, usernamePattern);
        }
        public static bool ValidatePassword(string password)
        {
            string passwordPattern = @"^(?=.*[!@#$%^&*()_+\-=\[\]{}|\\,.<>?])(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,16}$";
            return Regex.IsMatch(password, passwordPattern);
        }
    }

    public class User
    {
        public User(string username, string passwordHash)
        {
            Username = username;
            PasswordHash = passwordHash;
        }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }

    public static class PasswordHasher
    {
        const int keySize = 64;
        const int iterations = 350000;
        static byte[] salt = RandomNumberGenerator.GetBytes(keySize);
        public static string HashPassword(string password)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            HashAlgorithmName.SHA512,
            keySize);
            return Convert.ToHexString(hash);
        }
    }

    }
