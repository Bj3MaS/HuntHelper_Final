using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Windows.Storage;

namespace HuntHelper.Model
{
    /// <summary>
    /// Generic APIcall class
    /// </summary>
    public static class ApiCall
    {
        /// <summary>
        /// The URI
        /// </summary>
        public static string uri = "http://localhost:61604/api/";
        /// <summary>
        /// The client
        /// </summary>
        static HttpClient client = new HttpClient();



        /// <summary>
        /// Posts objects the specified path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static async Task<T> Post<T>(string path, Object obj)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(uri + path, obj);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();

                }

            }
            catch (Exception ex)
            {
                await Task.Run(() => ReportError.ErrorAsync(ex.Message));
            }
            return default(T);
        }


        /// <summary>
        /// Gets objects from the specified path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static async Task<T> Get<T>(string path)
        {


            try
            {
                HttpResponseMessage response = await client.GetAsync(uri + path);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }

            }

            catch (Exception ex)
            {

                await Task.Run(() => ReportError.ErrorAsync(ex.Message));

            }
            return default(T);
        }

        /// <summary>
        /// Updates a generic object the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="huntedAnimal">The hunted animal.</param>
        /// <returns></returns>
        public static async Task<bool> Update(string path, HuntedAnimal huntedAnimal)
        {

            HttpResponseMessage response = await client.PutAsJsonAsync(uri + path + huntedAnimal.HuntedAnimalId.ToString(), huntedAnimal);

            try
            {

                if (response.IsSuccessStatusCode)
                {
                    return response.IsSuccessStatusCode;

                }

            }

            catch (Exception ex)
            {
                await Task.Run(() => ReportError.ErrorAsync(ex.Message));
            }
            return false;
        }

        /// <summary>
        /// Deletes  a generic the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static async Task<HttpStatusCode> Delete(string path, int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(uri + path + id.ToString());
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    return response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                await Task.Run(() => ReportError.ErrorAsync(ex.Message));
            }

            return response.StatusCode;
        }


    }
}

