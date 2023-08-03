using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using ApiModel.Models.User;
using System.Text.Json;

namespace celilcavus.RandomUserAspCore.Controllers;

public class HomeController : Controller
{
    private static HttpClient client = new HttpClient();
   

    // API isteği için kullanılacak URL'yi belirleyin
    private const string API_URL = "https://random-data-api.com/api/v2/users";
    public async Task<IActionResult> Index()
    {
        try
        {
            // HttpClient oluşturun
            using (HttpClient httpClient = new HttpClient())
            {
                // API'den veriyi alın
                HttpResponseMessage response = await httpClient.GetAsync(API_URL);

                // İsteğin başarılı olup olmadığını kontrol edin
                if (response.IsSuccessStatusCode)
                {
                    // API'den dönen veriyi alın
                    var apiResponse = response.Content.ReadFromJsonAsync<Root>().Result;
                    return View(apiResponse);
                }
                else
                {
                    // İsteğin başarısız olduğu durumda hata mesajını alın
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("API isteği başarısız oldu. Hata: " + errorMessage);
                }
            }
        }
        catch (Exception ex)
        {
            // Hata durumunda istisna bilgisini alın
            Console.WriteLine("Hata oluştu: " + ex.Message);
        }
        return View();
    }
}
