using System.Windows.Forms;
using System.IO;
using System;
using System.Numerics;
namespace Sum
{

    /*
    Extensibility
    a.  This program could be written to read from a database instead of a CSV file. You could just replace
        the file access /  file processing code section with code that links to a database and reads through entries
    b.  This program does allow for many large numbers to be procesed. I use the BigInteger from the System.Numerics package
        because it is specifically used for massive numbers, ones that are too big to assign to int or even to long data types.
    c.  This file could be pretty easy to adjust, although in its current state it is specifically constructed to work with the 
        example CSV file that was provided. The lines were split into an array and the apostrophe's were forcibly removed. If a 
        different file format was being used then it would need to be accounted for. Such as delimination of commas rather than apostrophes.
    Thoughts
    1.  This could be made faster by having the data already be in the correct format rather than requiring some string manipulation
    2.  This program could work with negatives. It is unknown if it SHOULD be working with negatives because what the data is being used for is unknown.
    3.  Yes the entire CSV file is being help in active memory. This could cause problems with large loads because it is an impractical way to store data in a large scale.
        The computer running the program would have to process the file rather than accessing the information through a database.
    4.  The memory usage of the application grows with the larger amount of data within the file. This could be prevented by linking to an external database instead of uploading a file.
    5.  Yes, there is a lot of unnessary information being processed through string manipulation, conversion, and some math. 
    6.  Pretty much any information that is not in the correct format could destroy this program, since i wrote it specifically to process the data that was formatted in the CSV file.
        A few things i did to accomadate this are requiring the file uploaded to be a CSV type. As well as checking for the file and file path to exist. 
        There is also a parse check to make sure that the conversion is actually possible, otherwise the line is skipped.
        Finally, there is an input/output exception check.
     
    
    
     
     */
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Open a CSV File";
            fDialog.Filter = "CSV Files| *.csv";
            fDialog.InitialDirectory = @"C:\";
            fDialog.AddExtension = true;
            fDialog.CheckFileExists = true;
            fDialog.CheckPathExists = true;
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                string file = fDialog.FileName;
                try
                {
                    string text = File.ReadAllText(file);
                    text = text.Replace("\'", "");
                    string[] numbers = text.Split('\n');
                    BigInteger result = 0;
                    BigInteger number = BigInteger.Zero;
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        if (BigInteger.TryParse(numbers[i], System.Globalization.NumberStyles.Integer,
                                                null, out number))
                        {
                            result += BigInteger.Parse(numbers[i]);

                        }
                    }
                    textBox1.Text = result.ToString().Substring(result.ToString().Length - 10);
                }
                catch (IOException)
                {

                }
            }
        }
    }
}