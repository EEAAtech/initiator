using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IncredibleInvoicer;

namespace IncredibleInvoicer.Controllers
{
    public class InvoicesController : Controller
    {
        private IncredibleInvoicerEntities db = new IncredibleInvoicerEntities();

        // GET: Invoices
        public ActionResult Index()
        {
            var invoices = db.Invoices.Include(i => i.Customer).Include(i => i.Tax);
            return View(invoices.ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }

            ViewBag.Inv = invoice;
            ViewBag.InvDet = db.InvoiceDetails.Where(ivd => ivd.InvoiceID == id).ToList();
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "Unit1");
            

            return View();
        }

        
        public ActionResult AutoComplete(string term)
        {
            var filteredItems = db.Items.Where(
            item => item.Item1.Contains(term)
            ).Select(i => i.Item1);

            //var items = new[] {"Apple", "Pear", "Banana", "Pineapple", "Peach"};
            //var filteredItems = items.Where(
            //item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
            //);
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteDet(int id, int invid)
        {
            InvoiceDetail invoice = db.InvoiceDetails.Find(id);
            db.InvoiceDetails.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = invid } );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDet([Bind(Include = "InvoiceDetailID,InvoiceID,Item,Qty,UnitID,Rate")] InvoiceDetailStr invoiceDetail)
        {
            if (ModelState.IsValid)
            {
                Item itm;

                //If this item doesnt already exists create it
                if (!db.Items.Any(i => i.Item1.Contains(invoiceDetail.Item)))
                {
                    itm = new Item { Item1 = invoiceDetail.Item };
                    db.Items.Add(itm);
                    db.SaveChanges();
                }
                else
                    itm = db.Items.FirstOrDefault(i => i.Item1.Contains(invoiceDetail.Item));

                InvoiceDetail invd = new InvoiceDetail { InvoiceID = invoiceDetail.InvoiceID, ItemID = itm.ItemID, Qty = invoiceDetail.Qty, UnitID = invoiceDetail.UnitID, Rate = invoiceDetail.Rate };
                db.InvoiceDetails.Add(invd);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = invd.InvoiceID });
            }
                        
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "Unit1", invoiceDetail.UnitID);
            return View(invoiceDetail);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name");
            ViewBag.TaxID = new SelectList(db.Taxes, "TaxID", "Tax1");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceID,InvDate,POno,POdate,CustomerID,TaxID")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = invoice.InvoiceID });
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", invoice.CustomerID);
            ViewBag.TaxID = new SelectList(db.Taxes, "TaxID", "TaxID", invoice.TaxID);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", invoice.CustomerID);
            ViewBag.TaxID = new SelectList(db.Taxes, "TaxID", "TaxID", invoice.TaxID);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceID,InvDate,POno,POdate,CustomerID,TaxID")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", invoice.CustomerID);
            ViewBag.TaxID = new SelectList(db.Taxes, "TaxID", "TaxID", invoice.TaxID);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
