using Newtonsoft.Json;
using School.Conso.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace School.Conso.Infrastructures
{
    public class StudentService
    {
        private HttpClient _client;

        private string _base_uri;

        public StudentService(string base_uri, string login = null, string password = null)
        {
            NetworkCredential credential;
            HttpClientHandler handler = null;
            if (!(login is null && password is null))
            {
                credential = new NetworkCredential(login, password);
                handler = new HttpClientHandler { Credentials = credential };
            }
            _client = (handler is null) ? new HttpClient() : new HttpClient(handler);
            _base_uri = base_uri;
            _client.BaseAddress = new Uri(_base_uri);
        }

        public IEnumerable<Student> Get()
        {
            HttpResponseMessage response = _client.GetAsync("Student/").Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return response.Content.ReadAsAsync<IEnumerable<Student>>().Result;
        }

        public Student Get(int id)
        {
            HttpResponseMessage response = _client.GetAsync($"Student/{id}").Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la réception de données.");
            }
            return response.Content.ReadAsAsync<Student>().Result;
        }

        public int Post(Student bodyContent)
        {
            string jsonContent = JsonConvert.SerializeObject(bodyContent, Formatting.Indented);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync("Student/", content).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Echec de l'envois de données.");
            }
            return (int)response.Content.ReadAsAsync(typeof(int)).Result;
        }

        public void Put(int id, Student bodyContent)
        {
            string jsonContent = JsonConvert.SerializeObject(bodyContent);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync($"Student/{id}", content).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la mise à jour des données.");
            }
            return;
        }

        public void Delete(int id)
        {
            HttpResponseMessage response = _client.DeleteAsync($"Student/{id}").Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Echec de la suppression des données.");
            }
            return;
        }
    }
}
