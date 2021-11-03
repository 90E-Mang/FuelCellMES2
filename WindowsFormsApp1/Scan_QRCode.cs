using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;

namespace WindowsFormsApp1
{
    public partial class Scan_QRCode : Form
    {
        static public string Qrcode = "";

        public Scan_QRCode()
        {
            InitializeComponent();
        }
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice captureDevice;

        private void Scan_QRCode_Load(object sender, EventArgs e)
        {
            try
            {
                filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                foreach (FilterInfo filterInfo in filterInfoCollection)
                {
                    cboDevice.Items.Add(filterInfo.Name);
                }
                cboDevice.SelectedIndex = 0;

                captureDevice = new VideoCaptureDevice(filterInfoCollection[cboDevice.SelectedIndex].MonikerString);
                captureDevice.NewFrame += captureDevice_NewFrame;
                captureDevice.Start();
                timer1.Start();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void captureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                pictureBox.Image = (Bitmap)eventArgs.Frame.Clone();

            }
            catch (System.InvalidOperationException)
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Scan_QRCode_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (captureDevice.IsRunning)
                {
                    captureDevice.Stop();
                }
            }
            catch (System.NullReferenceException)
            {

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox.Image != null)
                {
                    BarcodeReader barcodeReader = new BarcodeReader();
                    Result result = barcodeReader.Decode((Bitmap)pictureBox.Image);

                    if (result != null)
                    {
                        Qrcode = result.ToString();
                        //textQRCode.Text = result.ToString();
                        timer1.Stop();

                        if (captureDevice.IsRunning)
                        {
                            captureDevice.Stop();
                        }
                        this.Close();
                    }
                }
            }
            catch (System.NullReferenceException)
            {

            }
            catch (Exception)
            {

                throw;
            }
            
        }

    }
}
