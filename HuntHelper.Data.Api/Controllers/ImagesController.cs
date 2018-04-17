using HuntHelper.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace HuntHelper.Data.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ImageController : ApiController
    {

        //[Route("api/Image/{Name}")]
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAsync(string filename)
        {
            try
            {
                string file = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads"), filename);
                var ext = new FileInfo(file).Extension;
                ext = ext.Substring(1);
                var img = File.ReadAllBytes(file);
                var ms = new MemoryStream(img);

                var respons = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(ms)
                };

                respons.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue($"image/{ext}");
                return respons;
            }

            catch (Exception ex)
            {
                await Task.Run(() => ReportError.ErrorAsync(ex.Message));
            }
            return default(HttpResponseMessage);
        }



        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> PostAsync()
        {
            try
            {
                var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;

                if (file != null && file.ContentLength > 0)
                {
                    var filename = Path.GetFileName(file.FileName);

                    var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads"), filename);
                    //var path = Path.Combine(@"C:\\Users\\bjornma\\Pictures\\bilder", filename);

                    file.SaveAs(path);
                }
                return Ok();
            }

            catch (Exception ex)
            {
                await Task.Run(() => ReportError.ErrorAsync(ex.Message));
            }

            return Conflict();
        }

       
    }
}
