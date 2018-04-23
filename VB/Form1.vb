Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraPrinting
' ...

Namespace DifferentBrickTypes
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			Dim gr As BrickGraphics = printingSystem1.Graph
			Dim bsf As New BrickStringFormat(StringAlignment.Near, StringAlignment.Center)
			gr.StringFormat = bsf
			gr.BorderColor = SystemColors.ControlDark

			' Declare bricks.
			Dim imagebrick As ImageBrick
			Dim textbrick As TextBrick
			Dim checkbrick As CheckBoxBrick
			Dim brick As Brick
			Dim pageinfobr As PageInfoBrick
			Dim pageimagebr As PageImageBrick

			' Declare text strings.
			Dim rows() As String = { "Species No:", "Length (cm):", "Category:", "Common Name:", "Species Name:" }, desc() As String = { "90070", "30", "Angelfish", "Blue Angelfish", "Pomacanthus nauarchus" }

			Dim note As String = "Habitat is around boulders, caves, coral ledges and crevices in shallow waters. " & "Swims alone or in groups. Its color changes dramatically from juvenile to adult. The mature " & "adult fish can startle divers by producing a powerful drumming or thumping sound intended " & "to warn off predators. Edibility is good. Range is the entire Indo-Pacific region.", devexpress As String = "XtraPrintingSystem by Developer Express Inc."

			' Define the images to display.
			Dim img As Image = Image.FromFile("..\..\angelfish.png"), pageimage As Image = Image.FromFile("..\..\logo.png")

			printingSystem1.PageSettings.Landscape = False

			printingSystem1.Begin()

			' Detail section creation.
			gr.Modifier = BrickModifier.Detail

			' Start creation of a non-separable group of bricks.
			gr.BeginUnionRect()

			' Display the image.
			imagebrick = gr.DrawImage(img, New RectangleF(0, 0, 250, 150), BorderSide.All, Color.Transparent)
			imagebrick.Hint = "Blue Angelfish"
			textbrick = gr.DrawString("1", Color.Blue, New RectangleF(5, 5, 30, 15), BorderSide.All)
			textbrick.StringFormat = textbrick.StringFormat.ChangeAlignment(StringAlignment.Center)

			' Display a checkbox.
			checkbrick = gr.DrawCheckBox(New RectangleF(5, 145, 10, 10), BorderSide.All, Color.White, True)

			' Create a set of bricks, representing a column with species names.
			gr.BackColor = Color.FromArgb(153, 204, 255)
			gr.Font = New Font("Arial", 10, FontStyle.Italic Or FontStyle.Bold Or FontStyle.Underline)
			For i As Integer = 0 To 4

				' Draw a VisualBrick representing borders for the following TextBrick.
				brick = gr.DrawRect(New RectangleF(256, 32 * i, 120, 32), BorderSide.All, Color.Transparent, Color.Empty)

				' Draw the TextBrick with species names.
				textbrick = gr.DrawString(rows(i), Color.Black, New RectangleF(258, 32 * i + 2, 116, 28), BorderSide.All)
			Next i

			' Create a set of bricks representing a column with the species characteristics.
			gr.Font = New Font("Arial", 11, FontStyle.Bold)
			gr.BackColor = Color.White
			For i As Integer = 0 To 4
				brick = gr.DrawRect(New RectangleF(376, 32 * i, gr.ClientPageSize.Width - 376, 32), BorderSide.All, Color.Transparent, gr.BorderColor)

				' Draw a TextBrick with species characteristics.
				textbrick = gr.DrawString(desc(i), Color.Indigo, New RectangleF(378, 32 * i + 2, gr.ClientPageSize.Width - 380, 28), BorderSide.All)

				' For text bricks containing numeric data, set text alignment to Far.
				If i < 2 Then
					textbrick.StringFormat = textbrick.StringFormat.ChangeAlignment(StringAlignment.Far)
				End If
			Next i

			' Drawing the TextBrick with notes.
			gr.Font = New Font("Arial", 8)
			gr.BackColor = Color.Cornsilk
			textbrick = gr.DrawString(note, Color.Black, New RectangleF(New PointF(0, 160), New SizeF(gr.ClientPageSize.Width, 40)), BorderSide.All)
			textbrick.StringFormat = textbrick.StringFormat.ChangeLineAlignment(StringAlignment.Near)
			textbrick.Hint = note

			' Finish the creation of a non-separable group of bricks.
			gr.EndUnionRect()

			' Create a MarginalHeader section.
			gr.Modifier = BrickModifier.MarginalHeader
			Dim r As RectangleF = RectangleF.Empty
			r.Height = 20
			gr.BackColor = Color.White

			' Display the DevExpress text string.
			Dim sz As SizeF = gr.MeasureString(devexpress)
			pageinfobr = gr.DrawPageInfo(PageInfo.None, devexpress, Color.Black, New RectangleF(New PointF(343 - (sz.Width - pageimage.Width) / 2, pageimage.Height + 3), sz), BorderSide.None)
			pageinfobr.Alignment = BrickAlignment.Center

			' Display the PageImageBrick containing the Developer Express logo.
			pageimagebr = gr.DrawPageImage(pageimage, New RectangleF(343, 0, pageimage.Width, pageimage.Height), BorderSide.None, Color.Transparent)
			pageimagebr.Alignment = BrickAlignment.Center

			' Display the PageInfoBrick containing date-time information. Date-time information is displayed
			' in the left part of the MarginalHeader section using the FullDateTimePattern.
			pageinfobr = gr.DrawPageInfo(PageInfo.DateTime, "{0:F}", Color.Black, r, BorderSide.None)
			pageinfobr.Alignment = BrickAlignment.Near

			' Display the PageInfoBrick containing the page number among total pages. The page number
			' is displayed in the right part of the MarginalHeader section.
			pageinfobr = gr.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", Color.Black, r, BorderSide.None)
			pageinfobr.Alignment = BrickAlignment.Far

			printingSystem1.End()
			printingSystem1.PreviewFormEx.Show()
		End Sub

	End Class
End Namespace