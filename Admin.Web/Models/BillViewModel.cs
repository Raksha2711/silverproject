﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Command.Entity1;

namespace Admin.Web.Models
{
    public class BillViewModel : Bill
    {
        public BillViewModel()
        {
            this.WarehouseDetail = new Warehouse();
        }

        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string SalesPersoName { get; set; }
        public Warehouse WarehouseDetail { get; set; }
        public List<BillItemViewModel> BillItemsView { get; set; }

    }
    public class BillItemViewModel : BillItem
    {
        public string ItemName { get; set; }
    }
    public class PurchaseRequestModel
    {
        public int PoId { get; set; }
        public string InvoceNo { get; set; }
        public DateTime? InvoceDate { get; set; }
        public DateTime? GoodRecordDate { get; set; }
    }
}
