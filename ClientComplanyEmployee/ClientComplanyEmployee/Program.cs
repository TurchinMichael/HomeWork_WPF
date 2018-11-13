using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net.Http;

namespace ClientComplanyEmployee
{
    class Program
    {
        static void Main(string[] args)
        {
            int id = 1;
            string urlgetlist = $@"http://localhost:65323/getlist/{id}";
            string addEmployee = $@"http://localhost:65323/addEmployee";

            string obj = @"{
    '_fullName': {
                '_firstName': 'Михаил              ',
        '_lastName': 'Турчин              ',
        '_patronymic': 'Николаевич          '
    },
    '_employmentDate': '2018-08-11T00:00:00',
    '_gender': 0,
    '_dateOfBirth': '1993-08-21T00:00:00',
    '_position': {
                '_position': 0,
        '_salary': 125000
    },
    '_address': {
                '_country': 'Россия              ',
        '_region': 'Москва              ',
        '_city': 'Москва              ',
        '_street': 'Советская           ',
        '_streetNumber': 24,
        '_apartmentNumber': 26
    },
    '_phoneNumber': '8-963-777-39-97     ',
    '_status': 1
}
";
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            
            HttpClient client = new HttpClient();

            var resGet = client.GetAsync(urlgetlist).Result;

            Console.WriteLine(resGet);
            Console.ReadKey();
        }
    }
}
