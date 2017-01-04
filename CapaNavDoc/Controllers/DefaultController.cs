using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Extensions.ViewModels;
using CapaNavDoc.Models.BusinessLayers;

namespace CapaNavDoc.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult Index<T, TD, TL>() where T : class where TD : new() where TL : new()
        {
            BusinessLayer<T> bl = new BusinessLayer<T>(new CapaNavDocDal());
            List<T> mDatas = bl.GetList();
            List<TD> mDataDetails = mDatas.Select(d => d.ToModel<TD>()).ToList();

            TL tl = new TL();
            typeof(TL).GetProperties()[0].SetValue(tl, mDataDetails);

            return View("Index", tl);
        }
    }
}