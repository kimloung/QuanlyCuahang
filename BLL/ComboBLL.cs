using QuanLyBanHang.DAL;
using QuanLyBanHang.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyBanHang.BLL
{
    class ComboBLL
    {
        ComboDAL comboDAL = new ComboDAL();
        StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase;
        public List<ComboDTO> getAll()
        {
            return comboDAL.getAll();
        }
        public string getDeviceListById(int id)
        {
            return comboDAL.getDeviceListById(id);
        }
        public ComboDTO getComboById(int id)
        {
            return comboDAL.getComboById(id);
        }
        public void addCombo(ComboDTO combo)
        {
            comboDAL.addCombo(combo);
        }
        public void updateCombo(int id, ComboDTO cb)
        {
            ComboDAL nva = new ComboDAL();
            nva.updateCombo(id, cb);
        }
        public void deleteCombo(int id)
        {
            ComboDAL nva = new ComboDAL();
            nva.deleteCombo(id);
        }
        public int getCurrentComboId()
        {
            return comboDAL.getCurrentComboId();
        }
        public List<ComboDTO> searchByName(string name)
        {
            List<ComboDTO> list = comboDAL.getAll();
            List<ComboDTO> kq = new List<ComboDTO>();

            if (name.Equals("")) return list;
            else
            {
                foreach (ComboDTO combo in list)
                {
                    if (combo.name.IndexOf(name, stringComparison) >= 0)
                    {
                        kq.Add(combo);
                    }
                }
                return kq;
            }
        }
    }
}
