using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MongoDBExample
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();

        }

       static MongoClient client = new MongoClient();
       static IMongoDatabase db = client.GetDatabase("Airdrop");
       static IMongoCollection<Person> collection = db.GetCollection<Person>("employees");
        Person person = new Person();
        static string Id;
        private void Form1_Load(object sender, EventArgs e)
        {

           LoadData();
        }
        // Load data from MongoDB
        public void LoadData()
        {
            var list = collection.AsQueryable().ToList<Person>();
            gridControl1.DataSource = list;
        }
        // load one person
        private void loadPerson()
        {
            person = collection.AsQueryable().Where(x=>x.Id==ObjectId.Parse(Id)).SingleOrDefault();
            txt_name.Text = person.FirstName;
            txt_ho.Text = person.LastName;
            txt_tuoi.Text = person.Age.ToString();
        }
        // Save Data
        public void Save()
        {
            
            //addnew
            if (person.Id == ObjectId.Empty)
            {
                person.FirstName = txt_name.Text;
                person.LastName = txt_ho.Text;
                person.Age = Convert.ToInt32(txt_tuoi.Text);
                person.Id = ObjectId.GenerateNewId();
                collection.InsertOne(person);
            }
            //edit
            else
            {
                var update = Builders<Person>.Update.Set("FirstName",txt_name.Text).Set("LastName",txt_ho.Text).Set("Age", Convert.ToInt32(txt_tuoi.Text));
                collection.UpdateOne(person =>person.Id == ObjectId.Parse(Id), update);
            }
           
           
            LoadData();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            Save();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Id = gridView1.GetFocusedRowCellValue("Id").ToString();
            loadPerson();
        }
    }
}
