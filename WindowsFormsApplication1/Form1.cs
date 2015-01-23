using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public string input_filename;
        public List<string> csv_columns = new List<string>();
        public List<Dictionary<String, String>> data = new List<Dictionary<String, String>>();

        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if(result == DialogResult.OK){
                input_filename = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label3.Text = "";

            if (input_filename != null)
            {
                openOldFile();
                saveFile();
                label3.Text = "Complete";

            }
            else {
                MessageBox.Show("Input file is not selected");
            }
        }

        private void openOldFile() {
            csv_columns = new List<string>();
            data = new List<Dictionary<String, String>>();

            var old_delimiter = textBox1.Text;

            var reader = new StreamReader(File.OpenRead(input_filename));

            bool firstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                line = line.Replace(old_delimiter,"#");
                var values = line.Split('#');

                if (firstLine)
                {
                    firstLine = false;
                    for (int i = 0; i < values.Length; i++) {
                        csv_columns.Add(values[i]);
                        Console.Write(values[i] + "-");
                    }
                    Console.WriteLine();
                }
                else { 
                    var tmp_dictionary = new Dictionary<String, String>();
                    int st = 0;

                    csv_columns.ForEach(col => {
                        tmp_dictionary.Add(col, values[st].Replace(",","."));
                        st++;
                    });
                    data.Add(tmp_dictionary);
                }
            }
        }

        private void saveFile() {
            DialogResult result = saveFileDialog1.ShowDialog();
            if(result == DialogResult.OK){
                string new_delimiter = textBox2.Text;

                StringBuilder sb = new StringBuilder();
                data.ForEach(item => {
                    if(item["name"]==this.comboBox1.SelectedValue.ToString()){
                        var tmp_data = new List<String>(){
                            item["value0"],
                            item["value1"],
                            item["value2"]
                        };
                        sb.AppendLine(string.Join(new_delimiter, tmp_data)); 
                    }
                });

                File.WriteAllText(saveFileDialog1.FileName, sb.ToString()); 
                
            }            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var dataSource = new List<String>();
            dataSource.Add("MPL Gravity");
            dataSource.Add("MPL Accelerometer");
            this.comboBox1.DataSource = dataSource;
        }
    }
}
