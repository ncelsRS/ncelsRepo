using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.Words;
using Aspose.Words.Drawing;

namespace Ncels.Helpers
{
    public static class AsposeWordsHelper
    {
        public static void ReplaceText(this Aspose.Words.Document doc, Dictionary<string, string> templateItems)
        {
            if (templateItems == null) return;
            foreach (var templateItem in templateItems)
            {
                doc.Range.Replace(string.Format("{{{{{0}}}}}", templateItem.Key), templateItem.Value ?? "", false, false);
            }
        }
        public static void InserQrCodes(this Document document, string placeHolder, string barCodeData)
        {
            Regex regex = new Regex(string.Format("{{{{{0}}}}}", placeHolder), RegexOptions.IgnoreCase);
            int maxStrLength = 800;
            FindMatchedNodes searchResult = new FindMatchedNodes();
            document.Range.Replace(regex, searchResult, false);
            foreach (Node node in searchResult.nodes)
            {
                var qrCodeHolder = new Paragraph(document);
                if (!string.IsNullOrEmpty(barCodeData))
                {
                    int maxImagesCount = barCodeData.Length / maxStrLength;
                    int imageIndex = 0;
                    for (int index = 0; index < barCodeData.Length; index += maxStrLength)
                    {
                        var qrCodeText =
                            barCodeData.Substring(index, Math.Min(maxStrLength, barCodeData.Length - index));
                        BarCodeBuilder barCodeBuilder = new BarCodeBuilder();
                        barCodeBuilder.SymbologyType = Symbology.QR;
                        barCodeBuilder.CodeText = qrCodeText;
                        barCodeBuilder.CodeLocation = CodeLocation.None;
                        barCodeBuilder.GraphicsUnit = GraphicsUnit.Pixel;
                        barCodeBuilder.Margins.Set(0);
                        // Allows to set size for whole picture with barcode inside and Save image on local disk
                        Bitmap bitmap = barCodeBuilder.GetCustomSizeBarCodeImage(new Size(150, 150), false);
                        MemoryStream img = new MemoryStream();
                        bitmap.Save(img, ImageFormat.Png);
                        img.Position = 0;
                        Shape image = new Shape(document, ShapeType.Image);
                        image.ImageData.SetImage(img);
                        image.Left = 0;
                        image.Top = 0;
                        image.Width = (double)image.ImageData.ImageSize.WidthPixels * 72 / 96;
                        image.Height = (double)image.ImageData.ImageSize.HeightPixels * 72 / 96;
                        image.DistanceLeft = 0;
                        image.DistanceRight = 0;
                        image.WrapType = WrapType.Inline;
                        qrCodeHolder.AppendChild(image);
                        if (imageIndex < maxImagesCount)
                            qrCodeHolder.AppendChild(new Run(document, "  "));
                        imageIndex++;
                    }
                    node.ParentNode.ParentNode.InsertAfter(qrCodeHolder, node.ParentNode);
                }
                node.ParentNode.Remove();
            }
        }

        public static void InserQrCodesToEnd(this Document document, string placeHolder, string barCodeData)
        {
            var builder = new DocumentBuilder(document);
            builder.MoveToDocumentEnd();
            var linkH = builder.InsertParagraph();
            linkH.ParagraphFormat.ClearFormatting();
            linkH.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            linkH.ParagraphFormat.Style.Font.Size = 12;
            var run = new Run(document, string.Format("{{{{{0}}}}}", placeHolder));
            linkH.AppendChild(run);
            document.InserQrCodes(placeHolder, barCodeData);
        }


        public static void InsertDocUrl(this Aspose.Words.Document document, string url)
        {
            var builder = new DocumentBuilder(document);
            builder.MoveToDocumentEnd();
            var linkH = builder.InsertParagraph();
            linkH.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            linkH.AppendChild(new Run(document, "Ссылка на оригинал"));
            var linkP = builder.InsertParagraph();
            linkP.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            BarCodeBuilder barCodeBuilder = new BarCodeBuilder();
            barCodeBuilder.SymbologyType = Symbology.QR;
            barCodeBuilder.QRErrorLevel = QRErrorLevel.LevelM;
            barCodeBuilder.CodeText = url;
            barCodeBuilder.CodeLocation = CodeLocation.None;
            barCodeBuilder.GraphicsUnit = GraphicsUnit.Pixel;
            barCodeBuilder.QREncodeType = QREncodeType.ForceQR;
            barCodeBuilder.Margins.Set(0);
            // Allows to set size for whole picture with barcode inside and Save image on local disk
            Bitmap bitmap = barCodeBuilder.GetCustomSizeBarCodeImage(new Size(35, 35), false);
            MemoryStream img = new MemoryStream();
            bitmap.Save(img, ImageFormat.Png);
            img.Position = 0;
            Shape image = new Shape(document, ShapeType.Image);
            image.ImageData.SetImage(img);
            image.Left = 0;
            image.Top = 0;
            image.Width = (double)image.ImageData.ImageSize.WidthPixels * 72 / 96;
            image.Height = (double)image.ImageData.ImageSize.HeightPixels * 72 / 96;
            image.DistanceLeft = 0;
            image.DistanceRight = 0;
            image.WrapType = WrapType.Inline;
            linkP.AppendChild(image);
        }

        public class FindMatchedNodes : IReplacingCallback
        {
            //Store Matched nodes in ArrayList
            public ArrayList nodes = new ArrayList();

            ReplaceAction IReplacingCallback.Replacing(ReplacingArgs e)
            {
                // This is a Run node that contains either the beginning or the complete match.
                Node currentNode = e.MatchNode;

                // The first (and may be the only) run can contain text before the match,
                // in this case it is necessary to split the run.
                if (e.MatchOffset > 0)
                    currentNode = SplitRun((Run)currentNode, e.MatchOffset);

                ArrayList runs = new ArrayList();

                // Find all runs that contain parts of the match string.
                int remainingLength = e.Match.Value.Length;
                while (

                    (remainingLength > 0) &&

                    (currentNode != null) &&

                    (currentNode.GetText().Length <= remainingLength))
                {

                    runs.Add(currentNode);

                    remainingLength = remainingLength - currentNode.GetText().Length;

                    // Select the next Run node.
                    // Have to loop because there could be other nodes such as BookmarkStart etc.
                    do
                    {
                        currentNode = currentNode.NextSibling;
                    }
                    while ((currentNode != null) && (currentNode.NodeType != NodeType.Run));
                }

                // Split the last run that contains the match if there is any text left.
                if ((currentNode != null) && (remainingLength > 0))
                {
                    SplitRun((Run)currentNode, remainingLength);
                    runs.Add(currentNode);
                }

                String runText = "";
                foreach (Run run in runs)
                    runText += run.Text;

                ((Run)runs[0]).Text = runText;

                for (int i = 1; i < runs.Count; i++)
                {
                    ((Run)runs[i]).Remove();
                }

                nodes.Add(runs[0]);

                // Signal to the replace engine to do nothing because we have already done all what we wanted.
                return ReplaceAction.Skip;
            }
        }

        /// <summary>
        /// Splits text of the specified run into two runs.
        /// Inserts the new run just after the specified run.
        /// </summary>
        private static Run SplitRun(Run run, int position)
        {
            Run afterRun = (Run)run.Clone(true);
            afterRun.Text = run.Text.Substring(position);
            run.Text = run.Text.Substring(0, position);
            run.ParentNode.InsertAfter(afterRun, run);
            return afterRun;
        }
    }
}