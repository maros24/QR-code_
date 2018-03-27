using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.Barcode;
using System.Text.RegularExpressions;

namespace qr_code
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       
        BarcodeDecoder Scanner;
        SaveFileDialog SD;
        OpenFileDialog OD;


        private void button1_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox1.Text, @"\p{IsBasicLatin}")&& textBox1.Text != "") 
            {
                string qrtext = textBox1.Text; //считываем текст из TextBox'a
                QRCodeEncoder encoder = new QRCodeEncoder(); //создаём новую "генерацию кода"
                Bitmap qrcode = encoder.Encode(qrtext); // кодируем слово, полученное из TextBox'a (qrtext) в переменную qrcode. класса Bitmap(класс, который используется для работы с изображениями)
                pictureBox1.Image = qrcode as Image; // pictureBox выводит qrcode как изображение.
            }
            else
            {
                MessageBox.Show("Заполните текстовое поле латинскими буквами");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog(); // save будет запрашивать у пользователя место, в которое он захочет сохранить файл. 
                save.Filter = "PNG|*.png|JPEG|*.jpg|GIF|*.gif|BMP|*.bmp"; //создаём фильтр, который определяет, в каких форматах мы сможем сохранить нашу информацию. В данном случае выбираем форматы изображений. Записывается так: "название_формата_в обозревателе|*.расширение_формата")
                if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK) //если пользователь нажимает в обозревателе кнопку "Сохранить".
                {
                    pictureBox1.Image.Save(save.FileName); //изображение из pictureBox'a сохраняется под именем, которое введёт пользователь
                }
                pictureBox1.Image = null;
                textBox1.Clear();
            }
            catch
            {
                MessageBox.Show("Сгенерируйте штрихкод");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
                OpenFileDialog load = new OpenFileDialog(); //  load будет запрашивать у пользователя место, из которого он хочет загрузить файл.
                if (load.ShowDialog() == System.Windows.Forms.DialogResult.OK) // //если пользователь нажимает в обозревателе кнопку "Открыть".
                {
                    pictureBox1.ImageLocation = load.FileName; // в pictureBox'e открывается файл, который был по пути, запрошенном пользователем.
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                QRCodeDecoder decoder = new QRCodeDecoder(); // создаём "раскодирование изображения"
                MessageBox.Show(decoder.decode(new QRCodeBitmapImage(pictureBox1.Image as Bitmap))); //в MessageBox'e программа запишет раскодированное сообщение с изображения, которое предоврительно будет переведено из pictureBox'a в класс Bitmap, чтобы мы смогли с этим изображением работать. 
                pictureBox1.Image = null;
            }
            catch
            {
                MessageBox.Show("Загрузите изображение с QR кодом!");
                pictureBox1.Image = null;
            }
        }



        private void scan_bar_Click(object sender, EventArgs e)
        {
            try
            {
                Scanner = new BarcodeDecoder();
                Result result = Scanner.Decode(new Bitmap(pictureBox2.Image));
                MessageBox.Show(result.Text);
            }
            catch
            {
                MessageBox.Show("Загрузите  ШТРИХКОД");
            }
        }

        private void generate_bar_Click(object sender, EventArgs e)
        {
            try 
            {
                BarcodeEncoder barcodeEncoder = new BarcodeEncoder();
                pictureBox2.Image = new Bitmap(barcodeEncoder.Encode(BarcodeFormat.Code128, textBox2.Text)); 
            }
            catch
            {
                  MessageBox.Show("Заполните текстовое поле латинской литерацией");
            }  
        }

        private void save_bar_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    SD = new SaveFileDialog();
                    SD.Filter = "PNG|*.png|JPEG|*.jpg|GIF|*.gif|BMP|*.bmp";
                    if (SD.ShowDialog() == DialogResult.OK)
                        pictureBox2.Image.Save(SD.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    pictureBox2.Image = null;
                    textBox2.Clear();
                }
            }
            catch
            {
                MessageBox.Show("Сгенерируйте штрихкод");
            }
        }

        private void load_bar_Click(object sender, EventArgs e)
        {
            try
            {
                OD = new OpenFileDialog();
                OD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (OD.ShowDialog() == DialogResult.OK)
                    pictureBox2.Load(OD.FileName);
            }
            catch
            {
                MessageBox.Show("Выберите файл с расширениями .png, .jpg, .gif, .bmp ");
            }
        }
    }
}
