using QuanLyBanHang.DAL;
using QuanLyBanHang.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyBanHang.BLL
{
    class EmployeeBLL
    {
        public int CheckLogin(string mail, string pass)
        {
            EmployeeDAL nva = new EmployeeDAL();
            int kq = nva.login(mail, pass);
            if (kq == 1)
            {
                return 1; //login thanh cong
            }
            else return -1;
        }
        public List<EmployeeDTO> getAll()
        {
            EmployeeDAL nva = new EmployeeDAL();
            return nva.getAll();
        }
        public void addEmployee(EmployeeDTO nv)
        {
            EmployeeDAL nva = new EmployeeDAL();
            nva.addEmployee(nv);
        }

        public void updateEmployee(int manv, EmployeeDTO nv)
        {
            EmployeeDAL nva = new EmployeeDAL();
            nva.updateEmployee(manv, nv);
        }
        public void deleteEmployee(int manv)
        {
            EmployeeDAL nva = new EmployeeDAL();
            nva.deleteEmployee(manv);
        }
        public int getCurrentEmployeeId()
        {
            EmployeeDAL nva = new EmployeeDAL();
            return nva.getCurrentEmployeeId();
        }
    }
}
