using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode
{
    public class CustomerBuilder
    {
        string username;
        string password;
        string firstName;
        string lastName;
        string email;
        string phone;
        string address;
        int age;
        int funds;
        List<Order> orders;

        public CustomerBuilder()
        {
            username = string.Empty;
            password = string.Empty;
            firstName = string.Empty;
            lastName = string.Empty;
            email = string.Empty;
            phone = string.Empty;
            address = string.Empty;
            age = 0;
            funds = 0;

            orders = new List<Order>();
        }

        public CustomerBuilder SetUsername(string username)
        {
            this.username = username;
            return this;
        }
        public CustomerBuilder SetPassword(string password) { this.password = password; return this; }
        public CustomerBuilder SetFirstName(string firstName) { this.firstName = firstName; return this; }
        public CustomerBuilder SetLastName(string lastName) { this.lastName = lastName; return this; }
        public CustomerBuilder SetEmail(string email) { this.email = email; return this; }
        public CustomerBuilder SetPhone(string phone) { this.phone = phone; return this; }
        public CustomerBuilder SetAddress(string address) { this.address = address; return this; }
        public CustomerBuilder SetAge(int age) { this.age = age; return this; }
        public CustomerBuilder Funds(int funds) { this.funds = funds; return this; }
        public CustomerBuilder Order(List<Order> orders) { this.orders = orders; return this; }

        public Customer Build()
        {
            return new Customer(username, password, firstName, lastName, email, age, address, phone, funds, orders);
        }
    }
}
