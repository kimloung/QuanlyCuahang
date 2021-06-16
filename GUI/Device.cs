using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QuanLyBanHang.BLL;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.GUI
{
    public partial class Device : Form
    {
        
        public Device()
        {
            InitializeComponent();           
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }


        private void btnadd_Click(object sender, EventArgs e)
        {
            DeviceBLL deviceBLL = new DeviceBLL();
            DeviceDTO dv = new DeviceDTO();                
            try
            {   
                dv.name = txtname.Text;
                dv.image = pictureBox.ImageLocation;
                dv.detail = txtdetail.Text;
                dv.catalog = combodanhmuc.SelectedValue.ToString();
                dv.type = comboloai.SelectedValue.ToString();
                dv.amount = Convert.ToInt32(txtamount.Text);
                dv.price = decimal.Parse(txtprice.Text);
                deviceBLL.addDevice(dv);
                List<DeviceDTO> list = deviceBLL.getAll();
                cbcatalog.DataSource = list;
                cbcatalog.DisplayMember = "catalog";
                cbcatalog.ValueMember = "catalog";
                cbtype.DataSource = list;
                cbtype.DisplayMember = "type";
                cbtype.ValueMember = "type";
                MessageBox.Show("Đã thêm sản phẩm thành công");                
            }
            catch (Exception) { MessageBox.Show("Thêm sản phẩm không thành công"); return; }
            tblDevice.Rows.Add(deviceBLL.getCurrentDeviceId(), dv.name, dv.image, dv.detail, dv.type, dv.catalog, dv.amount, dv.price);                                                                                                                          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            new Main().Visible = true;
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            DeviceBLL deviceBLL = new DeviceBLL();
            DeviceDTO dv = new DeviceDTO();
            try
            {
                int selectedrowindex = tblDevice.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = tblDevice.Rows[selectedrowindex];
                if (selectedrowindex >= 0 && (selectedRow.Cells[0].Value != null) && (selectedRow.Cells[2].Value != null)) 
                {
                    //string typeItem = cbcatalog.SelectedValue.ToString();
                 

                    //sua duoi db
                    dv.name = txtname.Text;
                        //dv.image = pictureBox.ImageLocation.ToString(); //url hình ảnh
                        dv.image = selectedRow.Cells[2].Value.ToString();                   
                        dv.detail = txtdetail.Text;
                        dv.catalog = combodanhmuc.SelectedValue.ToString();
                        dv.type = comboloai.SelectedValue.ToString();
                        dv.amount = Convert.ToInt32(txtamount.Text);
                        dv.price = decimal.Parse(txtprice.Text);
                        deviceBLL.updateDevice(Convert.ToInt32(txtid.Text), dv);
                  
                        // sua trong table
                        selectedRow.Cells[1].Value = txtname.Text;
                        selectedRow.Cells[2].Value = pictureBox.ImageLocation.ToString();
                        selectedRow.Cells[3].Value = txtdetail.Text;
                        selectedRow.Cells[4].Value = comboloai.SelectedValue.ToString();
                        selectedRow.Cells[5].Value = combodanhmuc.SelectedValue.ToString();
                        selectedRow.Cells[6].Value = Convert.ToInt32(txtamount.Text);
                        selectedRow.Cells[7].Value = decimal.Parse(txtprice.Text);

                        tblDevice.Refresh();
                        tblDevice.Update();
                    List<DeviceDTO> list = deviceBLL.getAll();
                    cbcatalog.DataSource = list;
                    cbcatalog.DisplayMember = "catalog";
                    cbcatalog.ValueMember = "catalog";
                    cbtype.DataSource = list;
                    cbtype.DisplayMember = "type";
                    cbtype.ValueMember = "type";
                    MessageBox.Show("Sửa sản phẩm thành công");
                                   
                }
                else
                { MessageBox.Show("Sửa sản phẩm không thành công"); }
            }
            catch (Exception) { MessageBox.Show("Sửa sản phẩm không thành công"); }
        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
           
            DeviceBLL deviceBLL = new DeviceBLL();
            try
            {
                int selectedrowindex = tblDevice.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = tblDevice.Rows[selectedrowindex];
                if (selectedrowindex >= 0 && (selectedRow.Cells[0].Value != null))
                {
                    deviceBLL.deleteDevice(Convert.ToInt32(selectedRow.Cells[0].Value));
                    tblDevice.Rows.Remove(selectedRow);
                    List<DeviceDTO> list = deviceBLL.getAll();
                    cbcatalog.DataSource = list;
                    cbcatalog.DisplayMember = "catalog";
                    cbcatalog.ValueMember = "catalog";

                    cbtype.DataSource = list;
                    cbtype.DisplayMember = "type";
                    cbtype.ValueMember = "type";

                }
                else return;
            }
            catch (Exception) { return; }
        }

        private void Device_Load(object sender, EventArgs e) 
        {
            DeviceBLL deviceBLL = new DeviceBLL();

            String[] myArray = { "Laptop", "Máy bộ", "Linh kiện", "Phụ kiện"};
            combodanhmuc.DataSource = myArray;

            String[] myArray1 = { "Học tập - Văn phòng", "Đồ họa - Kỹ thuật", "Mỏng nhẹ", "Cao cấp - Sang trọng", "Gaming", "Workstation", "Mainboard", "CPU", "RAM", "VGA", "SSD", "Webcam", "Microphone", "Dây cáp" };
            comboloai.DataSource = myArray1;

            List<DeviceDTO> list = deviceBLL.getAll();
            foreach (DeviceDTO dv in list)
            {
                tblDevice.Rows.Add(dv.id, dv.name, dv.image, dv.detail, dv.type, dv.catalog, dv.amount, dv.price); 
            }

            //Load combo box
                
           cbcatalog.DataSource = list;
           cbcatalog.DisplayMember = "catalog";
           cbcatalog.ValueMember = "catalog";

            cbtype.DataSource = list;
            cbtype.DisplayMember = "type";
            cbtype.ValueMember = "type";

        }

        private void txtcatalog_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtid_TextChanged(object sender, EventArgs e)
        {

        }

        private void tblDevice_SelectionChanged(object sender, EventArgs e) 
        {
            try
            {      
                foreach (DataGridViewRow row in tblDevice.SelectedRows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        txtid.Text = row.Cells[0].Value.ToString();
                        txtname.Text = row.Cells[1].Value.ToString();
                        txtdetail.Text = row.Cells[3].Value.ToString();
                        comboloai.SelectedItem = row.Cells[4].Value.ToString();
                        combodanhmuc.SelectedItem = row.Cells[5].Value.ToString();
                        txtamount.Text = row.Cells[6].Value.ToString();
                        txtprice.Text = row.Cells[7].Value.ToString();
                        pictureBox.Image = Image.FromFile(row.Cells[2].Value.ToString());
                    }
                    else { return; }
                }
            }
            catch (Exception) { return; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String imageLocation = "";
            //int selectedrowindex = tblDevice.SelectedCells[0].RowIndex;
            //DataGridViewRow selectedRow = tblDevice.Rows[selectedrowindex];
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All files(*.*)|*.*";
                //SaveFileDialog sf = new SaveFileDialog();
                //sf.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All files(*.*)|*.*";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //pictureBox.Image = new Bitmap(imageLocation);    
                    //imageLocation(sf.FileName);
                    //pictureBox.Image.Save(sf.FileName);
                    imageLocation = dialog.FileName.ToString();                    
                    pictureBox.ImageLocation = imageLocation;                                        
                }
                //selectedRow.Cells[2].Value = imageLocation;
            }
            catch (Exception) { MessageBox.Show("Upload ảnh không thành công"); }
            
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            int selectedrowindex = tblDevice.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = tblDevice.Rows[selectedrowindex];
            txtid.Text = "";
            txtname.Text = "";
            pictureBox.ImageLocation = null;
           // combodanhmuc.SelectedItem = "";
            //comboloai.SelectedItem = "";
         //   txtcatalog.Text = "";
          //  txttype.Text = "";
            txtprice.Text = "";
            txtamount.Text = "";
            txtdetail.Text = "";
        }

      
        private void pictureBox_Click(object sender, EventArgs e)
        {
            
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            DeviceBLL deviceBLL = new DeviceBLL();
            tblsearch.Rows.Clear();
            //Ten
            if (cbcatalog.Text.Equals("") && cbtype.Text.Equals(""))
            {
                List<DeviceDTO> results = deviceBLL.searchByName(txtTKname.Text);
                foreach (DeviceDTO kq in results)
                {
                    tblsearch.Rows.Add(kq.id, kq.name, kq.type, kq.catalog, kq.amount, kq.price);
                }
            }

            // Catalog
            if (cbtype.Text.Equals("") && txtTKname.Text.Equals(""))
            {
                List<DeviceDTO> results = deviceBLL.searchByCatalog(cbcatalog.Text);
                foreach (DeviceDTO kq in results)
                {
                    tblsearch.Rows.Add(kq.id, kq.name, kq.type, kq.catalog, kq.amount, kq.price);
                }
            }

            // Type
            if (cbcatalog.Text.Equals("") && txtTKname.Text.Equals(""))
            {
                List<DeviceDTO> results = deviceBLL.searchByType(cbtype.Text);
                foreach (DeviceDTO kq in results)
                {
                    tblsearch.Rows.Add(kq.id, kq.name, kq.type, kq.catalog, kq.amount, kq.price);
                }
            }

            // Ten + Catalog
            if (cbtype.Text.Equals(""))
            {
                List<DeviceDTO> results = deviceBLL.searchByNameAndCatalog(txtTKname.Text, cbcatalog.Text);
                foreach (DeviceDTO kq in results)
                {
                    tblsearch.Rows.Add(kq.id, kq.name, kq.type, kq.catalog, kq.amount, kq.price);
                }
            }

            // Ten + Type
            if (cbcatalog.Text.Equals(""))
            {
                List<DeviceDTO> results = deviceBLL.searchByTypeAndName(cbtype.Text, txtTKname.Text);
                foreach (DeviceDTO kq in results)
                {
                    tblsearch.Rows.Add(kq.id, kq.name, kq.type, kq.catalog, kq.amount, kq.price);
                }
            }

            // Catalog + Type
            if (txtTKname.Text.Equals(""))
            {
                List<DeviceDTO> results = deviceBLL.searchByTypeAndCatalog(cbtype.Text, cbcatalog.Text);
                foreach (DeviceDTO kq in results)
                {
                     tblsearch.Rows.Add(kq.id, kq.name, kq.type, kq.catalog, kq.amount, kq.price);
                }
            }

            // Ten + Type + Catalog
            else
            {
                List<DeviceDTO> results = deviceBLL.searchByTypeAndCatalogAndName(cbtype.Text, cbcatalog.Text, txtTKname.Text);
                foreach (DeviceDTO kq in results)
                {
                    tblsearch.Rows.Add(kq.id, kq.name, kq.type, kq.catalog, kq.amount, kq.price);
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txttype_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtTKname_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnrenew_Click(object sender, EventArgs e)
        {

        }

        private void cbtype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
