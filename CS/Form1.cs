using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
// ...

namespace DifferentBrickTypes {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            BrickGraphics gr = printingSystem1.Graph;
            BrickStringFormat bsf = new BrickStringFormat(StringAlignment.Near, StringAlignment.Center);
            gr.StringFormat = bsf;
            gr.BorderColor = SystemColors.ControlDark;

            // Declare bricks.
            ImageBrick imagebrick;
            TextBrick textbrick;
            CheckBoxBrick checkbrick;
            Brick brick;
            PageInfoBrick pageinfobr;
            PageImageBrick pageimagebr;

            // Declare text strings.
            string[] rows = { "Species No:", "Length (cm):", "Category:", "Common Name:", "Species Name:" },
                desc = { "90070", "30", "Angelfish", "Blue Angelfish", "Pomacanthus nauarchus" };

            string note = "Habitat is around boulders, caves, coral ledges and crevices in shallow waters. " +
                "Swims alone or in groups. Its color changes dramatically from juvenile to adult. The mature " +
                "adult fish can startle divers by producing a powerful drumming or thumping sound intended " +
                "to warn off predators. Edibility is good. Range is the entire Indo-Pacific region.",
                devexpress = "XtraPrintingSystem by Developer Express Inc.";

            // Define the images to display.
            Image img = Image.FromFile(@"..\..\angelfish.png"), pageimage = Image.FromFile(@"..\..\logo.png");

            printingSystem1.PageSettings.Landscape = false;

            printingSystem1.Begin();

            // Detail section creation.
            gr.Modifier = BrickModifier.Detail;

            // Start creation of a non-separable group of bricks.
            gr.BeginUnionRect();

            // Display the image.
            imagebrick = gr.DrawImage(img, new RectangleF(0, 0, 250, 150), BorderSide.All, Color.Transparent);
            imagebrick.Hint = "Blue Angelfish";
            textbrick = gr.DrawString("1", Color.Blue, new RectangleF(5, 5, 30, 15), BorderSide.All);
            textbrick.StringFormat = textbrick.StringFormat.ChangeAlignment(StringAlignment.Center);

            // Display a checkbox.
            checkbrick = gr.DrawCheckBox(new RectangleF(5, 145, 10, 10), BorderSide.All, Color.White, true);

            // Create a set of bricks, representing a column with species names.
            gr.BackColor = Color.FromArgb(153, 204, 255);
            gr.Font = new Font("Arial", 10, FontStyle.Italic | FontStyle.Bold | FontStyle.Underline);
            for(int i = 0; i < 5; i++) {

                // Draw a VisualBrick representing borders for the following TextBrick.
                brick = gr.DrawRect(new RectangleF(256, 32 * i, 120, 32), BorderSide.All,
                    Color.Transparent, Color.Empty);

                // Draw the TextBrick with species names.
                textbrick = gr.DrawString(rows[i], Color.Black, new RectangleF(258, 32 * i + 2, 116, 28),
                    BorderSide.All);
            }

            // Create a set of bricks representing a column with the species characteristics.
            gr.Font = new Font("Arial", 11, FontStyle.Bold);
            gr.BackColor = Color.White;
            for(int i = 0; i < 5; i++) {
                brick = gr.DrawRect(new RectangleF(376, 32 * i, gr.ClientPageSize.Width - 376, 32),
                    BorderSide.All,
                Color.Transparent, gr.BorderColor);

                // Draw a TextBrick with species characteristics.
                textbrick = gr.DrawString(desc[i], Color.Indigo, new RectangleF(378, 32 * i + 2,
                    gr.ClientPageSize.Width - 380, 28),
                BorderSide.All);

                // For text bricks containing numeric data, set text alignment to Far.
                if(i < 2)
                    textbrick.StringFormat = textbrick.StringFormat.ChangeAlignment(StringAlignment.Far);
            }

            // Drawing the TextBrick with notes.
            gr.Font = new Font("Arial", 8);
            gr.BackColor = Color.Cornsilk;
            textbrick = gr.DrawString(note, Color.Black, new RectangleF(new PointF(0, 160), new
                SizeF(gr.ClientPageSize.Width, 40)), BorderSide.All);
            textbrick.StringFormat = textbrick.StringFormat.ChangeLineAlignment(StringAlignment.Near);
            textbrick.Hint = note;

            // Finish the creation of a non-separable group of bricks.
            gr.EndUnionRect();

            // Create a MarginalHeader section.
            gr.Modifier = BrickModifier.MarginalHeader;
            RectangleF r = RectangleF.Empty;
            r.Height = 20;
            gr.BackColor = Color.White;

            // Display the DevExpress text string.
            SizeF sz = gr.MeasureString(devexpress);
            pageinfobr = gr.DrawPageInfo(PageInfo.None, devexpress, Color.Black, new RectangleF(new
                PointF(343 - (sz.Width - pageimage.Width) / 2, pageimage.Height + 3), sz), BorderSide.None);
            pageinfobr.Alignment = BrickAlignment.Center;

            // Display the PageImageBrick containing the Developer Express logo.
            pageimagebr = gr.DrawPageImage(pageimage, new RectangleF(343, 0, pageimage.Width, pageimage.Height),
                BorderSide.None, Color.Transparent);
            pageimagebr.Alignment = BrickAlignment.Center;

            // Display the PageInfoBrick containing date-time information. Date-time information is displayed
            // in the left part of the MarginalHeader section using the FullDateTimePattern.
            pageinfobr = gr.DrawPageInfo(PageInfo.DateTime, "{0:F}", Color.Black, r, BorderSide.None);
            pageinfobr.Alignment = BrickAlignment.Near;

            // Display the PageInfoBrick containing the page number among total pages. The page number
            // is displayed in the right part of the MarginalHeader section.
            pageinfobr = gr.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", Color.Black, r,
                 BorderSide.None);
            pageinfobr.Alignment = BrickAlignment.Far;

            printingSystem1.End();
            printingSystem1.PreviewFormEx.Show();
        }

    }
}