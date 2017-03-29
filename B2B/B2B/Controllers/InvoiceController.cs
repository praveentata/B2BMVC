using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using B2B.Models;

namespace B2B.Controllers
{
    public class InvoiceController : Controller
    {
        //
        // GET: /Invoice/

        public ActionResult ApproveInvoice(int id) // taking id to approve PO and create invoice.
        {
            Random r = new Random(); // for invoice number
            List<supplier_inv> approveList = new List<supplier_inv>();
            using(MyEntities ME = new MyEntities())
            {
                int invoiceId = r.Next(0, 1000);

                var listForID = ME.supplier_inv.Where(x => x.InvoiceID == invoiceId).ToList(); // find if the generated invoice number in already present.

                while(listForID.Count != 0) // until not found, keep on generating.
                {
                    invoiceId = r.Next(0, 1000);
                    listForID = ME.supplier_inv.Where(x => x.InvoiceID == invoiceId).ToList();
                }

                approveList = ME.supplier_inv.Where(x => x.PO_ID == id).ToList();

                foreach(var aList in approveList) // once the invoice number is generated, then change the status to 'Approved'.
                {
                    aList.status = "Approved";
                    aList.InvoiceID = invoiceId;

                    ME.SaveChanges(); // save changes to DB.
                }
                approveList = ME.supplier_inv.Where(x => x.status == "Approved").ToList(); // get list of approved POs.
                ViewBag.InvoiceID = invoiceId;
            }




            return View(approveList); // return list to the view.
        }


    }
}
