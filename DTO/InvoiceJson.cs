using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyBanHang.DTO
{
    class InvoiceJson
    {
        public List<CartDv> ThietBi { get; set; }
        public List<CartCb> Combo { get; set; }
    }
}
