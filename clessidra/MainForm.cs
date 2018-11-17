using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing.Drawing2D;

namespace Blue_Screen_saver
{
    public partial class MainForm : Form
    {        
        
        #region Preview API's

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        #endregion

        bool IsPreviewMode = false;

        public int Liv = 1;
        public int intnum = 1;
        public bool blnExit = false;
        public Graphics g = null;
        public int pintMinuti = 0;
        public int pintSecondi = 0;
        public List<Rectangle> LstRectangle;
        public int intRectangleDaCancellare = 0;
        public int intSemaforo = 0;
        public bool blnRuotazione = false;


        #region Constructors

        public MainForm()
        {
            InitializeComponent();
        }

       
        public MainForm(Rectangle Bounds)
        {
            InitializeComponent();
            this.Bounds = Bounds;
           
            Cursor.Hide();
        }

        
        public MainForm(IntPtr PreviewHandle)
        {
            InitializeComponent();
            SetParent(this.Handle, PreviewHandle);
            SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

            Rectangle ParentRect;
            GetClientRect(PreviewHandle, out ParentRect);
            this.Size = ParentRect.Size;

            
            this.Location = new Point(0, 0);

            IsPreviewMode = true;
        }

        #endregion

        #region GUI

        
        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (!IsPreviewMode) 
            {
                this.Refresh();
                System.Threading.Thread.Sleep(1000);
            }
            
            this.BackColor = Color.FromArgb(0, 0, 130);


            blnExit = false;
            AvvioClessidra(intSemaforo);


        }


        // Disegna Rettangoli Parte Superiore della Clessidra
        private void DisegnaRettangoliParteSuperioreClessidra(Graphics g, int x, int y, int intNumerorettangoli,int intSemaforo)
        {
            LstRectangle = new List<Rectangle>();
            System.Drawing.Color col = Color.AliceBlue;
            switch (intSemaforo)
            {
                case 0:
                    col = Color.Red;
                    break;
                case 1:
                    col = Color.LightGreen;
                    break;
                case 2:
                    col = Color.Orange;
                    break;
            }
            
            System.Drawing.SolidBrush myBrush_SkyB = new System.Drawing.SolidBrush(col);
            System.Drawing.SolidBrush myBrush_B = new System.Drawing.SolidBrush(Color.Black);

            for (int i = 10; i < intNumerorettangoli - i; i += 10)
            {
                for (int j = i; j <= intNumerorettangoli - i; j += 10)
                {
                    Rectangle rectY = new Rectangle(x + j, y + i, 8, 8);
                    g.FillRectangle(myBrush_SkyB, rectY);
                    LstRectangle.Add(rectY);
                }
            }

            tmrSecondi.Enabled = true;

            pictureBox1.Invalidate();
        }

        // Caduta Rettangolo Centrale.
        private void CadutaRettangolo(Graphics g, int x, int y, int Down, int Len,int intSemaforo)
        {

            if (Down == 0)
                return;
            System.Drawing.SolidBrush myBrush_B = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

            System.Drawing.Color col = Color.AliceBlue;
            switch (intSemaforo)
            {
                case 0:
                    col = Color.Red;
                    break;
                case 1:
                    col = Color.LightGreen;
                    break;
                case 2:
                    col = Color.Orange;
                    break;
            }
            
            System.Drawing.SolidBrush myBrush_R = new System.Drawing.SolidBrush(col);

            for (int i = 10; i < Len; i += 10)
            {

                Rectangle rect = new Rectangle(x, y + (i - 10), 8, 8);
                g.FillRectangle(myBrush_B, rect);

                Thread.Sleep(1);

                Rectangle rect1 = new Rectangle(x, y + i, 8, 8);
                g.FillRectangle(myBrush_R, rect1);

                Application.DoEvents();
                pictureBox1.Invalidate();
            }

            

            if (intRectangleDaCancellare < LstRectangle.Count)
            {
               
                Rectangle rct = LstRectangle[intRectangleDaCancellare++];
                g.FillRectangle(myBrush_B, rct);
            }

            pictureBox2.Invalidate();

        }

        // Caduta Rettangolo Successivo Destro.
        private void SetRettangoloSuccessivoD(Graphics g, int x, int y, int Down, int Len, int intSemaforo)
        {

            if (Down == 0)
                return;

            System.Drawing.SolidBrush myBrush_B = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            System.Drawing.Color col = Color.AliceBlue;
            switch (intSemaforo)
            {
                case 0:
                    col = Color.Red;
                    break;
                case 1:
                    col = Color.LightGreen;
                    break;
                case 2:
                    col = Color.Orange;
                    break;
            }
            

            System.Drawing.SolidBrush myBrush_R = new System.Drawing.SolidBrush(col);


            int intTotalePassi = Len - y;

            for (int i = 0; i <= intTotalePassi; i += 10)
            {
                if (i == 0)
                {
                    Rectangle rect1 = new Rectangle(x, y, 8, 8);
                    g.FillRectangle(myBrush_B, rect1);
                }
                else
                {

                    Rectangle rect1 = new Rectangle(x - (i - 10), y + (i - 10), 8, 8);
                    g.FillRectangle(myBrush_B, rect1);
                    
                }

                Thread.Sleep(2);

                Rectangle rect2 = new Rectangle(x - i, y + i, 8, 8);
                g.FillRectangle(myBrush_R, rect2);
                
                Application.DoEvents();
                pictureBox1.Invalidate();
               
            }

            

        }


        // Caduta Rettangolo Successivo Sinistro.
        private void SetRettangoloSuccessivoS(Graphics g, int x, int y, int Down, int Len, int intSemaforo)
        {

            if (Down == 0)
                return;
            System.Drawing.SolidBrush myBrush_B = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

            System.Drawing.Color col = Color.AliceBlue;
            switch (intSemaforo)
            {
                case 0:
                    col = Color.Red;
                    break;
                case 1:
                    col = Color.LightGreen;
                    break;
                case 2:
                    col = Color.Orange;
                    break;
            }
            

            System.Drawing.SolidBrush myBrush_R = new System.Drawing.SolidBrush(col);


            int intTotalePassi = Len - y;

            for (int i = 0; i <= intTotalePassi; i += 10)
            {
                if (i == 0)
                {
                    Rectangle rect1 = new Rectangle(x, y, 8, 8);
                    g.FillRectangle(myBrush_B, rect1);
                    
                }
                else
                {

                    Rectangle rect1 = new Rectangle(x + (i - 10), y + (i - 10), 8, 8);
                    g.FillRectangle(myBrush_B, rect1);

                }

                Thread.Sleep(2);

                Rectangle rect2 = new Rectangle(x + i, y + i, 8, 8);
                g.FillRectangle(myBrush_R, rect2);

                Application.DoEvents();
                pictureBox1.Invalidate();                
               
            }

        }


        private void AvvioClessidra(int intSemaforo)
        {
            // Setta Sfondo Nero All'avvio del Gioco.
            this.BackColor = Color.Black;
            pictureBox1.Image = null;

            // Setta L'immagine Della Clessidra.
            Image img = Image.FromFile("clessidra.bmp");
            Bitmap bmOriginal = new Bitmap(img.Width, img.Height);

            // Setta il Contenitore Grafico.
            using (g = Graphics.FromImage(bmOriginal))
            {

                // Disegna Clessidra.
                g.DrawImage(img, 0, 0);
                pictureBox1.Image = bmOriginal;
                pictureBox1.Invalidate();
                Application.DoEvents();

                blnRuotazione = true;

                Thread.Sleep(20);

                // Ruota Immagine 2 Cicli di 90°
                bmOriginal.RotateFlip(RotateFlipType.Rotate90FlipY);
                pictureBox1.Image = bmOriginal;
                pictureBox1.Invalidate();
                Application.DoEvents();

                Thread.Sleep(20);

                bmOriginal.RotateFlip(RotateFlipType.Rotate90FlipY);
                pictureBox1.Image = bmOriginal;
                pictureBox1.Invalidate();
                Application.DoEvents();

                Thread.Sleep(20);

                img.Dispose();

                // Traccia Linea alla Fine della Piramide 
                Pen myPen = new Pen(Color.Blue, 2);
                myPen.DashStyle = DashStyle.Solid;
                g.DrawLine(myPen, 100, 690, 485, 690);
                myPen.Dispose();


                // Inizio Algoritmo...
                int intMoltiplicatore = 2;
                int intLivello = 1;
                int intX = (Screen.PrimaryScreen.WorkingArea.Width / 4) - 50;
                int intY = 400;//(Screen.PrimaryScreen.WorkingArea.Height / 2);//10
                int intLen = 290;                                              
                int intDown = 300;
                int intNumeroMattoncini = 100;

                switch (intSemaforo)
                {
                    case 0:
                        intNumeroMattoncini = 220;
                        break;
                    case 1:
                        intNumeroMattoncini = 220;
                        break;
                    case 2:
                        intNumeroMattoncini = 220;
                        break;
                }

                // Disegna Mattoncini Parte superiore Clessidra.
                DisegnaRettangoliParteSuperioreClessidra(g, 180, 290, intNumeroMattoncini, intSemaforo);

                System.Drawing.SolidBrush SBrsOrange = new System.Drawing.SolidBrush(Color.Orange);

                blnRuotazione = false;

                // Totale Numero di Mattonici 
                for (int i = 1; i <= 100; )
                {
                    if (intSemaforo == 1 && i == 50)
                    {
                        intSemaforo = 2;

                        for (int idx = intRectangleDaCancellare; idx <= LstRectangle.Count; idx++)
                        {

                            Rectangle rct = LstRectangle[idx];
                            g.FillRectangle(SBrsOrange, rct);
                        }
                       
                    }

                    if (i == 1)
                        CadutaRettangolo(g, intX, intY, intDown, intLen, intSemaforo);
                    else
                        CadutaRettangolo(g, intX, intY, intDown, intLen - (10 * (intLivello - 1)), intSemaforo);

                    int intLivello_tmp = 1;
                    int intX_Tmp = 0;
                    int intY_Tmp = 0;
                    int intMoltTemp = intMoltiplicatore - 2;
                    if (intMoltTemp == 0) intMoltTemp = 1;
                    int intPasso = 0;
                    int intLivelloLast = intMoltiplicatore / 2;

                    for (int j = 1; j <= intMoltiplicatore; j++)
                    {
                        // Variabile di Uscita Alla Chiusura della Form.
                        if (blnExit == true)
                        {
                            tmrSecondi.Enabled = false;
                            break;
                        }

                        i++;
                        if ((j % 2) == 0)
                        {
                            intX_Tmp = intX + (10 * (intLivelloLast));
                            intY_Tmp = intY;

                            CadutaRettangolo(g, intX, intY, intDown, intLen - (10 * (intLivello - 1)), intSemaforo);

                            SetRettangoloSuccessivoS(g, intX, intY_Tmp + intLen - (10 * (intLivello + 1)), intDown, intY_Tmp + (intLen - intPasso) - 10, intSemaforo);

                            if (j == intMoltiplicatore) break;

                            intMoltTemp -= 2;
                            if (intMoltTemp == -1 || intMoltTemp == 0) intMoltTemp = 1;

                            intPasso = 10 * intLivello_tmp;
                            intLivello_tmp++;
                            intLivelloLast--;
                        }
                        else
                        {
                            intX_Tmp = intX - (10 * (intLivelloLast));
                            intY_Tmp = intY;

                            CadutaRettangolo(g, intX, intY, intDown, intLen - (10 * (intLivello - 1)), intSemaforo);

                            SetRettangoloSuccessivoD(g, intX, intY_Tmp + intLen - (10 * (intLivello + 1)), intDown, intY_Tmp + (intLen - intPasso) - 10, intSemaforo);
                        }

                        Application.DoEvents();

                        if (intSemaforo == 1 && i == 50)
                        {
                            intSemaforo = 2;

                            for (int idx = intRectangleDaCancellare; idx < LstRectangle.Count; idx++)
                            {

                                Rectangle rct = LstRectangle[idx];
                                g.FillRectangle(SBrsOrange, rct);
                            }
                        }
                    }

                    if (blnExit == true)
                    {
                        tmrSecondi.Enabled = false;
                        break;
                    }

                    intLivello++;
                    intMoltiplicatore += 2;

                    if (i > 100)
                    {
                        CadutaRettangolo(g, intX, intY, intDown, intLen - (10 * (intLivello - 1)), intSemaforo);
                    }

                    Application.DoEvents();
                }

                g.Dispose();
            }

        }

        #endregion

        #region User Input

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsPreviewMode) //disable exit functions for preview
            {
                tmrSecondi.Enabled = false;
                blnExit = true;
                Application.Exit();

            }
        }

        private void MainForm_Click(object sender, EventArgs e)
        {
            if (!IsPreviewMode) //disable exit functions for preview
            {
                tmrSecondi.Enabled = false;
                blnExit = true;
                Application.Exit();
            }
        }

        Point OriginalLocation = new Point(int.MaxValue, int.MaxValue);

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsPreviewMode) 
            {
                
                if (OriginalLocation.X == int.MaxValue & OriginalLocation.Y == int.MaxValue)
                {
                    OriginalLocation = e.Location;
                }
                
                if (Math.Abs(e.X - OriginalLocation.X) > 20 | Math.Abs(e.Y - OriginalLocation.Y) > 20)
                {
                    Application.Exit();
                }
            }
        }


        private void tmrSecondi_Tick(object sender, EventArgs e)
        {

            if (pictureBox2.Image == null)
            {
                pictureBox2.Image = new Bitmap(pictureBox2.Width,pictureBox2.Height);
            }

            Graphics g2 = Graphics.FromImage(pictureBox2.Image);
            System.Drawing.SolidBrush myBrush_Red = new System.Drawing.SolidBrush(Color.SkyBlue);
            System.Drawing.SolidBrush myBrush_White = new System.Drawing.SolidBrush(Color.Black);
            String strSecondi = string.Format("{0}:{1}", pintMinuti.ToString("00"), pintSecondi.ToString("00"));
            Font drawFont = new Font("Courier New", 50);
            g2.DrawString(strSecondi, drawFont, myBrush_White, 10, 10);
            g2.Clear(Color.Black);
            g2.DrawString(strSecondi, drawFont, myBrush_Red, 10, 10);
            pictureBox2.Invalidate();

            g2.Dispose();


            if (pictureBox3.Image == null)
            {
                pictureBox3.Image = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            }
            const string strTesto = "By Tatanka";            
            Graphics gfx = Graphics.FromImage(pictureBox3.Image);
            gfx.TextRenderingHint =
                System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            gfx.Clear(this.BackColor);
            // Make a font.
            using (Font the_font = new Font("Times New Roman", 50,
            FontStyle.Bold, GraphicsUnit.Pixel))
            gfx.DrawString(strTesto, the_font, myBrush_Red, 10, 10);
            pictureBox2.Invalidate();

            gfx.Dispose();

            if (blnRuotazione == false)
            {
                try
                {
                    int intDiff = LstRectangle.Count - intRectangleDaCancellare;
                    String strSecondi_Tmp = string.Format("{0}", pintSecondi.ToString("0"));
                    Font _drawFont = new Font("Courier New", 60);

                    Rectangle rectTesto = new Rectangle(250, 150, 160, 100);
                    //g.DrawRectangle(Pens.Black, rect);
                    g.FillRectangle(myBrush_White, rectTesto);

                    g.DrawString(strSecondi_Tmp, _drawFont, myBrush_Red, 250, 150);
                    pictureBox2.Invalidate();
                }
                catch (Exception Ex)
                {
                    
                }
            }
            
            if (intRectangleDaCancellare >= LstRectangle.Count)
            {

                LstRectangle.Clear();
                blnExit = false;
                intRectangleDaCancellare = 0;
                tmrSecondi.Enabled = false;
                pictureBox1.Image = null;
                if (intSemaforo == 1)
                    intSemaforo = 0;
                else
                    intSemaforo += 1;

                AvvioClessidra(intSemaforo);
            }

            if (pintSecondi == 59)
            {
                pintMinuti += 1;
                pintSecondi = 0;
            }
            pintSecondi++;

            Application.DoEvents();
        }

        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!IsPreviewMode) //disable exit functions for preview
            {
                tmrSecondi.Enabled = false;
                blnExit = true;
                LstRectangle.Clear();
                Application.Exit();
            }

        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_Load(object sender , EventArgs e)
        {

        }
    }
}
