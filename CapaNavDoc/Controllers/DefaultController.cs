using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Extensions;
using CapaNavDoc.Extensions.ViewModels;
using CapaNavDoc.Models.BusinessLayers;

namespace CapaNavDoc.Controllers
{
    public class DefaultController<T> : Controller where T : class, new()
    {
        public ActionResult Index<TD, TL>() where TD : new() where TL : new()
        {
            BusinessLayer<T> bl = new BusinessLayer<T>(new CapaNavDocDal());
            List<T> mDatas = bl.GetList();
            List<TD> mDataDetails = mDatas.Select(d => d.ToModel<TD>()).ToList();

            TL tl = new TL();
            typeof(TL).GetProperties()[0].SetValue(tl, mDataDetails);

            // ReSharper disable once Mvc.ViewNotResolved
            return View("Index", tl);
        }

        public void Edit(object editionViewModel) 
        {
            BusinessLayer<T> bl;

            switch (editionViewModel.GetType().GetProperty("EditionMode").GetValue(editionViewModel).ToString())
            {
                case "Ajouter":
                    bl = new BusinessLayer<T>(new CapaNavDocDal());
                    bl.Insert(editionViewModel.ToModel<T>());
                    break;

                case "Changer":
                    bl = new BusinessLayer<T>(new CapaNavDocDal());
                    bl.Update(editionViewModel.ToModel<T>());
                    break;
            }
        }

        public void Delete(object confirmationViewModel)
        {
            BusinessLayer<T> bl = new BusinessLayer<T>(new CapaNavDocDal());
            bl.Delete(confirmationViewModel.GetType().GetProperty("Id").GetValue(confirmationViewModel).ToString().ToInt32());
        }
    }
}