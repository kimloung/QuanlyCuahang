using QuanLyBanHang.BLL;
using QuanLyBanHang.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace QuanLyBanHang.GUI
{
    public partial class Combo : Form
    {
        public Combo()
        {
            InitializeComponent();
        }

        private void Combo_Load(object sender, EventArgs e)
        {
            ComboBLL comboBLL = new ComboBLL();
            List<ComboDTO> list = comboBLL.getAll();

            foreach (ComboDTO combo in list)
            { 
                tblCombo.Rows.Add(combo.id, combo.name, combo.amount, combo.finalMoney);
            }
        }

        /*private void tblCombo_DoubleClick(object sender, EventArgs e)
        {
            *//*ComboBLL comboBLL = new ComboBLL();
            List<DeviceDTO> listSP = new List<DeviceDTO>();
            try
            {
                int selectedrowindex = tblCombo.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = tblCombo.Rows[selectedrowindex];
                if (selectedrowindex >= 0 && (selectedRow.Cells[0].Value != null))
                {
                    // hien sp cua combo trong json, load tu db
                    string deviceList = comboBLL.getDeviceListById(Convert.ToInt32(selectedRow.Cells[0]));
                    listSP = JsonConvert.DeserializeObject<List<DeviceDTO>>(deviceList);
                    foreach(DeviceDTO sp in listSP)
                    {
                        tblSPinCombo.Rows.Add(sp.id, sp.name, sp.amount);
                    }
                }
                else return;
            }
            catch (Exception) { return; }     *//*     
        }*/

        private void tblCombo_SelectionChanged(object sender, EventArgs e)
        {
            ComboBLL comboBLL = new ComboBLL();
            ComboDTO cb = new ComboDTO();
            List<DeviceDTO> listSP = new List<DeviceDTO>();
            tblSPinCombo.Rows.Clear();
            foreach (DataGridViewRow row in tblCombo.SelectedRows)
            {
                tblSPinCombo.Rows.Clear();
                if (row.Cells[0].Value != null)
                { 
                    cb = comboBLL.getComboById(Convert.ToInt32(row.Cells[0].Value));
                    txtIdCombo.Text = row.Cells[0].Value.ToString();
                    txtname.Text = row.Cells[1].Value.ToString();
                    txtAmount.Text = row.Cells[2].Value.ToString();
                    txtFinalMoney.Text = row.Cells[3].Value.ToString();
                    txtDiscount.Text = cb.discount.ToString();
                    txtTotalMoney.Text = cb.totalMoney.ToString();

                    listSP = JsonConvert.DeserializeObject<List<DeviceDTO>>(cb.deviceList);

                    
                    foreach (DeviceDTO sp in listSP)
                    {
                        
                        tblSPinCombo.Rows.Add(sp.id, sp.name, sp.amount, sp.price);
                    }
                }
                
                else return;
            }
        }

        private void btnLoadProduct_Click(object sender, EventArgs e)
        {
            DeviceBLL deviceBLL = new DeviceBLL();
            List<DeviceDTO> list = deviceBLL.getAll();
            foreach(DeviceDTO sp in list)
            {
                tblProduct.Rows.Add(sp.id, sp.name, sp.price);
            }
        }

        private void tblProduct_DoubleClick(object sender, EventArgs e)
        {
            bool flag = false;
            if (tblProduct.SelectedCells.Count > 0)
            {
                int selectedrowindex = tblProduct.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = tblProduct.Rows[selectedrowindex];
                foreach (DataGridViewRow row in tblSPinCombo.Rows)
                {
                    if (row.Cells[0].Value == null)
                    {
                        break;
                    }
                    if (row.Cells[0].Value.Equals(selectedRow.Cells[0].Value))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                    tblSPinCombo.Rows.Add(selectedRow.Cells[0].Value, selectedRow.Cells[1].Value, 1, selectedRow.Cells[2].Value);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAmount.Text = "";
            txtDiscount.Text = "";
            txtFinalMoney.Text = "";
            txtname.Text = "";
            txtTotalMoney.Text = "";
            tblSPinCombo.Rows.Clear();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            bool flag = false;
            ComboBLL comboBLL = new ComboBLL();
            ComboDTO combo = new ComboDTO();

            List<DeviceDTO> list = new List<DeviceDTO>();
            
            DeviceBLL deviceBLL = new DeviceBLL();

            if (txtAmount.Text.Equals("") || txtTotalMoney.Text.Equals("") || txtDiscount.Text.Equals("") || txtFinalMoney.Text.Equals("") || txtAmount.Text.Equals(""))
            {
                MessageBox.Show("Thông tin không được để trống", "Thông báo");
            }
            else if (tblSPinCombo.Rows.Count < 1)
            {
                MessageBox.Show("Bảng sản phẩm trong Combo không được để trống", "Thông báo");
            }
            else flag = true;

            if (flag)
            {
                combo.name = txtname.Text;
                combo.totalMoney = Convert.ToDecimal(txtTotalMoney.Text);
                combo.discount = Convert.ToInt32(txtDiscount.Text);
                combo.finalMoney = Convert.ToDecimal(txtFinalMoney.Text);
                combo.amount = Convert.ToInt32(txtAmount.Text);

                foreach(DataGridViewRow row in tblSPinCombo.Rows)
                {
                    DeviceDTO device = new DeviceDTO();
                    device.id = Convert.ToInt32(row.Cells[0].Value);
                    device.name = row.Cells[1].Value.ToString();
                    device.amount = Convert.ToInt32(row.Cells[2].Value);
                    device.image = deviceBLL.getDeviceById(device.id).image;
                    device.price = deviceBLL.getDeviceById(device.id).price;

                    list.Add(device);
                }
                combo.deviceList = JsonConvert.SerializeObject(list);

                comboBLL.addCombo(combo);
                MessageBox.Show("Thêm thành công", "Thông báo");

                //add combo vua them vao table
                tblCombo.Rows.Add(comboBLL.getCurrentComboId(), combo.name, combo.amount, combo.finalMoney);
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try { 
                decimal gia = (Convert.ToDecimal(txtTotalMoney.Text)) - (Convert.ToDecimal(txtTotalMoney.Text) * (Convert.ToDecimal(txtDiscount.Text) / 100));
                txtFinalMoney.Text = gia.ToString();
            }
            catch (Exception) { return; }
        }

        private void tblSPinCombo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int selectedrowindex = tblSPinCombo.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = tblSPinCombo.Rows[selectedrowindex];
                if (selectedrowindex >= 0 && (selectedRow.Cells[0].Value != null))
                {
                    tblSPinCombo.Rows.Remove(selectedRow);
                }
                else return;
            }
            catch (Exception) { return; }
        }

        private void tblSPinCombo_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            double total = 0;
            foreach (DataGridViewRow row in tblSPinCombo.Rows)
            {
                total += (Convert.ToDouble(row.Cells[2].Value)) * (Convert.ToDouble(row.Cells[3].Value));
            }
            txtTotalMoney.Text = total.ToString();
        }

        private void tblSPinCombo_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            double total = 0;
            foreach (DataGridViewRow row in tblSPinCombo.Rows)
            {
                total += (Convert.ToDouble(row.Cells[2].Value)) * (Convert.ToDouble(row.Cells[3].Value));

            }
            txtTotalMoney.Text = total.ToString();
        }

        private void tblSPinCombo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            double total = 0;
            foreach(DataGridViewRow row in tblSPinCombo.Rows)
            {
                total += (Convert.ToDouble(row.Cells[2].Value)) * (Convert.ToDouble(row.Cells[3].Value));
            }
            txtTotalMoney.Text = total.ToString();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            ComboBLL comboBLL = new ComboBLL();
            ComboDTO combo = new ComboDTO();
            List<DeviceDTO> list = new List<DeviceDTO>();
            DeviceBLL deviceBLL = new DeviceBLL();
            try
            {
                int selectedrowindex = tblCombo.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = tblCombo.Rows[selectedrowindex];
                if (selectedrowindex >= 0 && (selectedRow.Cells[0].Value != null))
                {
                    //sua duoi db
                    combo.name = txtname.Text;
                    combo.amount = Convert.ToInt32(txtAmount.Text);
                    combo.discount = Convert.ToInt32(txtDiscount.Text);
                    combo.totalMoney = Convert.ToDecimal(txtTotalMoney.Text);
                    combo.finalMoney = Convert.ToDecimal(txtFinalMoney.Text);
                    
                    foreach(DataGridViewRow row in tblSPinCombo.Rows)
                    {
                        DeviceDTO device = new DeviceDTO();
                        device.id = Convert.ToInt32(row.Cells[0].Value);
                        device.name = row.Cells[1].Value.ToString();
                        device.amount = Convert.ToInt32(row.Cells[2].Value);
                        device.image = deviceBLL.getDeviceById(device.id).image;
                        device.price = deviceBLL.getDeviceById(device.id).price;

                        list.Add(device);
                    }
                    combo.deviceList = JsonConvert.SerializeObject(list);
                    comboBLL.updateCombo(Convert.ToInt32(txtIdCombo.Text),combo);


                    //sua trong table
                    selectedRow.Cells[1].Value = txtname.Text;
                    selectedRow.Cells[2].Value = txtAmount.Text;
                    selectedRow.Cells[3].Value = txtFinalMoney.Text;
                MessageBox.Show("Sửa Combo thành công!!", "Thông báo");
                }
        }
            catch (Exception) { return; }
}

        private void btndelete_Click(object sender, EventArgs e)
        {
            ComboBLL comboBLL = new ComboBLL();
            try
            {
                int selectedrowindex = tblCombo.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = tblCombo.Rows[selectedrowindex];
                if (selectedrowindex >= 0 && (selectedRow.Cells[0].Value != null))
                {
                    comboBLL.deleteCombo(Convert.ToInt32(selectedRow.Cells[0].Value));
                    tblCombo.Rows.Remove(selectedRow);

                }
                else return;
                MessageBox.Show("Xóa Combo thành công!!", "Thông báo");
            }
            catch (Exception) { return; }
        }

        private void txtTotalMoney_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal gia = (Convert.ToDecimal(txtTotalMoney.Text)) - (Convert.ToDecimal(txtTotalMoney.Text) * (Convert.ToDecimal(txtDiscount.Text) / 100));
                txtFinalMoney.Text = gia.ToString();
            }
            catch (Exception) { return; }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            new Main().Visible = true;
        }
    }
}
