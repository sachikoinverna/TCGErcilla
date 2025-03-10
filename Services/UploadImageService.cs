using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.Services
{
    class UploadImageService
    {
        public static async Task<string> UploadImageAsync(string filePath,string id, string url)
        {
            try
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);

                using (HttpClient client = new HttpClient())
                using (var content = new MultipartFormDataContent())
                {
                    // Recuperar el token y usuario desde SecureStorage
                    string token = await SecureStorage.GetAsync("auth_token") ?? "";
                    string user = await SecureStorage.GetAsync("user") ?? "";

                    // Si el token está disponible, agregarlo a los encabezados del cliente HTTP
                    if (!string.IsNullOrEmpty(token))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        client.DefaultRequestHeaders.Add("User", user);
                    }

                    var fileContent = new ByteArrayContent(fileBytes);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    content.Add(fileContent, "file",id + Path.GetExtension(filePath));

                    // Enviar la imagen al servidor de Spring Boot
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Imagen subida correctamente.");
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        Debug.WriteLine($"Error al subir la imagen. Código de estado: {response.StatusCode}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

    }
}
