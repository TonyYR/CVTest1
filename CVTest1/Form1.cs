using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

namespace CVTest1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = @"D:\";
            ofd.Filter = "jpgファイル(*.jpg)|*.jpg|pngファイル(*.png)|*.png";
            ofd.FilterIndex = 1;
            ofd.Title = "Fileを選択してください";
            ofd.CheckFileExists = true;
            ofd.RestoreDirectory = true;
            ofd.CheckPathExists = true;

            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Mat src = new Mat(ofd.FileName, ImreadModes.Grayscale);
            // 2値化画像
            Mat dst = src.Clone();
            // 2値化
            Cv2.Threshold(src, dst, 0, 255, ThresholdTypes.Otsu);

            // Formに追加したPicture Boxと同じサイズではまるBit Mapを作成
            Bitmap canvasDst = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            Bitmap canvasSrc = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);

            // canvasオブジェクト上のImageを操作するためにGraphicsオブジェクトを取得
            Graphics g1 = Graphics.FromImage(canvasDst);
            Graphics g2 = Graphics.FromImage(canvasSrc);

            g1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            // 表示する画像
            Bitmap bitimgdst = MatToBitMap(dst);
            Bitmap bitimgsrc = MatToBitMap(src);
            g1.DrawImage(bitimgdst, 0, 0, 225, 225);
            g2.DrawImage(bitimgsrc, 0, 0, 225, 225);

            bitimgdst.Dispose();
            g1.Dispose();
            g2.Dispose();
            // Picture Boxに紐づけ
            this.pictureBox1.Image = canvasDst;
            this.pictureBox2.Image = canvasSrc;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Bitmap canvas1 = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            Bitmap canvas2 = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.pictureBox1.Image = canvas1;
            this.pictureBox2.Image = canvas2;
        }

        // GUI上に表示するにはOpen CV上で扱うMat形式をBitMap形式に変換する必要がある
        public static Bitmap MatToBitMap(Mat Image)
        {
            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(Image);
        }

    }
}
