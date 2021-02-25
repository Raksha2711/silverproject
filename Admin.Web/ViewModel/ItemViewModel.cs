using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Command.Entity1;

namespace Admin.Web.ViewModel
{
    public class ItemViewModel : Item
    {
        public string ItemGroupName { set; get; }
        //public List<ItemGroupData> Itemgroup { get; set; }
    }
    //public class ItemGroupData
    //{
    //    public int Id { get; set; }
    //    public string ItemGroupName { get; set; }
    //}
}
