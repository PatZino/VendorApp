using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDbSpCallMVC.Models;
using CoreDbSpCallMVC.Models.DB;
using Microsoft.AspNetCore.Mvc;


namespace CoreDbSpCallMVC.Controllers
{
    public class VendorController : Controller
    {
        private readonly db_core_sp_callContext databaseManager;

        public VendorController(db_core_sp_callContext databaseManagerContext)
        {
            databaseManager = databaseManagerContext;
        }

        #region Display all Vendors

        [Obsolete]
        public async Task<IActionResult> Index(int? page)
        {
            var allVendors = new List<SpVendors>();
			int pageSize = 5;
			try
            {

                // Settings.  
                allVendors = await this.databaseManager.GetVendors();
				var vend = allVendors.AsQueryable();
				ViewBag.NumberOfPages = repo.GetPages(vend, pageSize);
				vend = vend.OrderBy(q => q.Name);

			}
			catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }
			
			//return View(allVendors);
			return View(PaginatedList<SpVendors>.Create(allVendors, page ?? 1, pageSize));			
		}

        #endregion

        #region Add new Vendors

        [HttpGet]
        public IActionResult Create()
        {           
            return View();
        }

        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> Create(SpVendors spVendors, string Name)
        {
            var allVendors = new SpVendors();

            if (string.IsNullOrEmpty(spVendors.Name))
                ModelState.AddModelError(nameof(spVendors.Name), "Please enter the Vendor's name");

            if (ModelState.IsValid)
            {

                try
                {
                    if (ModelState.IsValid)
                    {
                        // Settings.  
                        allVendors = await this.databaseManager.AddVendors(Name);
                        ViewBag.Result = "Success";
                    }

                }
                catch (Exception ex)
                {
                    // Info  
                    Console.Write(ex);
                }
                //return View(allVendors);
                return RedirectToAction("Index");
                //return RedirectToAction("Index", "Vendor");
                //return RedirectToAction("Create", "Vendor", new { dis = ViewData["display"] });
            }
            else
            {
                //return View(allVendors);
				return RedirectToAction("Index");
			}
        }

        #endregion

        #region Update Vendors

        [HttpGet]
        public IActionResult Update(SpVendors vendors, string Name)
        {
            ViewData["Name"] = Name;
            return View(vendors);
        }

        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> Update(SpVendors spVendors, string Name, int id)
        {
			spVendors.VendorId = id;
			var allVendors = new SpVendors();
           if (string.IsNullOrEmpty(spVendors.Name))
                ModelState.AddModelError(nameof(spVendors.Name), "Please enter the Vendor's name");
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        // Settings.  
                        allVendors = await this.databaseManager.UpdateVendors(Name, id);

                        ViewBag.Result = "Success";
                    }

                }
                catch (Exception ex)
                {
                    // Info  
                    Console.Write(ex);
                }
				// return View(allVendors);
				return RedirectToAction("Index");
			}
            else
            {
				// return View(allVendors);
				return RedirectToAction("Index");
			}
        }

        #endregion

        #region Delete Vendors

        [HttpGet]
        public IActionResult Delete(SpVendors vendors, int Vendorid)
        {
            vendors.VendorId = Vendorid;
            return View(vendors);
        }

        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> Delete(int Vendorid)
        {
            var allVendors = new List<SpVendors>();

            try
            {

                // Settings.  
                allVendors = await this.databaseManager.DeleteVendor(Vendorid);

            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }
            return RedirectToAction("Index");
        }
        #endregion

    }
}