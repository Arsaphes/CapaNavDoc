using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CapaNavDoc.Classes;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Extensions;
using CapaNavDoc.Models.BusinessLayers;

namespace CapaNavDoc.Controllers
{
    public class DefaultController<T> : Controller where T : class, new()
    {
        public ActionResult Index<TD, TL>() where TD : new() where TL : new()
        {
            BusinessLayer<T> bl = new BusinessLayer<T>(new CapaNavDocDal());
            List<T> mDatas = bl.GetList();
            List<TD> mDataDetails = mDatas.Select(d => (TD)d.ToModel(new TD())).ToList();

            TL tl = new TL();
            typeof(TL).GetProperties()[0].SetValue(tl, mDataDetails);

            // ReSharper disable once Mvc.ViewNotResolved
            return View("Index", tl);
        }

        public T Edit(object editionViewModel)
        {
            BusinessLayer<T> bl;

            switch (editionViewModel.GetType().GetProperty("EditionMode").GetValue(editionViewModel).ToString())
            {
                case EditionMode.Insert:
                    bl = new BusinessLayer<T>(new CapaNavDocDal());
                    return bl.Insert((T)editionViewModel.ToModel(new T()));

                case EditionMode.Update:
                    bl = new BusinessLayer<T>(new CapaNavDocDal());
                    object model = bl.Get(editionViewModel.GetType().GetProperty("Id").GetValue(editionViewModel).ToString().ToInt32());
                    return bl.Update((T)editionViewModel.ToModel(model));

                default:
                    return null;
            }
        }

        public void Delete(object confirmationViewModel)
        {
            BusinessLayer<T> bl = new BusinessLayer<T>(new CapaNavDocDal());
            bl.Delete(confirmationViewModel.GetType().GetProperty("Id").GetValue(confirmationViewModel).ToString().ToInt32());
        }
    }
}