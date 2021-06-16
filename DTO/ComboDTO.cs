using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyBanHang.DTO
{
    class ComboDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string deviceList { get; set; }
        public decimal totalMoney { get; set; }
        public int discount { get; set; }
        public decimal finalMoney { get; set; }
        public int amount { get; set; }
    }
}
