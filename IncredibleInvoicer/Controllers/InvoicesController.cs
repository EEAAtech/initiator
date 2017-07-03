using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IncredibleInvoicer;
using PagedList;

namespace IncredibleInvoicer.Controllers
{
    public class InvoicesController : EAController
    {
        private IncredibleInvoicerEntities db = new IncredibleInvoicerEntities();

        // GET: Invoices
        public ActionResult Index(int? page)
        {
            var invoices = db.Invoices.Include(i => i.Customers);

            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(invoices.OrderByDescending(c => c.InvoiceID).ToPagedList(pageNumber, pageSize));
            
        }

        // GET: Invoices report Request
        public ActionResult InvoiceRpt()
        {
            
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name");
            ViewBag.ReturnAction = "InvoicesReport";
            return View("RptRq");
        }

        public ActionResult InvoicesReport(FormCollection fm)
        {

            if (fm["FromDt"] ==null || fm["ToDt"] ==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DateTime Fd = DateTime.Parse(fm["FromDt"]);
            DateTime Td = DateTime.Parse(fm["ToDt"]);

            var invoices = db.Invoices.Where(i => i.InvDate > Fd && i.InvDate < Td && i.ReverseCharge==false);

            if (fm["CustomerID"] != "")
            {
                var cid = int.Parse(fm["CustomerID"]);
                invoices = invoices.Where(i => i.CustomerID == cid);
            }

            

            invoices = invoices.Include(i => i.InvoiceDetails);
            ViewBag.FromDt = Fd;
            ViewBag.ToDt = Td;
            return View(invoices.ToList());
        }

        // GET: Summary report Request
        public ActionResult SummaryRpt()
        {
            
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name");
            ViewBag.ReturnAction = "SummaryReport";
            return View("RptRq");
        }

        public ActionResult SummaryReport(FormCollection fm)
        {

            if (fm["FromDt"] == null || fm["ToDt"] == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DateTime Fd = DateTime.Parse(fm["FromDt"]);
            DateTime Td = DateTime.Parse(fm["ToDt"]);


            string str = "WITH cteInvTots (InvoiceID, Net, TaxAmt) AS " +
                " (SELECT InvoiceID, SUM(Qty * Rate) - SUM(COALESCE(Discount,0)) as Net, (SUM((Qty * Rate)* (t.Tax/100)) - SUM(COALESCE(Discount,0))) as TaxAmt FROM InvoiceDetails ids, Taxes t WHERE ids.TaxID=t.TaxID AND InvoiceID IN(SELECT InvoiceID FROM Invoices WHERE ReverseCharge=0 AND InvDate BETWEEN " + string.Format("'{0:yyyy-MM-dd}' AND '{1:yyyy-MM-dd}' ", Fd, Td);

            if (fm["CustomerID"] != "")
            {
                var cid = int.Parse(fm["CustomerID"]);
                str += " AND CustomerID = " + cid;
            }

            
            str += " ) GROUP BY InvoiceID) SELECT YEAR(i.InvDate) as yr, MONTH(i.InvDate) as mont, COUNT(*) as cont, SUM(c.Net) as net, SUM(c.TaxAmt) as ctax " +
                " FROM Invoices i, cteinvTots c WHERE i.InvoiceID = c.InvoiceID " +
                " GROUP BY YEAR(i.InvDate), MONTH(i.InvDate)";

            IEnumerable<SummaryRptData> todt = db.Database.SqlQuery<SummaryRptData>(str).ToList();

            ViewBag.FromDt = Fd;
            ViewBag.ToDt = Td;
            return View(todt.ToList());
        }



        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoices invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }

            ViewBag.Inv = invoice;
            ViewBag.InvDet = db.InvoiceDetails.Where(ivd => ivd.InvoiceID == id).Include(t => t.Taxes).ToList();
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "Unit1");
            ViewBag.TaxID = new SelectList(db.Taxes, "TaxID", "Tax");

            return View();
        }

        
        public ActionResult AutoComplete(string term)
        {
            var filteredItems = db.Items.Where(
            item => item.Item.Contains(term)
            ).Select(i => new { value = i.Item, HSNCode = i.HSNCode });

            //var items = new[] {"Apple", "Pear", "Banana", "Pineapple", "Peach"};
            //var filteredItems = items.Where(
            //item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
            //);
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoCompleteCusts(string term)
        {
            var filteredItems = db.Customers.Where(
            c => c.Name.Contains(term)
            ).Select(c => new { id = c.CustomerID, value = c.Name });

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
        public ActionResult CreateDet([Bind(Include = "InvoiceDetailID,InvoiceID,Item,Qty,UnitID,Rate, TaxID, Disc, HSNCode, Discount")] InvoiceDetailStr invoiceDetail)
        {

            if (ModelState.IsValid)
            {
                Items itm;

                //If this item doesnt already exists create it
                if (!db.Items.Any(i => i.Item.Contains(invoiceDetail.Item)))
                {
                    itm = new Items { Item = invoiceDetail.Item, HSNCode = invoiceDetail.HSNCode };
                    db.Items.Add(itm);
                }
                else
                {
                    itm = db.Items.FirstOrDefault(i => i.Item.Contains(invoiceDetail.Item));
                    itm.HSNCode = invoiceDetail.HSNCode;
                    db.Entry(itm).State = EntityState.Modified;                    
                }
                    db.SaveChanges();

                InvoiceDetail invd = new InvoiceDetail { InvoiceID = invoiceDetail.InvoiceID, ItemID = itm.ItemID, Qty = invoiceDetail.Qty, UnitID = invoiceDetail.UnitID, Rate = invoiceDetail.Rate, Disc=invoiceDetail.Disc,Discount= invoiceDetail.Discount,HSNCode= invoiceDetail.HSNCode,TaxID= invoiceDetail.TaxID };
                db.InvoiceDetails.Add(invd);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = invd.InvoiceID });
            }
                        
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "Unit1", invoiceDetail.UnitID);

         

            return View("Details", invoiceDetail);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name");
            ViewBag.BankID = new SelectList(db.Banks, "BankID", "BankName");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceID,InvDate,POno,POdate,CustomerID, BankID, ReverseCharge")] Invoices invoice)
        {
            if (ModelState.IsValid)
            {
                int lastInx=0;
                DateTime invyr = (DateTime)invoice.InvDate;
                if (db.InvYrs.Any(y=>y.Year==invyr.Year))
                {
                    lastInx = db.InvYrs.FirstOrDefault(y => y.Year == invyr.Year).InvLastNumber;
                    var existInvYr = db.InvYrs.Find(invyr.Year);
                    lastInx++;
                    existInvYr.InvLastNumber = lastInx;
                } else
                {
                    db.InvYrs.Add(new InvYr { Year = invyr.Year, InvLastNumber = 1 });
                    lastInx = 1;
                }

                invoice.FriendlyID = lastInx.ToString() + "-" + invyr.Year.ToString().Substring(2);
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = invoice.InvoiceID });
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", invoice.CustomerID);            
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoices invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", invoice.CustomerID);
            ViewBag.BankID = new SelectList(db.Banks, "BankID", "BankName", invoice.BankID);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceID,InvDate,POno,POdate,CustomerID,FriendlyID, BankID, ReverseCharge, RevChrg")] Invoices invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", invoice.CustomerID);            
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoices invoice = db.Invoices.Find(id);
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
            Invoices invoice = db.Invoices.Find(id);
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
