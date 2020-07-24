using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO_Practical2
{
    public partial class Form1 : Form
    {
        private Fabrikam_ADOEntities context = new Fabrikam_ADOEntities();
        private string[] majorsArr = new string[5] { "English", "Computer Science", "MIS", "Physics", "Mathematics" };

        public Form1()
        {
            InitializeComponent();
            majorDrop.Items.AddRange(majorsArr); //adds Major options to ComboBox
        }

        private void findBtn_Click(object sender, EventArgs e)
        {
            string firstName = firstTextBox.Text;
            string lastName = lastTextBox.Text;

            var query =
                from c in context.Students
                where c.FirstName == firstName && c.LastName == lastName
                select c;

            if(!query.Any()) MessageBox.Show("This student does not exist in our system.");

            foreach (var obj in query)
            {
                if (obj.Major != null)
                {
                    MessageBox.Show(
                        $"{firstName} {lastName} successfully found. Their Id is {obj.Id} and Major is {obj.Major}.");
                }
                else
                {
                    MessageBox.Show(
                        $"{firstName} {lastName} successfully found. Their Id is {obj.Id} and Major is unidentified.");
                }
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            string firstName = firstTextBox.Text;
            string lastName = lastTextBox.Text;
            string major = null;
            if (majorDrop.SelectedItem != null)
            {
                major = majorDrop.SelectedItem.ToString();
            }

            var student = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                Major = major
            };

            context.Students.Add(student);
            try
            {
                context.SaveChanges();
                MessageBox.Show("Insert Successful.");
            }
            catch (DbUpdateException ex) //Weird error. once duplicate entry tried, all further attempt receive error
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Insert Unsuccessful. Duplicate Entry.");
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            string firstName = firstTextBox.Text;
            string lastName = lastTextBox.Text;

            var query =
                from c in context.Students
                where c.FirstName == firstName && c.LastName == lastName
                select c;

            if (!query.Any())
            {
                MessageBox.Show("This student does not exist in our system.");
            }
            else
            {
                MessageBox.Show("Update Successful.");
            }

            foreach (var obj in query)
            {
                obj.Major = majorDrop.SelectedItem.ToString();
            }

            context.SaveChanges();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            string firstName = firstTextBox.Text;
            string lastName = lastTextBox.Text;

            var query =
                from c in context.Students
                where c.FirstName == firstName && c.LastName == lastName
                select c;

            if (!query.Any())
            {
                MessageBox.Show("This student does not exist in our system.");
            }
            else
            {
                MessageBox.Show("Update Successful.");
            }

            foreach (var obj in query)
            {
                context.Students.Remove(obj);
            }

            context.SaveChanges();
        }
    }
}
