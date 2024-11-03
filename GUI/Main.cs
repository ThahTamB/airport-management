using DevExpress.XtraEditors;
using DTO;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DevExpress.Mvvm.Native;
using System.Globalization;
using System.Windows.Controls;
using System.Runtime.Serialization;
using DevExpress.Charts.Native;
using DevExpress.CodeParser;
using LiveCharts;
using LiveCharts.Wpf;
using Separator = LiveCharts.Wpf.Separator;
using DevExpress.Pdf;
using DevExpress.XtraPrinting.Native.LayoutAdjustment;

namespace GUI
{
    public partial class Main : DevExpress.XtraEditors.XtraForm
    {
        BUS_Customer bCustomer = new BUS_Customer();
        BUS_Plane planebus = new BUS_Plane();
        BUS_Flight flightbus = new BUS_Flight();
        BUS_Ticket ticketbus = new BUS_Ticket();
        Account current_account;
        Employee current_employee;
        private List<System.Windows.Forms.Label> listSeat = new List<System.Windows.Forms.Label>();
        private System.Windows.Forms.Label current_seat = new System.Windows.Forms.Label();

        public Main(Account current_account, Employee current_employee)
        {
            InitializeComponent();
            this.current_account = current_account;
            this.current_employee = current_employee;
            MessageBox.Show("2");
            listSeat = initSeatUI();
            List<Flight> fls = flightbus.GetListFlights();
            loadSeatFlight(fls[0], 1);
        }

        void Main_Load(object sender, EventArgs e)
        {

        }

        // Customer Controller
        void tpCustomer_Paint(object sender, PaintEventArgs e)
        {
            gcCustomer.DataSource = bCustomer.getCustomerList();
        }

        void btnAddCustomer_Click(object sender, EventArgs e)
        {
            if (txtCustomerNationalID.Text != null && !txtCustomerNationalID.Text.IsEmptyOrSingle()
               && txtCustomerPhone.Text != null && !txtCustomerPhone.Text.IsEmptyOrSingle()
               && txtCustomerName.Text != null && !txtCustomerName.Text.IsEmptyOrSingle()
               && txtCustomerEmail.Text != null && !txtCustomerEmail.Text.IsEmptyOrSingle()
               && txtCustomerNationality.Text != null && !txtCustomerNationality.Text.IsEmptyOrSingle()
               && txtCustomerAddress.Text != null && !txtCustomerAddress.Text.IsEmptyOrSingle()
               && dtpCustomerDate.Text != null && (rbCustomerMale.Checked == true || rbCustomerFemale.Checked == true))
            {
                string customerID = txtCustomerNationalID.Text.Trim();
                string customerName = txtCustomerName.Text.Trim();
                string customerEmail = txtCustomerEmail.Text.Trim();
                string customerAddress = txtCustomerAddress.Text.Trim();
                string customerPhone = txtCustomerPhone.Text.Trim();
                string customerDate = dtpCustomerDate.Value.ToString("dd/MM/yyyy");
                string customerNationality = txtCustomerNationality.Text.Trim();
                bool? customerSex;

                if (rbCustomerFemale.Checked)
                {
                    customerSex = true;
                }
                else
                {
                    customerSex = false;
                }

                Customer customer = new Customer();
                customer.NationalID = customerID;
                customer.Name = customerName;
                customer.Email = customerEmail;
                customer.Address = customerAddress;
                customer.TeleNumber = customerPhone;
                customer.DateOfBirth = DateTime.ParseExact(customerDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                customer.Nationality = customerNationality;
                customer.Sex = customerSex;

                try
                {
                    if (bCustomer.addCustomer(customer))
                    {
                        MessageBox.Show("Add customer successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gcCustomer.DataSource = bCustomer.getCustomerList();
                    }
                    else
                    {
                        MessageBox.Show("Add customer failure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Not enough infomation about customer", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void gvCustomer_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2) //Send customer to ticket tab
            {
                tabControls.SelectedPageIndex = 0;
                Customer order_customer = (Customer)gvCustomer.GetRow(gvCustomer.FocusedRowHandle);
                lbTicketCustomerID.Text = order_customer.CustomerID.ToString();
                lbTicketCustomerName.Text = order_customer.Name;
                lbTicketCustomerAddress.Text = order_customer.Address;
                lbTicketCustomerPhone.Text = order_customer.TeleNumber;
                lbTicketCustomerNationalID.Text = order_customer.NationalID;
                if (order_customer.Sex == true)
                {
                    lbTicketCustomerSex.Text = "Nữ";
                }
                else
                {
                    lbTicketCustomerSex.Text = "Nam";
                }
                lbTicketCustomerDoB.Text = order_customer.DateOfBirth.Value.ToString("dd/MM/yyyy");
            }
            else if (e.Clicks == 1) //fetch data to textfield
            {

                if (gvCustomer.GetRow(gvCustomer.FocusedRowHandle) != null)
                {
                    Customer cur_customer = (Customer)gvCustomer.GetRow(gvCustomer.FocusedRowHandle);
                    txtCustomerID.Text = cur_customer.CustomerID.ToString();
                    txtCustomerNationalID.Text = cur_customer.NationalID;
                    txtCustomerName.Text = cur_customer.Name;
                    txtCustomerAddress.Text = cur_customer.Address;
                    txtCustomerPhone.Text = cur_customer.TeleNumber;
                    txtCustomerEmail.Text = cur_customer.Email;
                    txtCustomerNationality.Text = cur_customer.Nationality;
                    dtpCustomerDate.Value = cur_customer.DateOfBirth.Value;
                    if (cur_customer.Sex == true)
                    {
                        rbCustomerFemale.Checked = true;
                    }
                    else
                    {
                        rbCustomerMale.Checked = true;
                    }
                }

            }
        }

        void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            if (gvCustomer.GetRow(gvCustomer.FocusedRowHandle) != null)
            {
                if (txtCustomerNationalID.Text != null && !txtCustomerNationalID.Text.IsEmptyOrSingle()
               && txtCustomerPhone.Text != null && !txtCustomerPhone.Text.IsEmptyOrSingle()
               && txtCustomerName.Text != null && !txtCustomerName.Text.IsEmptyOrSingle()
               && txtCustomerEmail.Text != null && !txtCustomerEmail.Text.IsEmptyOrSingle()
               && txtCustomerNationality.Text != null && !txtCustomerNationality.Text.IsEmptyOrSingle()
               && txtCustomerAddress.Text != null && !txtCustomerAddress.Text.IsEmptyOrSingle()
               && dtpCustomerDate.Text != null && (rbCustomerMale.Checked == true || rbCustomerFemale.Checked == true))
                {
                    var current_customer = (Customer)gvCustomer.GetRow(gvCustomer.FocusedRowHandle);
                    var updated_customer = new Customer();
                    updated_customer.CustomerID = current_customer.CustomerID;
                    updated_customer.Name = txtCustomerName.Text.Trim();
                    updated_customer.NationalID = txtCustomerNationalID.Text.Trim();
                    updated_customer.Email = txtCustomerEmail.Text.Trim();
                    updated_customer.Address = txtCustomerAddress.Text.Trim();
                    updated_customer.TeleNumber = txtCustomerPhone.Text.Trim();
                    updated_customer.DateOfBirth = DateTime.ParseExact(dtpCustomerDate.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    updated_customer.Nationality = txtCustomerNationality.Text.Trim();

                    if (rbCustomerFemale.Checked)
                    {
                        updated_customer.Sex = true;
                    }
                    else
                    {
                        updated_customer.Sex = false;
                    }

                    try
                    {
                        if (bCustomer.updateCustomer(updated_customer))
                        {
                            MessageBox.Show("Update customer successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gcCustomer.DataSource = bCustomer.getCustomerList();
                        }
                        else
                        {
                            MessageBox.Show("Update customer failure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this customer?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (gvCustomer.GetRow(gvCustomer.FocusedRowHandle) != null)
                {
                    var current_customer = (Customer)gvCustomer.GetRow(gvCustomer.FocusedRowHandle);
                    var id = current_customer.CustomerID;

                    if (bCustomer.deleteCustomer(id))
                    {
                        MessageBox.Show("Delete customer successfully", "Success");
                        gcCustomer.DataSource = bCustomer.getCustomerList();
                    }
                    else
                    {
                        MessageBox.Show("Delete customer failure", "Error");
                    }
                }
            }
        }

        // PlaneController  =========>

        void gridControlPlane_Load(object sender, EventArgs e)
        {
            gridControlPlane.DataSource = planebus.GetListPlanes();

            if (!comboBoxPlaneState.Items.Contains("Free"))
            {
                comboBoxPlaneState.Items.Add("Free");
                comboBoxPlaneState.Items.Add("Busy");
            }
            comboBoxPlaneState.Text = "Free";
        }

        void GridPlaneRowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridPlane.GetRow(gridPlane.FocusedRowHandle) != null)
            {
                var pickedPlane = (Plane)gridPlane.GetRow(gridPlane.FocusedRowHandle);
                txtPlaneModel.Text = pickedPlane.Model;
                txtManufactor.Text = pickedPlane.Manufacturer;
                txtPlaneSeat.Text = pickedPlane.TotalSeat.ToString();
                txtRegistration.Text = pickedPlane.Registration;
                comboBoxPlaneState.SelectedIndex = pickedPlane.State > 0 ? 1 : 0;
            }
        }

        void ButtonAddPlaneClick(object sender, EventArgs e)
        {
            if (txtPlaneModel.Text != null && !txtPlaneModel.Text.IsEmptyOrSingle()
                                           && txtManufactor.Text != null && !txtManufactor.Text.IsEmptyOrSingle()
                                           && txtPlaneSeat.Text != null && !txtPlaneSeat.Text.IsEmptyOrSingle()
                                           && txtRegistration.Text != null && !txtRegistration.Text.IsEmptyOrSingle()
                                           && comboBoxPlaneState != null)
            {
                var obj = new Plane
                {
                    Model = txtPlaneModel.Text.Trim(),
                    Manufacturer = txtManufactor.Text.Trim(),
                    Registration = txtRegistration.Text.Trim(),
                    TotalSeat = int.Parse(txtPlaneSeat.Text.Trim()),
                    State = comboBoxPlaneState.SelectedIndex
                };

                try
                {
                    if (planebus.AddPlane(obj))
                    {
                        MessageBox.Show(@"Add plane successful!!!", @"SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gcCustomer.DataSource = bCustomer.getCustomerList();
                    }
                    else
                    {
                        MessageBox.Show(@"Add plane failed!!!", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(@"Missing plane data!!!", @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void ButtonUpdatePlaneClick(object sender, EventArgs e)
        {
            if (gridPlane.GetRow(gridPlane.FocusedRowHandle) != null)
            {
                if (txtPlaneModel.Text != null && !txtPlaneModel.Text.IsEmptyOrSingle()
                                               && txtManufactor.Text != null && !txtManufactor.Text.IsEmptyOrSingle()
                                               && txtPlaneSeat.Text != null && !txtPlaneSeat.Text.IsEmptyOrSingle()
                                               && txtRegistration.Text != null && !txtRegistration.Text.IsEmptyOrSingle()
                                               && comboBoxPlaneState != null)
                {
                    var pickedPlane = (Plane)gridPlane.GetRow(gridPlane.FocusedRowHandle);
                    var updatePlane = new Plane();
                    updatePlane.PlaneID = pickedPlane.PlaneID;
                    updatePlane.Model = txtPlaneModel.Text.Trim();
                    updatePlane.Manufacturer = txtManufactor.Text.Trim();
                    updatePlane.TotalSeat = int.Parse(txtPlaneSeat.Text.Trim());
                    updatePlane.Registration = txtRegistration.Text.Trim();
                    updatePlane.State = comboBoxPlaneState.SelectedIndex;

                    try
                    {
                        if (planebus.UpdatePlane(updatePlane))
                        {
                            MessageBox.Show(@"Update plane successful", @"SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gcCustomer.DataSource = bCustomer.getCustomerList();
                        }
                        else
                        {
                            MessageBox.Show(@"Update plane failed", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(@"Missing plane data!!!", @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(@"Missing plane data!!!", @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void ButtonDeletePlaneClick(object sender, EventArgs e)
        {
            if (txtPlaneModel.Text != null && !txtPlaneModel.Text.IsEmptyOrSingle()
                                           && txtManufactor.Text != null && !txtManufactor.Text.IsEmptyOrSingle()
                                           && txtPlaneSeat.Text != null && !txtPlaneSeat.Text.IsEmptyOrSingle()
                                           && txtRegistration.Text != null && !txtRegistration.Text.IsEmptyOrSingle()
                                           && comboBoxPlaneState != null)
            {
                var pickedPlane = (Plane)gridPlane.GetRow(gridPlane.FocusedRowHandle);

                try
                {
                    if (planebus.RemovePlane(pickedPlane))
                    {
                        MessageBox.Show(@"Remove plane successful", @"SUCCESS", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        gcCustomer.DataSource = bCustomer.getCustomerList();
                    }
                    else
                    {
                        MessageBox.Show(@"Remove plane failed", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // <=========

        // FlightController  =========>

        void GridControlFlightLoad(object sender, EventArgs e)
        {
            comboBoxDepart.DataSource = flightbus.GetLocations();
            comboBoxDesti.DataSource = flightbus.GetLocations();
            comboBoxDepart.DisplayMember = comboBoxDesti.DisplayMember = "LocationName";
            comboBoxPlane.DataSource = planebus.GetListPlanes();
            comboBoxPlane.DisplayMember = "Registration";
        }

        void gridViewFlight_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridViewFlight.GetRow(gridViewFlight.FocusedRowHandle) == null) return;
            var pickedFlight = (Flight)gridViewFlight.GetRow(gridViewFlight.FocusedRowHandle);
            var planeID = (Plane)comboBoxPlane.SelectedItem;
            var loDes = (Location)comboBoxDepart.SelectedItem;
            var loDep = (Location)comboBoxDesti.SelectedItem;
            textFlightID.Text = pickedFlight.FlightID.ToString();
            planeID.PlaneID = pickedFlight.PlaneID;
            textAirline.Text = pickedFlight.Airline;
            textFliPrice.Text = pickedFlight.Price.ToString();
            loDes.LocationID = (int)pickedFlight.Departure;
            loDep.LocationID = (int)pickedFlight.Destination;
            dateDepartPicker.Value = pickedFlight.DateOfDeparture.Date;
            timeDepartPicker.Value = DateTime.Parse(pickedFlight.DateOfDeparture.TimeOfDay.ToString());
        }

        void buttonAddFlight_Click(object sender, EventArgs e)
        {
            if (textAirline.Text != null && !textAirline.Text.IsEmptyOrSingle()
                                           && comboBoxDepart.SelectedItem != comboBoxDesti.SelectedItem)
            {
                var daTime = dateDepartPicker.Value.ToString("yyyy/MM/dd") + " " + timeDepartPicker.Value.ToString("HH:mm:ss");
                var planeFl = (Plane)comboBoxPlane.SelectedItem;
                var loDes = (Location)comboBoxDepart.SelectedItem;
                var loDep = (Location)comboBoxDesti.SelectedItem;

                var obj = new Flight()
                {
                    PlaneID = planeFl.PlaneID,
                    Airline = textAirline.Text.Trim(),
                    Departure = loDep.LocationID,
                    Destination = loDes.LocationID,
                    Price = Decimal.Parse(textFliPrice.Text),
                    DateOfDeparture = DateTime.Parse(daTime)
                };

                try
                {
                    if (flightbus.AddFlights(obj))
                    {
                        MessageBox.Show(@"Add flight successful!!!", @"SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gcCustomer.DataSource = bCustomer.getCustomerList();
                    }
                    else
                    {
                        MessageBox.Show(@"Add flight failed!!!", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(@"Missing flight data!!!", @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void buttonUpdateFlight_Click(object sender, EventArgs e)
        {
            if (gridViewFlight.GetRow(gridViewFlight.FocusedRowHandle) != null)
            {
                if (textAirline.Text != null && !textAirline.Text.IsEmptyOrSingle()
                                                && comboBoxDepart.SelectedItem != comboBoxDesti.SelectedItem)
                {
                    var pickedFlight = (Flight)gridViewFlight.GetRow(gridViewFlight.FocusedRowHandle);
                    var updatedFlight = new Flight();
                    var daTime = dateDepartPicker.Value.ToString("yyyy/MM/dd") + " " + timeDepartPicker.Value.ToString("HH:mm:ss");
                    var planeFl = (Plane)comboBoxDepart.SelectedItem;
                    var loDes = (Location)comboBoxDepart.SelectedItem;
                    var loDep = (Location)comboBoxDesti.SelectedItem;
                    updatedFlight.FlightID = pickedFlight.FlightID;
                    updatedFlight.PlaneID = planeFl.PlaneID;
                    updatedFlight.Airline = textAirline.Text.Trim();
                    updatedFlight.Departure = loDep.LocationID;
                    updatedFlight.Destination = loDes.LocationID;
                    updatedFlight.Price = Decimal.Parse(textFliPrice.Text);
                    updatedFlight.DateOfDeparture = DateTime.Parse(daTime);

                    try
                    {
                        if (flightbus.UpdateFlights(updatedFlight))
                        {
                            MessageBox.Show(@"Update flight successful", @"SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gcCustomer.DataSource = bCustomer.getCustomerList();
                        }
                        else
                        {
                            MessageBox.Show(@"Update flight failed", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(@"Missing flight data!!!", @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(@"Missing flight data!!!", @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void buttonDeleteFlight_Click(object sender, EventArgs e)
        {
            var pickedFlight = (Flight)gridViewFlight.GetRow(gridViewFlight.FocusedRowHandle);

            try
            {
                if (flightbus.DeleteFlights(pickedFlight))
                {
                    MessageBox.Show(@"Remove flight successful", @"SUCCESS", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    gcCustomer.DataSource = bCustomer.getCustomerList();
                }
                else
                {
                    MessageBox.Show(@"Remove flight failed", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void TabNavigationFlightPaint(object sender, PaintEventArgs e)
        {
            gridControlFlight.DataSource = flightbus.GetListFlights();
        }
        // <=========

        // EmployeeAccountJobController  =========>

        void LoadDataGridViewEmployee()
        {
            using (AirportManager db = new AirportManager())
            {
                dgvEmployee.DataSource = db.Employees.ToList<Employee>();
            }
        }
        void LoadDataGridViewJob()
        {
            using (AirportManager db = new AirportManager())
            {
                dgvJob.DataSource = db.Jobs.ToList<Job>();
            }
        }
        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            Account account = new Account();
            BUS_Employee busEmp = new BUS_Employee();
            BUS_Account busAcc = new BUS_Account();
            DAL_Account dalAccount = new DAL_Account();
            DAL_Employee dalEmployee = new DAL_Employee();

            if (String.IsNullOrEmpty(txtNameEmployee.Text) || String.IsNullOrEmpty(txtAddressEmployee.Text) || String.IsNullOrEmpty(txtNationalityEmployee.Text) || String.IsNullOrEmpty(txtEmailEmployee.Text) || String.IsNullOrEmpty(txtNationalIDEmployee.Text) || String.IsNullOrEmpty(txtPhoneEmployee.Text) || String.IsNullOrEmpty(txtPhoneEmployee.Text))
            {
                MessageBox.Show("Please enter all the information!");
            }
            else
            {
                employee.Name = txtNameEmployee.Text.Trim();
                employee.Address = txtAddressEmployee.Text.Trim();
                employee.Nationality = txtNationalityEmployee.Text.Trim();
                if (rbMale.Checked)
                    employee.Sex = true;
                else
                    employee.Sex = false;
                employee.DateOfBirth = dtpBirthdayEmployee.Value.Date;
                employee.Email = txtEmailEmployee.Text.Trim();
                employee.NationID = txtNationalIDEmployee.Text.Trim();
                employee.TeleNumber = txtPhoneEmployee.Text.Trim();
                employee.Position = txtPositionEmployee.Text.Trim();




                if (busEmp.AddEmployee(employee) == 1)
                {
                    account.EmployeeID = dalAccount.takeEmployeeIDbyEmployeeNationalID(txtNationalIDEmployee.Text.Trim());
                    account.Username = txtUsernameEmployee.Text.Trim();
                    account.Password = txtPasswordEmployee.Text.Trim();
                    account.AccessLevel = false;

                    if (busAcc.AddAccount(account) == 2)
                    {
                        MessageBox.Show("Username was exited");
                        LoadDataGridViewEmployee();
                    }
                    else
                    {
                        if (busAcc.AddAccount(account) == 1)
                        {
                            MessageBox.Show("Add Employee Successfully");
                            LoadDataGridViewEmployee();
                        }
                        else
                            MessageBox.Show("Add Employee Successfully");
                    }
                }
                else
                {
                    if (busEmp.AddEmployee(employee) == 0)
                        MessageBox.Show("Add Employee Fail");
                    else
                        if (busEmp.AddEmployee(employee) == 2)
                        MessageBox.Show("Exited Phone");
                    else
                            if (busEmp.AddEmployee(employee) == 3)
                        MessageBox.Show("Exited NationalID");
                    else
                                if (busEmp.AddEmployee(employee) == 4)
                        MessageBox.Show("Exited NationalID and Phone");
                    else
                        MessageBox.Show("Something wrong");
                }

            }
        }

        private void gbEmployee_Paint(object sender, PaintEventArgs e)
        {
            txtEmployeeID.ReadOnly = true;
            LoadDataGridViewEmployee();
        }
        private void gbJob_Paint(object sender, PaintEventArgs e)
        {
            LoadDataGridViewJob();
        }

        private void gvEmployee_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gvEmploy.GetRow(gvEmploy.FocusedRowHandle) != null)
            {
                Employee cur_emp = (Employee)gvEmploy.GetRow(gvEmploy.FocusedRowHandle);
                txtEmployeeID.Text = cur_emp.EmployeeID.ToString();
                txtNameEmployee.Text = cur_emp.Name.ToString();
                txtAddressEmployee.Text = cur_emp.Address.ToString();
                txtNationalityEmployee.Text = cur_emp.Nationality.ToString();
                //    dtpBirthday.Value.Date = cur_emp.DateOfBirth.;
                txtNationalIDEmployee.Text = cur_emp.NationID.ToString();
                txtEmailEmployee.Text = cur_emp.Email.ToString();
                txtNationalIDEmployee.Text = cur_emp.NationID.ToString();
                txtPhoneEmployee.Text = cur_emp.TeleNumber.ToString();
                txtPositionEmployee.Text = cur_emp.Position.ToString();
                if (cur_emp.Sex == true)
                    rbMale.Checked = true;
                else
                    rbFelmale.Checked = true;
            }
        }
        private void gvJob_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gvJob.GetRow(gvJob.FocusedRowHandle) != null)
            {
                Job cur_job = (Job)gvJob.GetRow(gvJob.FocusedRowHandle);
                txtJobID.Text = cur_job.JobID.ToString();
                dtpAssignedDateJob.Text = cur_job.AssignedDate.ToString();
                txtEmpIDJob.Text = cur_job.EmployeeID.ToString();
                txtFightIDJob.Text = cur_job.FlightID.ToString();
                txtJobDescription.Text = cur_job.JobDescription.ToString();
                cbStateJob.Text= cur_job.JobState.ToString();
                
            }
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            Account account = new Account();
            BUS_Employee busEmp = new BUS_Employee();
            BUS_Account busAcc = new BUS_Account();
            DAL_Account dalAccount = new DAL_Account();
            DAL_Employee dalEmployee = new DAL_Employee();

            if (String.IsNullOrEmpty(txtEmployeeID.Text))
            {
                MessageBox.Show("Please select Employee you want to delete");
            }
            else
            {
                if (MessageBox.Show("Do you want to delete this employee?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (dalEmployee.CheckExitEmployee(Int32.Parse(txtEmployeeID.Text)) == false)
                    {
                        MessageBox.Show("This employee not exit!");
                    }
                    else
                    {
                        if (busAcc.DeleteAccount(Int32.Parse(txtEmployeeID.Text)) == 1)
                        {
                            if (busEmp.DeleteEmployee(Int32.Parse(txtEmployeeID.Text)) == 1)
                                MessageBox.Show("Delete Employee Successfully");
                            else
                                MessageBox.Show("Delete Employee Fail");
                        }
                        else
                        {
                            MessageBox.Show("Fail, Something wrong");
                        }

                    }
                }
            }
        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            Account account = new Account();
            BUS_Employee busEmp = new BUS_Employee();
            BUS_Account busAcc = new BUS_Account();
            DAL_Account dalAccount = new DAL_Account();
            DAL_Employee dalEmployee = new DAL_Employee();

            if (String.IsNullOrEmpty(txtEmployeeID.Text) || String.IsNullOrEmpty(txtNameEmployee.Text) || String.IsNullOrEmpty(txtAddressEmployee.Text) || String.IsNullOrEmpty(txtNationalityEmployee.Text) || String.IsNullOrEmpty(txtEmailEmployee.Text) || String.IsNullOrEmpty(txtNationalIDEmployee.Text) || String.IsNullOrEmpty(txtPhoneEmployee.Text) || String.IsNullOrEmpty(txtUsernameEmployee.Text) || String.IsNullOrEmpty(txtPasswordEmployee.Text))
            {
                MessageBox.Show("Please enter all the information!");
            }
            else
            {
                employee.EmployeeID = int.Parse(txtEmployeeID.Text.Trim());
                employee.Name = txtNameEmployee.Text.Trim();
                employee.Address = txtAddressEmployee.Text.Trim();
                employee.Nationality = txtNationalityEmployee.Text.Trim();
                if (rbMale.Checked)
                    employee.Sex = true;
                else
                    employee.Sex = false;
                employee.DateOfBirth = dtpBirthdayEmployee.Value.Date;
                employee.Email = txtEmailEmployee.Text.Trim();
                employee.NationID = txtNationalIDEmployee.Text.Trim();
                employee.TeleNumber = txtPhoneEmployee.Text.Trim();
                employee.Position = txtPositionEmployee.Text.Trim();


                if (busEmp.UpdateEmployee(employee) == 2)
                {
                    MessageBox.Show("EmployeeID not exit");
                }
                else
                {
                    if (busEmp.UpdateEmployee(employee) == 1)
                    {
                        account.EmployeeID = dalAccount.takeEmployeeIDbyEmployeeNationalID(txtNationalIDEmployee.Text.Trim());
                        account.Username = txtUsernameEmployee.Text.Trim();
                        account.Password = txtPasswordEmployee.Text.Trim();
                        account.AccessLevel = false;
                        dalAccount.UpdateAccount(account);

                        MessageBox.Show("Update Employee successfully");
                    }
                    else
                        MessageBox.Show("Something Wrong");
                }

            }
        }
        private void txtPasswordEmployee_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnAddJob_Click(object sender, EventArgs e)
        {
            BUS_Job busJob = new BUS_Job();
            Job job = new Job();
            if (String.IsNullOrEmpty(txtEmpIDJob.Text) || String.IsNullOrEmpty(txtFightIDJob.Text) || String.IsNullOrEmpty(txtJobDescription.Text) || cbStateJob.SelectedItem == null)
            {
                MessageBox.Show("Please enter all the information!");
            }
            else
            {
                job.AssignedDate = dtpAssignedDateJob.Value.Date;
                job.EmployeeID = int.Parse(txtEmpIDJob.Text.Trim());
                job.FlightID = int.Parse(txtFightIDJob.Text.Trim());
                job.JobDescription = txtJobDescription.Text.Trim();
                job.JobState = cbStateJob.Text.Trim();
                
                if (busJob.AddJob(job) == 1)
                {
                    MessageBox.Show(" Add job successfull");
                    LoadDataGridViewJob();
                }
                else
                {
                    if (busJob.AddJob(job) == 2)
                    {
                        MessageBox.Show("NOT EXIT FLIGHTID");
                    }
                    else
                    {
                        if (busJob.AddJob(job) == 3)
                        {
                            MessageBox.Show("NOT EXIT EMPLOYEEID");
                        }
                        else
                            MessageBox.Show("Something wrong");
                    }
                }
            }
        }

        // Ticket Controller

        private void gridTicket_Load(object sender, EventArgs e)
        {
            comboBoxTickDepart.DataSource = flightbus.GetLocations();
            comboBoxTickDesti.DataSource = flightbus.GetLocations();
            comboBoxTickDepart.DisplayMember = comboBoxTickDesti.DisplayMember = "LocationName";
        }

        private void comboBoxTickDateDepart_DropDown(object sender, EventArgs e)
        {
            try
            {
                var loDep = (Location)comboBoxTickDepart.SelectedItem;
                var loDes = (Location)comboBoxTickDesti.SelectedItem;
                comboBoxTickDateDepart.DataSource = flightbus.GetDatebyLocations(loDep.LocationID, loDes.LocationID);
                comboBoxTickDateDepart.DisplayMember = "DateOfDeparture";
            }
            catch (Exception ex) { MessageBox.Show("I don't believe it"); }
        }

        private void buttonAddTicket_Click(object sender, EventArgs e)
        {
            var datez = (Flight)comboBoxTickDateDepart.SelectedItem;
            Bill_Detail bt = new Bill_Detail();
            bt.CustomerID = int.Parse(lbTicketCustomerID.Text);
            bt.FlightID = datez.FlightID;
            bt.EmployeeID = current_account.EmployeeID;
            bt.SeatNumber = current_seat.Text;
            if (bt.SeatNumber.Substring(0, 1) == "A" || bt.SeatNumber.Substring(0, 1) == "B")
                bt.SeatClass = true;
            else
                bt.SeatClass = false;

            if (!bt.SeatClass)
                bt.TotalPrice = datez.Price;
            else
                bt.TotalPrice = datez.Price * (decimal)1.6;
            bt.BookingDate = DateTime.Now;
            ticketbus.AddBillService(bt);
        }

        private void buttonDeleteTicket_Click(object sender, EventArgs e)
        {
            var pickedTick = (Bill_Detail)gridViewTick.GetRow(gridViewTick.FocusedRowHandle);
            BUS_Ticket.DeleteBillService(pickedTick);
        }

        private List<System.Windows.Forms.Label> initSeatUI()
        {
            var labels = groupBoxSeat.Controls.OfType<System.Windows.Forms.Label>();
            List<System.Windows.Forms.Label> seats = new List<System.Windows.Forms.Label>();
            foreach (var label in labels)
                if (label.Name.Substring(1, 2).All(char.IsDigit))
                    seats.Add(label);
            foreach (var seat in seats)
            {
                seat.ForeColor = Color.White;
                seat.BackColor = Color.Gray;
                seat.Click += new EventHandler(changeStageSeat);
            }
            current_seat = null;
            return seats;
        }

        private void changeStageSeat(object sender, EventArgs e)
        {
            System.Windows.Forms.Label chosen_seat = (System.Windows.Forms.Label)sender;
            if (chosen_seat.BackColor != Color.Red)
            {
                if (current_seat != null)
                    current_seat.BackColor = Color.Gray;
                chosen_seat.BackColor = Color.LightGreen;
                current_seat = chosen_seat;
            }
        }

        private void loadSeatFlight(Flight fl, int ticket_id)
        {
            current_seat = null;
            List<Bill_Detail> listTicket = fl.Bill_Detail.ToList();
            foreach (var seat in listSeat)
            {
                foreach (var t in listTicket)
                {
                    if (seat.Name.Equals(t.SeatNumber))
                    {
                        if (t.BillID == ticket_id)
                        {
                            seat.BackColor = Color.LightGreen;
                            current_seat = seat;
                            break;
                        }
                        seat.BackColor = Color.Red;
                        break;
                    }
                    seat.BackColor = Color.Gray;
                }
            }

        }

        private void tabNavigationPage1_Paint(object sender, PaintEventArgs e)
        {
            gridTicket.DataSource = ticketbus.GetListBills();
        }

        private void gridViewTick_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridViewTick.GetRow(gridViewTick.FocusedRowHandle) != null)
            {
                var pickedTick = (Bill_Detail)gridViewTick.GetRow(gridViewTick.FocusedRowHandle);
                
                textEdit14.Text = pickedTick.BillID.ToString();
                comboBoxTickDepart.SelectedItem = pickedTick.Flight.Departure;
                comboBoxTickDesti.SelectedItem = pickedTick.Flight.Destination;
                comboBoxTickDateDepart.SelectedItem = pickedTick.Flight.DateOfDeparture;
                textEdit12.Text = pickedTick.Flight.Price.ToString();
                lbTicketCustomerID.Text = pickedTick.Customer.CustomerID.ToString();
                lbTicketCustomerName.Text = pickedTick.Customer.Name;
                lbTicketCustomerAddress.Text = pickedTick.Customer.Address;
                lbTicketCustomerPhone.Text = pickedTick.Customer.TeleNumber;
                lbTicketCustomerNationalID.Text = pickedTick.Customer.NationalID;
                loadSeatFlight(pickedTick.Flight, pickedTick.BillID);

            }
        }

        private void btnUpdateJob_Click(object sender, EventArgs e)
        {
            Job job = new Job();
            BUS_Job busJob = new BUS_Job();
            job.JobID = Int32.Parse(txtJobID.Text.Trim());
            job.AssignedDate = dtpAssignedDateJob.Value.Date;
            job.EmployeeID = Int32.Parse(txtEmpIDJob.Text.Trim());
            job.FlightID = Int32.Parse(txtFightIDJob.Text.Trim());
            job.JobDescription = txtJobDescription.Text.Trim();
            job.JobState = cbStateJob.Text;
         
            if(busJob.UpdateJob(job) == 1)
            {
                MessageBox.Show("Update Job Successfully");
                LoadDataGridViewJob();  
            }
            else
            {
                if(busJob.UpdateJob(job) == 2)
                {
                    MessageBox.Show("Job Not Exited");
                }
                else
                {
                    MessageBox.Show("Update Job Fail");
                }

            }
        }

        private void btnDeleteJob_Click(object sender, EventArgs e)
        {
            BUS_Job busJob = new BUS_Job();
            Job job = new Job();
            job.JobID = Int32.Parse(txtJobID.Text.Trim());
            job.AssignedDate = dtpAssignedDateJob.Value.Date;
            job.EmployeeID = Int32.Parse(txtEmpIDJob.Text.Trim());
            job.FlightID = Int32.Parse(txtFightIDJob.Text.Trim());
            job.JobDescription = txtJobDescription.Text.Trim();
            job.JobState = cbStateJob.Text;
            if (busJob.DeleteJob(job) == 2)
            {
                MessageBox.Show("This Job not exited");
            }
            else
            {
                if(busJob.DeleteJob(job) == 1) 
                {
                    MessageBox.Show("Delete Job successfully");
                    LoadDataGridViewJob();
                }
                else
                {
                    if (busJob.DeleteJob(job) == 0)
                    {
                        MessageBox.Show("Delete Job Fail, something wrong");
                    }
                    else
                        MessageBox.Show("Delete Job successfully");
                    LoadDataGridViewJob();
                }
            }
        }
    }
}